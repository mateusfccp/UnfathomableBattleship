using System.Data.SQLite;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Forms;
using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Models
{
    internal class Game : IGame
    {
        private readonly int _gameId;
        private readonly string _connectionString;
        private List<Point> _targetQueue = new List<Point>();
        private List<Point> _currentShipHits = new List<Point>();

        public bool[,] EnemyBoard { get; private set; }
        public bool[,] PlayerBoard { get; private set; }
        public Dictionary<Point, Ship> PlayerShips { get; private set; }
        public Dictionary<Point, Ship> EnemyShips { get; private set; }
        public Size BoardSize { get; private set; }
        public GameState State { get; private set; }
        public GameDescription Description { get; private set; }
        private TimeSpan _elapsedTime;
        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                // Keep Description in sync so UI that reads Description.ElapsedTime shows updated value
                Description = Description with { ElapsedTime = value };
            }
        }

        public Game(GameDescription description, string connectionString)
        {
            Description = description;
            _connectionString = connectionString;
            _gameId = Convert.ToInt32(description.Id);
            BoardSize = description.Configuration.BoardSize;
            State = description.State;

            EnemyBoard = new bool[BoardSize.Width, BoardSize.Height];
            PlayerBoard = new bool[BoardSize.Width, BoardSize.Height];
            PlayerShips = new Dictionary<Point, Ship>();
            EnemyShips = new Dictionary<Point, Ship>();

            // Initialize ElapsedTime from the description so saves use the correct accumulated time
            ElapsedTime = description.ElapsedTime;

            GenerateEnemyShips(description.Configuration.Ships);
        }

        public Game(GameDescription description, string connectionString,
                    Dictionary<Point, Ship> savedPlayerShips,
                    Dictionary<Point, Ship> savedEnemyShips,
                    bool[,] savedPlayerBoard,
                    bool[,] savedEnemyBoard)
        {
            Description = description;
            _connectionString = connectionString;
            _gameId = Convert.ToInt32(description.Id);
            BoardSize = description.Configuration.BoardSize;
            State = description.State;

            PlayerShips = savedPlayerShips;
            EnemyShips = savedEnemyShips;
            PlayerBoard = savedPlayerBoard;
            EnemyBoard = savedEnemyBoard;

            // Initialize ElapsedTime from the description so saves use the correct accumulated time
            ElapsedTime = description.ElapsedTime;
        }

        private void GenerateEnemyShips(List<Ship> shipsToPlace) // Generacion de los barcos enemigos. Estos se generan en posiciones al azar y orientacion al azar.
        {                                                          // Se utiliza la funcion "IsValidPlacement" para que se coloquen correctamente.
            foreach (var shipTemplate in shipsToPlace)
            {
                bool placed = false;
                while (!placed)
                {
                    int x = Random.Shared.Next(BoardSize.Width);
                    int y = Random.Shared.Next(BoardSize.Height);
                    var orientation = (ShipOrientation)Random.Shared.Next(2);
                    var ship = new Ship(shipTemplate.Length, orientation);

                    if (IsValidPlacement(x, y, ship, EnemyShips))
                    {
                        EnemyShips.Add(new Point(x, y), ship);
                        placed = true;
                    }
                }
            }
        }

        public bool IsValidPlacement(int startX, int startY, Ship ship, Dictionary<Point, Ship> existingShips)  // Funcion para comprobar si es valido el posicionamiento.
        {
            for (int i = 0; i < ship.Length; i++)
            {
                int checkX = startX + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                int checkY = startY + (ship.Orientation == ShipOrientation.Vertical ? i : 0);

                if (checkX >= BoardSize.Width || checkY >= BoardSize.Height) return false;

                foreach (var kvp in existingShips)
                {
                    var existingOrigin = kvp.Key;
                    var existingShip = kvp.Value;
                    for (int j = 0; j < existingShip.Length; j++)
                    {
                        int exX = existingOrigin.X + (existingShip.Orientation == ShipOrientation.Horizontal ? j : 0);
                        int exY = existingOrigin.Y + (existingShip.Orientation == ShipOrientation.Vertical ? j : 0);
                        if (checkX == exX && checkY == exY) return false;
                    }
                }
            }
            return true;
        }

        public bool PlacePlayerShip(Point origin, Ship ship)
        {
            if (!IsValidPlacement(origin.X, origin.Y, ship, PlayerShips)) return false;
            PlayerShips.Add(origin, ship);
            return true;
        }

        public void RemovePlayerShip(Point origin)
        {
            PlayerShips.Remove(origin);
        }

        public Point? AttackCell(Point position)    // Logica de ataque y dificultades de la IA.
        {
            EnemyBoard[position.X, position.Y] = true;

            if (CountSunkShips(EnemyShips, EnemyBoard) == EnemyShips.Count)
            {
                State = GameState.Victory;
                return null;
            }

            Point pcTarget = new Point();
            bool validTarget = false;
            var mode = Description.Configuration.Mode;
            if (mode == GameMode.SinglePlayer) return null;     // MODO SINGLEPLAYER. Si esta activo no se realiza nada.
            do
            {
                switch (mode)
                {
                    case GameMode.MultiPlayerFearAndHunger:        // FEAR AND HUNGER. 
                        pcTarget = GetUnhitShipCell() ?? new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height)); // Ataca a un punto exacto o al azar si es nulo.
                        break;

                    case GameMode.MultiPlayerEasy:      // MULTYPLAYEREASY
                        if (Random.Shared.Next(100) < 50 || _targetQueue.Count == 0)
                        {
                            pcTarget = new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));    // Ataca al azar si se da las condiciones.
                        }
                        else
                        {
                            int r = Random.Shared.Next(_targetQueue.Count); // Si le pego a un barco lanza una moneda de 50/50 para volver a pegarle.
                            pcTarget = _targetQueue[r];
                            _targetQueue.RemoveAt(r);
                        }
                        break;

                    case GameMode.MultiPlayerNormal:       // MODO NORMAL Y DIFICIL
                                                            // La logica del modo normal contiene tambien la del modo dificil ya que la IA funciona de forma similar.
                    case GameMode.MultiPlayerHard:
                        if (_targetQueue.Count > 0)
                        {
                            pcTarget = _targetQueue[0]; // Si tiene memoria de lograr un ataque va a focusear a ese barco
                            _targetQueue.RemoveAt(0);
                        }
                        else if (mode == GameMode.MultiPlayerHard && Random.Shared.Next(100) < 40)  // 60% de probabilidad de disparar al azar, va a pegar 1 de 6 disparos.
                        {
                            pcTarget = GetUnhitShipCell() ?? new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));
                        }
                        else
                        {
                            pcTarget = new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));    // Ataca al azar si no tiene informacion.
                        }
                        break;

                    default:
                        pcTarget = new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));
                        break;
                }
                ////////////////////////// Fin del switch
                // Logica de Queues de targets para la inteligencia de la IA.
                if (!PlayerBoard[pcTarget.X, pcTarget.Y])
                {
                    validTarget = true;
                }
            } while (!validTarget);

            PlayerBoard[pcTarget.X, pcTarget.Y] = true;

            if (IsHit(pcTarget))
            {
                if (mode == GameMode.MultiPlayerNormal || mode == GameMode.MultiPlayerHard)
                {
                    _currentShipHits.Add(pcTarget);
                    if (IsShipSunk(pcTarget))
                    {
                        _currentShipHits.Clear();
                        _targetQueue.Clear();
                    }
                    else
                    {
                        UpdateTargetQueueHard(pcTarget);
                    }
                }
                else if (mode == GameMode.MultiPlayerEasy)
                {
                    AddAdjacentTargets(pcTarget);
                }
            }

            if (CountSunkShips(PlayerShips, PlayerBoard) == PlayerShips.Count)
            {
                State = GameState.GameOver;
                return null;
            }

            return pcTarget;
        }

        private int CountSunkShips(Dictionary<Point, Ship> ships, bool[,] board)       // Contar cantidad de barcos undidos.
        {
            int sunk = 0;
            foreach (var kvp in ships)
            {
                var origin = kvp.Key;
                var ship = kvp.Value;
                int hits = 0;
                for (int i = 0; i < ship.Length; i++)
                {
                    int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                    int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                    if (board[x, y]) hits++;
                }
                if (hits == ship.Length) sunk++;
            }
            return sunk;
        }

        private Point? GetUnhitShipCell()       // Radar trampa de la IA que hace que sepa las posiciones.
        {
            var unhitCells = new List<Point>();
            foreach (var kvp in PlayerShips)
            {
                var origin = kvp.Key;
                var ship = kvp.Value;
                for (int i = 0; i < ship.Length; i++)
                {
                    int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                    int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                    if (!PlayerBoard[x, y]) unhitCells.Add(new Point(x, y));
                }
            }
            if (unhitCells.Count > 0) return unhitCells[Random.Shared.Next(unhitCells.Count)];
            return null;
        }

        private bool IsHit(Point target)
        {
            foreach (var kvp in PlayerShips)
            {
                var origin = kvp.Key;
                var ship = kvp.Value;
                for (int i = 0; i < ship.Length; i++)
                {
                    int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                    int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                    if (x == target.X && y == target.Y) return true;
                }
            }
            return false;
        }

        private bool IsShipSunk(Point hit)
        {
            foreach (var kvp in PlayerShips)
            {
                var origin = kvp.Key;
                var ship = kvp.Value;
                bool hitBelongsToShip = false;
                for (int i = 0; i < ship.Length; i++)
                {
                    int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                    int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                    if (x == hit.X && y == hit.Y) hitBelongsToShip = true;
                }
                if (hitBelongsToShip)
                {
                    int hits = 0;
                    for (int i = 0; i < ship.Length; i++)
                    {
                        int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                        int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                        if (PlayerBoard[x, y]) hits++;
                    }
                    return hits == ship.Length;
                }
            }
            return false;
        }

        private void UpdateTargetQueueHard(Point hit)
        {
            if (_currentShipHits.Count == 1)
            {
                AddAdjacentTargets(hit);
            }
            else if (_currentShipHits.Count >= 2)
            {
                bool isHorizontal = _currentShipHits[0].Y == _currentShipHits[1].Y;
                _targetQueue.RemoveAll(p => isHorizontal ? p.Y != hit.Y : p.X != hit.X);

                Point lastHit = _currentShipHits.Last();
                Point firstHit = _currentShipHits.First();

                if (isHorizontal)
                {
                    int minX = Math.Min(lastHit.X, firstHit.X) - 1;
                    int maxX = Math.Max(lastHit.X, firstHit.X) + 1;
                    if (minX >= 0 && !PlayerBoard[minX, hit.Y] && !_targetQueue.Contains(new Point(minX, hit.Y))) _targetQueue.Insert(0, new Point(minX, hit.Y));
                    if (maxX < BoardSize.Width && !PlayerBoard[maxX, hit.Y] && !_targetQueue.Contains(new Point(maxX, hit.Y))) _targetQueue.Insert(0, new Point(maxX, hit.Y));
                }
                else
                {
                    int minY = Math.Min(lastHit.Y, firstHit.Y) - 1;
                    int maxY = Math.Max(lastHit.Y, firstHit.Y) + 1;
                    if (minY >= 0 && !PlayerBoard[hit.X, minY] && !_targetQueue.Contains(new Point(hit.X, minY))) _targetQueue.Insert(0, new Point(hit.X, minY));
                    if (maxY < BoardSize.Height && !PlayerBoard[hit.X, maxY] && !_targetQueue.Contains(new Point(hit.X, maxY))) _targetQueue.Insert(0, new Point(hit.X, maxY));
                }
            }
        }

        private void AddAdjacentTargets(Point hit)
        {
            Point[] adjacents = {
                new Point(hit.X, hit.Y - 1),
                new Point(hit.X, hit.Y + 1),
                new Point(hit.X - 1, hit.Y),
                new Point(hit.X + 1, hit.Y)
            };

            foreach (var p in adjacents)
            {
                if (p.X >= 0 && p.X < BoardSize.Width && p.Y >= 0 && p.Y < BoardSize.Height)
                {
                    if (!PlayerBoard[p.X, p.Y] && !_targetQueue.Contains(p))
                    {
                        _targetQueue.Add(p);
                    }
                }
            }
        }

        public void Save()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                (int userBoardId, int enemyBoardId) = GetBoardIds(connection);
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
            const string query = @"DELETE FROM Shot WHERE board_id IN (SELECT user_board_id FROM Game WHERE game_id = @gameId UNION SELECT enemy_board_id FROM Game WHERE game_id = @gameId);";
            using var shotCommand = new SQLiteCommand(query, connection);
            shotCommand.Parameters.AddWithValue("@gameId", _gameId);
            shotCommand.ExecuteNonQuery();

            const string shipQuery = @"DELETE FROM Ship WHERE board_id IN (SELECT user_board_id FROM Game WHERE game_id = @gameId UNION SELECT enemy_board_id FROM Game WHERE game_id = @gameId);";
            using var shipCommand = new SQLiteCommand(shipQuery, connection);
            shipCommand.Parameters.AddWithValue("@gameId", _gameId);
            shipCommand.ExecuteNonQuery();
        }

        private void SaveShips(SQLiteConnection connection, int userBoardId, int enemyBoardId)
        {
            const string query = "INSERT INTO Ship (board_id, length, orientation, pos_x, pos_y) VALUES (@boardId, @length, @orientation, @posX, @posY);";
            using var command = new SQLiteCommand(query, connection);

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
            const string query = "UPDATE Game SET last_update = @now, elapsed_time = @elapsed WHERE game_id = @gameId;";
            using var command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@now", DateTime.Now.ToString("O"));
            command.Parameters.AddWithValue("@elapsed", ElapsedTime.Ticks);
            command.Parameters.AddWithValue("@gameId", _gameId);

            if (command.ExecuteNonQuery() == 0)
            {
                throw new Exception("No se pudo actualizar la última hora de actualización del juego.");
            }
        }
    }
}
