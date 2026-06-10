using UnfathomableBattleship.Enums;

namespace UnfathomableBattleship.Models;

/// <summary>
/// A ship in a battleship board.
/// </summary>
/// <param name="Length">How many cells the ship occupies in the board.</param>
/// <param name="Orientation">The orientation of the ship.</param>
public record Ship(
    int Length,
    ShipOrientation Orientation
);
