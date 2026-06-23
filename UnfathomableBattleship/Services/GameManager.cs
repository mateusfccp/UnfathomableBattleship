using System.Data.SQLite;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using System.Drawing;

namespace UnfathomableBattleship.Services
{
    internal class GameManager : IGameManager
    {
        private readonly string _connectionString;
        private readonly object _userId;

        public GameManager(string connectionString, object userId)
        {
            _connectionString = connectionString;
            _userId = userId;
        }

        public IGame NewGame(GameConfiguration configuration)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                var userBoardId = CreateBoard(connection, configuration.BoardSize);
                var enemyBoardId = CreateBoard(connection, configuration.BoardSize);
                string now = DateTime.Now.ToString("O");

                const string query = "INSERT INTO Game (user_id, game_mode,state,user_board_id,enemy_board_id,start_time,last_update) VALUES (@UserId, @mode, @gameState, @userBoardId, @enemyBoardId, @startTime, @lastUpdate); SELECT last_insert_rowid();";
                using var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", _userId);
                command.Parameters.AddWithValue("@mode", configuration.Mode);
                command.Parameters.AddWithValue("@gameState", GameState.InGame);
                command.Parameters.AddWithValue("@userBoardId", userBoardId);
                command.Parameters.AddWithValue("@enemyBoardId", enemyBoardId);
                command.Parameters.AddWithValue("@startTime", now);
                command.Parameters.AddWithValue("@lastUpdate", now);
                var gameId = Convert.ToInt32(command.ExecuteScalar());

                const string userQuery = "SELECT user_name FROM User WHERE user_id = @id;";
                using var userCmd = new SQLiteCommand(userQuery, connection);
                userCmd.Parameters.AddWithValue("@id", _userId);
                string username = Convert.ToString(userCmd.ExecuteScalar()) ?? "Usuario Desconocido";

                var description = new GameDescription(
                    gameId,
                    username,
                    DateTime.Parse(now),
                    DateTime.Parse(now),
                    null,
                    TimeSpan.Zero,
                    GameState.InGame,
                    configuration
                );

                transaction.Commit();
                return new Game(description, _connectionString);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public IGame QuickGame()
        {
            GameConfiguration configuration;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                const string query = @"
    SELECT g.game_mode, b.width, b.height, g.user_board_id 
    FROM Game g
    INNER JOIN Board b ON g.user_board_id = b.board_id
    WHERE g.user_id = @UserId
    ORDER BY g.game_id DESC 
    LIMIT 1;";
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", _userId);
                (GameMode, Size, int) gameInfo;
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) throw new InvalidOperationException("No existing game found for quick start.");
                    gameInfo =
                            ((GameMode)reader.GetInt32(0),
                            new Size(reader.GetInt32(1), reader.GetInt32(2)),
                            reader.GetInt32(3));

                }
                var shipsCommand = new SQLiteCommand("SELECT length, orientation FROM Ship WHERE board_id = @boardId;", connection);
                shipsCommand.Parameters.AddWithValue("@boardId", gameInfo.Item3);
                using var shipsReader = shipsCommand.ExecuteReader();
                var result = new List<Ship>();
                while (shipsReader.Read())
                {
                    result.Add(new Ship
                    (
                        shipsReader.GetInt32(0),
                        (ShipOrientation)shipsReader.GetInt32(1)
                    ));
                }
                configuration = new GameConfiguration(gameInfo.Item1, gameInfo.Item2, result);
            }
            return NewGame(configuration);
        }

        private static int CreateBoard(SQLiteConnection connection, Size boardSize)
        {
            const string query = "INSERT INTO Board (width, height) VALUES (@width, @height); SELECT last_insert_rowid();";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@width", boardSize.Width);
            command.Parameters.AddWithValue("@height", boardSize.Height);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public void DeleteGame(object id)
        {
            int targetGameId = Convert.ToInt32(id);

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var (userBoardId, enemyBoardId) = GetBoardIdsForDeletion(connection, targetGameId);
                DeleteBoardsContent(connection, userBoardId, enemyBoardId);
                DeleteGameRecord(connection, targetGameId);
                DeleteBoards(connection, userBoardId, enemyBoardId);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private (int userBoardId, int enemyBoardId) GetBoardIdsForDeletion(SQLiteConnection connection, int targetGameId)
        {
            const string query = "SELECT user_board_id, enemy_board_id FROM Game WHERE game_id = @gameId AND user_id = @userId;";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@gameId", targetGameId);
            command.Parameters.AddWithValue("@userId", _userId);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("La partida no existe o no tienes permisos para borrarla.");
            }

            return (reader.GetInt32(0), reader.GetInt32(1));
        }

        private static void DeleteBoardsContent(SQLiteConnection connection, int userBoardId, int enemyBoardId)
        {
            const string query = @"
        DELETE FROM Shot WHERE board_id IN (@userBoard, @enemyBoard);
        DELETE FROM Ship WHERE board_id IN (@userBoard, @enemyBoard);";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@userBoard", userBoardId);
            command.Parameters.AddWithValue("@enemyBoard", enemyBoardId);
            command.ExecuteNonQuery();
        }

        private void DeleteGameRecord(SQLiteConnection connection, int targetGameId)
        {
            const string query = "DELETE FROM Game WHERE game_id = @gameId AND user_id = @userId;";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@gameId", targetGameId);
            command.Parameters.AddWithValue("@userId", _userId);
            command.ExecuteNonQuery();
        }

        private static void DeleteBoards(SQLiteConnection connection, int userBoardId, int enemyBoardId)
        {
            const string query = "DELETE FROM Board WHERE board_id IN (@userBoard, @enemyBoard);";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@userBoard", userBoardId);
            command.Parameters.AddWithValue("@enemyBoard", enemyBoardId);
            command.ExecuteNonQuery();
        }

        public List<GameDescription> GetAllGames()
        {
            return FetchGamesData(false);
        }

        public List<GameDescription> GetCurrentPlayerGames()
        {
            return FetchGamesData(true);
        }

        private List<GameDescription> FetchGamesData(bool filterByCurrentUser)
        {
            var rawGames = GetRawGameList(filterByCurrentUser);
            var finalGamesList = new List<GameDescription>();

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            foreach (var (gameId, username, startTime, lastUpdate, endTime, state, mode, boardSize, boardId) in rawGames)
            {
                var ships = new List<Ship>();
                const string shipQuery = "SELECT length, orientation FROM Ship WHERE board_id = @boardId;";

                using (var shipCommand = new SQLiteCommand(shipQuery, connection))
                {
                    shipCommand.Parameters.AddWithValue("@boardId", boardId);
                    using var shipReader = shipCommand.ExecuteReader();
                    while (shipReader.Read())
                    {
                        ships.Add(new Ship(
                            shipReader.GetInt32(0),
                            (ShipOrientation)shipReader.GetInt32(1)
                        ));
                    }
                }

                var config = new GameConfiguration(mode, boardSize, ships);
                finalGamesList.Add(new GameDescription(gameId, username, startTime, lastUpdate, endTime, TimeSpan.Zero, state, config));
            }

            return finalGamesList;
        }

        private List<(int gameId, string username, DateTime startTime, DateTime lastUpdate, DateTime? endTime, GameState state, GameMode mode, Size boardSize, int boardId)> GetRawGameList(bool filterByCurrentUser)
        {
            var rawGames = new List<(int gameId, string username, DateTime startTime, DateTime lastUpdate, DateTime? endTime, GameState state, GameMode mode, Size boardSize, int boardId)>();
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = @"
        SELECT 
            g.game_id, 
            u.user_name, 
            g.start_time, 
            g.last_update, 
            g.end_time, 
            g.state, 
            g.game_mode, 
            b.width, 
            b.height, 
            g.user_board_id
        FROM Game g
        INNER JOIN User u ON g.user_id = u.user_id
        INNER JOIN Board b ON g.user_board_id = b.board_id";

            if (filterByCurrentUser)
            {
                query += " WHERE g.user_id = @UserId";
            }

            query += " ORDER BY g.last_update DESC;";

            using (var command = new SQLiteCommand(query, connection))
            {
                if (filterByCurrentUser)
                {
                    command.Parameters.AddWithValue("@UserId", _userId);
                }

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rawGames.Add((
                        gameId: reader.GetInt32(0),
                        username: reader.GetString(1),
                        startTime: DateTime.Parse(reader.GetString(2)),
                        lastUpdate: DateTime.Parse(reader.GetString(3)),
                        endTime: reader.IsDBNull(4) ? null : DateTime.Parse(reader.GetString(4)),
                        state: (GameState)reader.GetInt32(5),
                        mode: (GameMode)reader.GetInt32(6),
                        boardSize: new Size(reader.GetInt32(7), reader.GetInt32(8)),
                        boardId: reader.GetInt32(9)
                    ));
                }
            }
            return rawGames;
        }

        public IGame LoadGame(object id)
        {
            int targetGameId = Convert.ToInt32(id);
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            var (mode, state, boardSize, userBoardId, enemyBoardId, username, startTime, lastUpdate, endTime, elapsedTime) = GetGameInfo(connection, targetGameId);
            var (playerShips, enemyShips, allShipsForConfig) = LoadShips(connection, userBoardId, enemyBoardId);
            var (playerBoard, enemyBoard) = LoadShots(connection, userBoardId, enemyBoardId, boardSize);
            var (targetQueue, shipHits) = LoadAiMemory(connection, targetGameId);

            var configuration = new GameConfiguration(mode, boardSize, allShipsForConfig);

            var description = new GameDescription(
                targetGameId,
                username,
                startTime,
                lastUpdate,
                endTime,
                elapsedTime,
                state,
                configuration
            );
            return new Game(description, _connectionString, playerShips, enemyShips, playerBoard, enemyBoard, targetQueue, shipHits);
        }

        private (List<Point> targetQueue, List<Point> shipHits) LoadAiMemory(SQLiteConnection connection, int gameId)
        {
            var targetQueue = new List<Point>();
            var shipHits = new List<Point>();

            const string query = "SELECT pos_x, pos_y, list_type FROM AiMemory WHERE game_id = @gameId;";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@gameId", gameId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var point = new Point(reader.GetInt32(0), reader.GetInt32(1));
                if (reader.GetInt32(2) == 0)
                    targetQueue.Add(point);
                else
                    shipHits.Add(point);
            }

            return (targetQueue, shipHits);
        }

        private (GameMode mode, GameState state, Size boardSize, int userBoardId, int enemyBoardId, string username, DateTime startTime, DateTime lastUpdate, DateTime? endTime, TimeSpan elapsedTime) GetGameInfo(SQLiteConnection connection, int targetGameId)
        {
            const string query = @"
        SELECT g.game_mode, g.state, b.width, b.height, g.user_board_id, g.enemy_board_id, 
               u.user_name, g.start_time, g.last_update, g.end_time, g.elapsed_time
        FROM Game g 
        INNER JOIN Board b ON g.user_board_id = b.board_id 
        INNER JOIN User u ON g.user_id = u.user_id
        WHERE g.game_id = @gameId AND g.user_id = @userId;";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@gameId", targetGameId);
            command.Parameters.AddWithValue("@userId", _userId);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Partida no encontrada o no pertenece a este usuario.");
            }

            return (
                (GameMode)reader.GetInt32(0),
                (GameState)reader.GetInt32(1),
                new Size(reader.GetInt32(2), reader.GetInt32(3)),
                reader.GetInt32(4),
                reader.GetInt32(5),
                reader.GetString(6),
                DateTime.Parse(reader.GetString(7)),
                DateTime.Parse(reader.GetString(8)),
                reader.IsDBNull(9) ? null : DateTime.Parse(reader.GetString(9)),
                TimeSpan.FromTicks(reader.GetInt64(10))
            );
        }

        private static (Dictionary<Point, Ship> playerShips, Dictionary<Point, Ship> enemyShips, List<Ship> allShipsForConfig) LoadShips(SQLiteConnection connection, int userBoardId, int enemyBoardId)
        {
            var playerShips = new Dictionary<Point, Ship>();
            var enemyShips = new Dictionary<Point, Ship>();
            var allShipsForConfig = new List<Ship>();

            const string query = "SELECT board_id, length, orientation, pos_x, pos_y FROM Ship WHERE board_id IN (@userBoard, @enemyBoard);";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@userBoard", userBoardId);
            command.Parameters.AddWithValue("@enemyBoard", enemyBoardId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int currentBoardId = reader.GetInt32(0);
                var ship = new Ship(reader.GetInt32(1), (ShipOrientation)reader.GetInt32(2));
                var position = new Point(reader.GetInt32(3), reader.GetInt32(4));

                if (currentBoardId == userBoardId)
                {
                    playerShips.Add(position, ship);
                    allShipsForConfig.Add(ship);
                }
                else
                {
                    enemyShips.Add(position, ship);
                }
            }

            return (playerShips, enemyShips, allShipsForConfig);
        }

        private static (bool[,] playerBoard, bool[,] enemyBoard) LoadShots(SQLiteConnection connection, int userBoardId, int enemyBoardId, Size boardSize)
        {
            bool[,] playerBoard = new bool[boardSize.Width, boardSize.Height];
            bool[,] enemyBoard = new bool[boardSize.Width, boardSize.Height];

            const string query = "SELECT board_id, pos_x, pos_y FROM Shot WHERE board_id IN (@userBoard, @enemyBoard);";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@userBoard", userBoardId);
            command.Parameters.AddWithValue("@enemyBoard", enemyBoardId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int currentBoardId = reader.GetInt32(0);
                int posX = reader.GetInt32(1);
                int posY = reader.GetInt32(2);

                if (currentBoardId == userBoardId)
                {
                    playerBoard[posX, posY] = true;
                }
                else
                {
                    enemyBoard[posX, posY] = true;
                }
            }

            return (playerBoard, enemyBoard);
        }
    }
}