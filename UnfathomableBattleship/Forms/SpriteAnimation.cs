namespace UnfathomableBattleship.Forms;

/// <summary>
/// An animation that a sprite can use.
/// </summary>
public record SpriteAnimation(int TicksPerFrame, List<int> Frames)
{
    public List<int> Frames { get; init; } = Frames.Count == 0
        ? throw new ArgumentException("Frames cannot be empty.", nameof(Frames))
        : Frames;
}

