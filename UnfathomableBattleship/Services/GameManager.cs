using System.Data.SQLite;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Services
{
    internal class GameManager(string connectionString, object userId) : IGameManager
    {
        private object UserId { get; } = userId;

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
            using var connection = new SQLiteConnection(connectionString);
            throw new NotImplementedException();
        }

        public IGame QuickGame()
        {
            throw new NotImplementedException();
        }
    }
}
