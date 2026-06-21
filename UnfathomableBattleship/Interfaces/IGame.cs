using System.Drawing;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Interfaces;

public interface IGame
{
    /// <summary>
    /// The state of the enemy board.
    /// <br />
    /// True means a shot was shot in the corresponding cell.
    /// <br />
    /// False means the corresponding cell was never shot.
    /// </summary>
    public bool[,] EnemyBoard { get; }

    /// <summary>
    /// The state of the player board.
    /// <br />
    /// True means a shot was shot in the corresponding cell.
    /// <br />
    /// False means the corresponding cell was never shot.
    /// </summary>
    public bool[,] PlayerBoard { get; }

    /// <summary>
    /// The ships of the player.
    /// </summary>
    public Dictionary<Point, Ship> PlayerShips { get; }

    /// <summary>
    /// The ships of the enemy.
    /// </summary>
    public Dictionary<Point, Ship> EnemyShips { get; }

    /// <summary>
    /// The board size.
    /// </summary>
    public Size BoardSize { get; }

    /// <summary>
    /// Attacks the given cell.
    /// </summary>
    /// <param name="position">The position of the cell to attack.</param>
    /// <returns>The position of the cell the enemy attacked back, if any. The only cases where this should be null are when the result of ¬your attack ends the game or when you are playing in single-player mode.</returns>   
    public Point? AttackCell(Point position);

    /// <summary>
    /// The current state of the game.
    /// </summary>
    public GameState State { get; }
}
