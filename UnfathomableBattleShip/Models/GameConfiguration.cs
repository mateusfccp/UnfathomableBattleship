using System.Drawing;
using UnfathomableBattleship.Enums;

namespace UnfathomableBattleship.Models;

/// <summary>
/// The configuration of a game.
/// </summary>
/// <param name="Mode">The mode of the game.</param>
/// <param name="BoardSize">The size of the board.</param>
/// <param name="Ships">A list of ships to place on the board. Both the player and the enemy will have the same ships.</param>
public record GameConfiguration(
    GameMode Mode,
    Size BoardSize,
    List<Ship> Ships
);
