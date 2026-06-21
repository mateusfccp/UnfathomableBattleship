using System.Diagnostics;
using System.Drawing;

namespace UnfathomableBattleship.Forms;

/// <summary>
/// A sprite that can be drawn to a bitmap.
/// </summary>
public class Sprite
{
    /// <summary>
    /// The image that contains the sprite.
    /// </summary>
    public Image SpriteSheet { get; }

    public bool IsFinished { get; private set; }
    public event Action? OnAnimationFinished;

    /// <summary>
    /// Creates a new sprite.
    /// </summary>
    /// <param name="spriteSheet">The image that contains the sprite.</param>
    /// <param name="rectSize">The size of the rect that will be shown within the sprite sheet. If null, it will take the size of the entire image.</param>
    public Sprite(Image spriteSheet, Size? rectSize = null)
    {
        SpriteSheet = spriteSheet;
        _rectSize = rectSize;
        var frameWidth = rectSize == null ? spriteSheet.Width : rectSize.Value.Width;
        _columnsCount = spriteSheet.Width / frameWidth;
        _animations = [];
    }

    /// <summary>
    /// Creates a new sprite with animations.
    /// </summary>
    /// <param name="spriteSheet">The image that contains the sprite.</param>
    /// <param name="animations">A dictionary of named animations.</param>
    /// <param name="defaultAnimation">The default animation name.</param>
    /// <param name="rectSize">The size of the rect that will be shown within the sprite sheet. If null, it will take the size of the entire image.</param>
    public Sprite(Image spriteSheet, Dictionary<string, SpriteAnimation> animations, String defaultAnimation,
        Size? rectSize = null)
    {
        Debug.Assert(animations.ContainsKey(defaultAnimation));

        SpriteSheet = spriteSheet;
        _rectSize = rectSize;
        var frameWidth = rectSize == null ? spriteSheet.Width : rectSize.Value.Width;
        _columnsCount = spriteSheet.Width / frameWidth;
        _animations = animations;
        _currentAnimation = defaultAnimation;
    }

    private Point FramePoint
    {
        get
        {
            if (CurrentAnimation is not { } animation) return Point.Empty;

            var index = animation.Frames[_currentFrame];
            var column = index % _columnsCount;
            var row = index / _columnsCount;

            return new Point(column * FrameSize.Width, row * FrameSize.Height);
        }
    }

    private SpriteAnimation? CurrentAnimation
    {
        get
        {
            if (_currentAnimation is { } key)
            {
                return _animations[key];
            }

            return null;
        }
    }

    private Size FrameSize
    {
        get
        {
            if (_rectSize is Size size)
            {
                return size;
            }

            return SpriteSheet.Size;
        }
    }

    public Rectangle Rectangle => new Rectangle(FramePoint, FrameSize);

    public void Play(Action? onFinish = null)
    {
        _playbackState = PlaybackState.Playing;
        _onFinish = onFinish;
        _currentFrame = 0;
        _currentTick = 0;
    }

    public void Repeat()
    {
        _playbackState = PlaybackState.Repeating;
        _currentFrame = 0;
        _currentTick = 0;
    }

    public void Stop()
    {
        _playbackState = PlaybackState.Stopped;
    }

    public void Tick()
    {
        if (CurrentAnimation is not { } animation) return;
        if (_playbackState == PlaybackState.Stopped) return;

        _currentTick = _currentTick + 1;

        if (_currentTick != animation.TicksPerFrame) return;

        _currentTick = 0;
        _currentFrame = _currentFrame + 1;

        if (_currentFrame == animation.Frames.Count)
        {
            if (_playbackState == PlaybackState.Repeating)
            {
                _currentFrame = 0;
            }
            else if (_playbackState == PlaybackState.Playing)
            {
                _currentFrame = animation.Frames.Count - 1;
                _playbackState = PlaybackState.Stopped;
                var callback = _onFinish;
                _onFinish = null;
                callback?.Invoke();
            }
        }
    }

    private enum PlaybackState { Stopped, Playing, Repeating }
    private PlaybackState _playbackState = PlaybackState.Repeating;
    private Action? _onFinish;

    private readonly int _columnsCount;
    private readonly Size? _rectSize;
    private int _currentFrame;
    private int _currentTick;
    private readonly string? _currentAnimation;
    private readonly Dictionary<string, SpriteAnimation> _animations;
}

public static class SpriteExtensions
{
    public static void DrawSprite(this Graphics graphics, Sprite sprite, Point point)
    {
        graphics.DrawImage(sprite.SpriteSheet, point.X, point.Y, sprite.Rectangle, GraphicsUnit.Pixel);
    }
}
