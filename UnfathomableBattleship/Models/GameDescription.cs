using UnfathomableBattleship.Enums;

namespace UnfathomableBattleship.Models;

/// <summary>
/// The description of a game.
/// </summary>
/// <param name="Id">The unique identifier of a game.</param>
/// <param name="Username">The name of the player that played this game.</param>
/// <param name="StartTime">The date and time the game started.</param>
/// <param name="LastUpdate">The date and time the game was last updated.</param>
/// <param name="EndTime">The date and time in which the game ended, if it ended.</param>
/// <param name="ElapsedTime">The amount of time elapsed since the game started.</param>
/// <param name="State">The state of the game.</param>
/// <param name="Configuration">The configuration of the game.</param>
public record GameDescription(
    object Id,
    string Username,
    DateTime StartTime,
    DateTime LastUpdate,
    DateTime? EndTime,
    TimeSpan ElapsedTime,
    GameState State,
    GameConfiguration Configuration
);
