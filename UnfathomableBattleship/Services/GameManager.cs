using System.Data.SQLite;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Services
{
    internal class GameManager : IGameManager
    {
        private readonly string _connectionString;
        private readonly int _userId;
        public GameManager(string connectionString, int userId)
        {
            _connectionString = connectionString;
            _userId = userId;
        }
        public void DeleteGame(object id)
        {
            throw new NotImplementedException();
        }

        public List<GameDescription> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public List<GameDescription> GetCurrentPlayerGames()
        {
            throw new NotImplementedException();
        }

        public IGame LoadGame(object id)
        {
            throw new NotImplementedException();
        }

        public IGame NewGame(GameConfiguration configuration)
        {
            using(var connection = new SQLiteConnection(_connectionString))
            {
                throw new NotImplementedException();
            }
        }

        public IGame QuickGame()
        {
            throw new NotImplementedException();
        }
    }
}
