namespace UnfathomableBattleship.Enums;

/// <summary>
/// The possible states of the game.
/// </summary>
public enum GameState
{
    /// <summary>
    /// The game is still in progress.
    /// </summary>
    InGame,

    /// <summary>
    /// The game has ended with the victory of the enemy.
    /// </summary>
    GameOver,

    /// <summary>
    /// The game has ended with the victory of the player.
    /// </summary>
    Victory
}

/// <summary>
/// Extensions on <see cref="GameState"/>.
/// </summary>
public static class GameStateExtensions
{
    /// <summary>
    /// Returns whether the game is over.
    /// </summary>
    /// <param name="state">The state.</param>
    /// <returns>Whether the game is over.</returns>
    public static bool IsOver(this GameState state) => state != GameState.InGame;
    public static string ToDisplayString(this GameState gameState) => gameState switch
    {
        GameState.InGame => "En juego",
        GameState.GameOver => "DERROTA",
        GameState.Victory => "VICTORIA",
        _ => gameState.ToString(),
    };
}
