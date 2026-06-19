using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Services
{
    internal class GameManager : IGameManager
    {
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
            throw new NotImplementedException();
        }

        public IGame QuickGame()
        {
            throw new NotImplementedException();
        }
    }
}
