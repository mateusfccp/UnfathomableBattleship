using System.Data.SQLite;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Models
{
    internal class Game : IGame
    {
        private readonly int _gameId;
        private readonly string _connectionString;

        public bool[,] EnemyBoard { get; private set; }
        public bool[,] PlayerBoard { get; private set; }
        public Dictionary<Point, Ship> PlayerShips { get; private set; }
        public Dictionary<Point, Ship> EnemyShips { get; private set; }
        public Size BoardSize { get; private set; }
        public GameState State { get; private set; }

        /// <summary>
        /// Constructor para NUEVAS partidas.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="config"></param>
        /// <param name="connectionString"></param>
        public Game(int gameId, GameConfiguration config, string connectionString)
        {
            _gameId = gameId;
            _connectionString = connectionString;
            BoardSize = config.BoardSize;
            State = GameState.InGame;

            EnemyBoard = new bool[BoardSize.Width, BoardSize.Height];
            PlayerBoard = new bool[BoardSize.Width, BoardSize.Height];
            PlayerShips = [];
            EnemyShips = [];
        }
        /// <summary>
        /// Constructor para partidas que se están cargando desde la base de datos.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="config"></param>
        /// <param name="connectionString"></param>
        /// <param name="savedState"></param>
        /// <param name="savedPlayerShips"></param>
        /// <param name="savedEnemyShips"></param>
        /// <param name="savedPlayerBoard"></param>
        /// <param name="savedEnemyBoard"></param>
        public Game(int gameId, GameConfiguration config, string connectionString,
                    GameState savedState,
                    Dictionary<Point, Ship> savedPlayerShips,
                    Dictionary<Point, Ship> savedEnemyShips,
                    bool[,] savedPlayerBoard,
                    bool[,] savedEnemyBoard)
        {
            _gameId = gameId;
            _connectionString = connectionString;
            BoardSize = config.BoardSize;
            State = savedState;

            PlayerShips = savedPlayerShips;
            EnemyShips = savedEnemyShips;
            PlayerBoard = savedPlayerBoard;
            EnemyBoard = savedEnemyBoard;
        }
        public Point? AttackCell(Point position)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                (int userBoardId, int enemyBoardId) = GetBoardIds(connection);

                // 2. Ejecutamos el guardado pasando los IDs ya calculados
                ClearBoards(connection);
                SaveShips(connection, userBoardId, enemyBoardId);
                SaveShots(connection, userBoardId, enemyBoardId);
                RefreshLastUpdateTime(connection);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private void ClearBoards(SQLiteConnection connection)
        {
            //To delete all shots related to the game
            const string query = @"DELETE FROM Shot WHERE board_id IN 
(SELECT user_board_id FROM Game WHERE game_id = @gameId 
UNION SELECT enemy_board_id FROM Game WHERE game_id = @gameId);";
            var shotCommand = new SQLiteCommand(query, connection);
            shotCommand.Parameters.AddWithValue("@gameId", _gameId);
            shotCommand.ExecuteNonQuery();

            //To delete all ships related to the game 
            const string shipQuery = @"DELETE FROM Ship WHERE board_id IN 
(SELECT user_board_id FROM Game WHERE game_id = @gameId 
UNION SELECT enemy_board_id FROM Game WHERE game_id = @gameId);";
            var shipCommand = new SQLiteCommand(shipQuery, connection);
            shipCommand.Parameters.AddWithValue("@gameId", _gameId);
            shipCommand.ExecuteNonQuery();
        }

        private void SaveShips(SQLiteConnection connection, int userBoardId, int enemyBoardId)
        {
            const string query = "INSERT INTO Ship (board_id, length, orientation, pos_x, pos_y) VALUES (@boardId, @length, @orientation, @posX, @posY);";
            using var command = new SQLiteCommand(query, connection);

           //To create void parameters and avoid create them in each loop iteration.
            command.Parameters.Add("@boardId", System.Data.DbType.Int32);
            command.Parameters.Add("@length", System.Data.DbType.Int32);
            command.Parameters.Add("@orientation", System.Data.DbType.Int32);
            command.Parameters.Add("@posX", System.Data.DbType.Int32);
            command.Parameters.Add("@posY", System.Data.DbType.Int32);

            foreach (var (position, ship) in PlayerShips)
            {
                command.Parameters["@boardId"].Value = userBoardId;
                command.Parameters["@length"].Value = ship.Length;
                command.Parameters["@orientation"].Value = (int)ship.Orientation;
                command.Parameters["@posX"].Value = position.X;
                command.Parameters["@posY"].Value = position.Y;
                command.ExecuteNonQuery();
            }

            foreach (var (position, ship) in EnemyShips)
            {
                command.Parameters["@boardId"].Value = enemyBoardId;
                command.Parameters["@length"].Value = ship.Length;
                command.Parameters["@orientation"].Value = (int)ship.Orientation;
                command.Parameters["@posX"].Value = position.X;
                command.Parameters["@posY"].Value = position.Y;
                command.ExecuteNonQuery();
            }
        }

        private (int userBoardId, int enemyBoardId) GetBoardIds(SQLiteConnection connection)
        {
            const string query = "SELECT user_board_id, enemy_board_id FROM Game WHERE game_id = @gameId;";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@gameId", _gameId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return (reader.GetInt32(0), reader.GetInt32(1));
            }
            throw new Exception("No se encontraron los tableros para este juego.");
        }

        private void SaveShots(SQLiteConnection connection, int userBoardId, int enemyBoardId)
        {
            const string query = "INSERT INTO Shot (board_id, pos_x, pos_y) VALUES (@boardId, @posX, @posY);";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.Add("@boardId", System.Data.DbType.Int32);
            command.Parameters.Add("@posX", System.Data.DbType.Int32);
            command.Parameters.Add("@posY", System.Data.DbType.Int32);

            for (int x = 0; x < BoardSize.Width; x++)
            {
                for (int y = 0; y < BoardSize.Height; y++)
                {
                    if (PlayerBoard[x, y])
                    {
                        command.Parameters["@boardId"].Value = enemyBoardId;
                        command.Parameters["@posX"].Value = x;
                        command.Parameters["@posY"].Value = y;
                        command.ExecuteNonQuery();
                    }

                    if (EnemyBoard[x, y])
                    {
                        command.Parameters["@boardId"].Value = userBoardId;
                        command.Parameters["@posX"].Value = x;
                        command.Parameters["@posY"].Value = y;
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private void RefreshLastUpdateTime(SQLiteConnection connection)
        {
            const string query = "UPDATE Game SET last_update = @now WHERE game_id = @gameId;";
            var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@now", DateTime.Now.ToString("O"));
            command.Parameters.AddWithValue("@gameId", _gameId);
            if(command.ExecuteNonQuery() == 0)
            {
                throw new Exception("No se pudo actualizar la última hora de actualización del juego.");
            }
        }
    }
}
