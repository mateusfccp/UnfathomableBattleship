using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Interfaces;

/// <summary>
/// A service that manages games.
/// </summary>
public interface IGameManager
{
    /// <summary>
    /// Creates a new game based on the last used configurations.
    /// </summary>
    /// <returns>The newly created game object.</returns>
    IGame QuickGame();

    /// <summary>
    /// Creates a new game.
    /// </summary>
    /// <param name="configuration">The configuration of the game.</param>
    /// <returns>The newly created game object.</returns>
    IGame NewGame(GameConfiguration configuration);

    /// <summary>
    /// Loads an existing game.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    /// <returns>The loaded game object.</returns>
    IGame LoadGame(object id);

    /// <summary>
    /// Deletes an existing game.
    /// </summary>
    /// <param name="id">The unique identifier of the game.</param>
    void DeleteGame(object id);

    /// <summary>
    /// Gets all games, from all players.
    /// </summary>
    /// <returns>A list with all stored games.</returns>
    List<GameDescription> GetAllGames();

    /// <summary>
    /// Gets all games, from the current player.
    /// </summary>
    /// <returns>A list with all stored games.</returns>
    List<GameDescription> GetCurrentPlayerGames();
}
