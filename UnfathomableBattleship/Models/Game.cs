using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using UnfathomableBattleship.Enums;
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
        public TimeSpan ElapsedTime { get; set; }

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

            GenerateEnemyShips(description.Configuration.Ships);
        }

        public Game(GameDescription description, string connectionString,
                    Dictionary<Point, Ship> savedPlayerShips,
                    Dictionary<Point, Ship> savedEnemyShips,
                    bool[,] savedPlayerBoard,
                    bool[,] savedEnemyBoard,
                    List<Point> savedTargetQueue,
                    List<Point> savedShipHits)
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

            _targetQueue = savedTargetQueue;
            _currentShipHits = savedShipHits;
        }

        private void GenerateEnemyShips(List<Ship> shipsToPlace)
        {
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

        public bool IsValidPlacement(int startX, int startY, Ship ship, Dictionary<Point, Ship> existingShips)
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

        public Point? AttackCell(Point position)
        {
            EnemyBoard[position.X, position.Y] = true;

            Point pcTarget;
            bool validTarget = false;
            var mode = Description.Configuration.Mode;

            if (mode == GameMode.FearAndHunger)
            {
                pcTarget = GetUnhitShipCell() ?? new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));
                PlayerBoard[pcTarget.X, pcTarget.Y] = true;
                return pcTarget;
            }

            do
            {
                if (_targetQueue.Count > 0)
                {
                    if (mode == GameMode.SinglePlayerEasy)
                    {
                        if (Random.Shared.Next(100) < 50)
                        {
                            pcTarget = new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));
                        }
                        else
                        {
                            int r = Random.Shared.Next(_targetQueue.Count);
                            pcTarget = _targetQueue[r];
                            _targetQueue.RemoveAt(r);
                        }
                    }
                    else
                    {
                        pcTarget = _targetQueue[0];
                        _targetQueue.RemoveAt(0);
                    }
                }
                else
                {
                    if (mode == GameMode.SinglePlayerHard && Random.Shared.Next(100) < 40)
                    {
                        pcTarget = GetUnhitShipCell() ?? new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));
                    }
                    else
                    {
                        pcTarget = new Point(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Height));
                    }
                }

                if (!PlayerBoard[pcTarget.X, pcTarget.Y])
                {
                    validTarget = true;
                }
            } while (!validTarget);

            PlayerBoard[pcTarget.X, pcTarget.Y] = true;

            if (IsHit(pcTarget))
            {
                if (mode == GameMode.SinglePlayerNormal || mode == GameMode.SinglePlayerHard)
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
                else if (mode == GameMode.SinglePlayerEasy)
                {
                    AddAdjacentTargets(pcTarget);
                }
            }

            return pcTarget;
        }

        private Point? GetUnhitShipCell()
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
                SaveAiMemory(connection);
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
            throw new Exception();
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
                throw new Exception();
            }
        }

        private void SaveAiMemory(SQLiteConnection connection)
        {
            using var deleteCmd = new SQLiteCommand("DELETE FROM AiMemory WHERE game_id = @gameId;", connection);
            deleteCmd.Parameters.AddWithValue("@gameId", _gameId);
            deleteCmd.ExecuteNonQuery();

            using var insertCmd = new SQLiteCommand("INSERT INTO AiMemory (game_id, pos_x, pos_y, list_type) VALUES (@gameId, @x, @y, @type);", connection);
            insertCmd.Parameters.Add("@gameId", System.Data.DbType.Int32);
            insertCmd.Parameters.Add("@x", System.Data.DbType.Int32);
            insertCmd.Parameters.Add("@y", System.Data.DbType.Int32);
            insertCmd.Parameters.Add("@type", System.Data.DbType.Int32);

            foreach (var point in _targetQueue)
            {
                insertCmd.Parameters["@gameId"].Value = _gameId;
                insertCmd.Parameters["@x"].Value = point.X;
                insertCmd.Parameters["@y"].Value = point.Y;
                insertCmd.Parameters["@type"].Value = 0;
                insertCmd.ExecuteNonQuery();
            }

            foreach (var point in _currentShipHits)
            {
                insertCmd.Parameters["@gameId"].Value = _gameId;
                insertCmd.Parameters["@x"].Value = point.X;
                insertCmd.Parameters["@y"].Value = point.Y;
                insertCmd.Parameters["@type"].Value = 1;
                insertCmd.ExecuteNonQuery();
            }
        }
    }
}