namespace UnfathomableBattleship.Forms;

/// <summary>
/// An animation that a sprite can use.
/// </summary>
public record SpriteAnimation(int TicksPerFrame, List<int> Frames)
{
}

