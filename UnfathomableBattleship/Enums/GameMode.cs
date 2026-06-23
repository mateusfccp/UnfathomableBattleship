namespace UnfathomableBattleship.Enums;

/// <summary>
/// The mode of the game.
/// </summary>
public enum GameMode
{
    /// <summary>
    /// The game is played against yourself.
    /// </summary>
    SinglePlayer,

    /// <summary>
    /// The game is played against an AI, easy mode.
    /// </summary>
    MultiPlayerEasy,

    /// <summary>
    /// The game is played against an AI, normal mode.
    /// </summary>
    MultiPlayerNormal,

    /// <summary>
    /// The game is played against an AI, hard mode.
    /// </summary>
    MultiPlayerHard,

    /// <summary>
    /// The game is played against an AI, fear and hunger mode.
    /// </summary>
    MultiPlayerFearAndHunger
}

/// <summary>
/// Extensions for the Game Mode enum.
/// </summary>
public static class GameModeExtensions
{
    /// <summary>
    /// Gets a display string of a Game Mode.
    /// </summary>
    /// <param name="gameMode">This.</param>
    /// <returns>The display string of this game mode.</returns>
    public static string ToDisplayString(this GameMode gameMode) => gameMode switch
    {
        GameMode.SinglePlayer => "Solo (prueba)",
        GameMode.MultiPlayerEasy => "Fácil",
        GameMode.MultiPlayerNormal => "Normal",
        GameMode.MultiPlayerHard => "Duro",
        GameMode.MultiPlayerFearAndHunger => "Miedo y Hambre",
        _ => gameMode.ToString(),
    };
}
