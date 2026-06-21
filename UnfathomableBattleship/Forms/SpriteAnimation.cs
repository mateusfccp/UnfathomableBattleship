namespace UnfathomableBattleship.Forms;

/// <summary>
/// An animation that a sprite can use.
/// </summary>
public record SpriteAnimation(int TicksPerFrame, List<int> Frames)
{
    /// <summary>
    /// The frames of the animation.
    /// </summary>
    public List<int> Frames { get; } = Frames.Count == 0
        ? throw new ArgumentException("Frames cannot be empty.", nameof(Frames))
        : Frames;
}
