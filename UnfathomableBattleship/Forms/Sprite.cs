using System.CodeDom;

namespace UnfathomableBattleship.Forms;

/// <summary>
/// A sprite that can be drawn to a bitmap.
/// </summary>
public class Sprite
{
    public Image SpriteSheet { get; }

    public Sprite(Image spriteSheet, Size? rectSize)
    {
        SpriteSheet = spriteSheet;
        this.rectSize = rectSize;
        var frameWidth = rectSize == null ? spriteSheet.Width : rectSize.Value.Width;
        columnsCount = spriteSheet.Width / frameWidth;
        animations = [];
    }

    public Sprite(Image spriteSheet, Size? rectSize, Dictionary<string, SpriteAnimation> animations, String defaultAnimation)
    {
        SpriteSheet = spriteSheet;
        this.rectSize = rectSize;
        var frameWidth = rectSize == null ? spriteSheet.Width : rectSize.Value.Width;
        columnsCount = spriteSheet.Width / frameWidth;
        this.animations = animations;
        currentAnimation = defaultAnimation;
    }

    private Point FramePoint
    {
        get
        {
            if (CurrentAnimation is SpriteAnimation animation)
            {
                var index = animation.Frames[currentFrame];
                var column = index % columnsCount;
                var row = index / columnsCount;

                return new Point(column * FrameSize.Width, row * FrameSize.Height);
            }
            else
            {
                return Point.Empty;
            }
        }
    }

    private SpriteAnimation? CurrentAnimation
    {
        get
        {
            if (currentAnimation is string key)
            {
                return animations[key];
            }
            else
            {
                return null;
            }
        }
    }

    private Size FrameSize
    {
        get
        {
            if (rectSize is Size size)
            {
                return size;
            }
            else
            {
                return SpriteSheet.Size;
            }
        }
    }

    public Rectangle Rectangle
    {
        get
        {
            return new Rectangle(FramePoint, FrameSize);
        }
    }

    public void Tick()
    {
        if (CurrentAnimation is SpriteAnimation animation)
        {
            currentTick = currentTick + 1;

            if (currentTick == animation.TicksPerFrame)
            {
                currentTick = 0;
                currentFrame = currentFrame + 1;

                if (currentFrame == animation.Frames.Count)
                {
                    currentFrame = 0;
                }
            }
        }
    }

    private int columnsCount;
    private Size? rectSize;
    private int currentFrame = 0;
    private int currentTick = 0;
    private string? currentAnimation;
    private Dictionary<string, SpriteAnimation> animations;
}

public static class SpriteExtensions
{
    public static void DrawSprite(this Graphics graphics, Sprite sprite, Point point)
    {
        graphics.DrawImage(sprite.SpriteSheet, point.X, point.Y, sprite.Rectangle, GraphicsUnit.Pixel);
    }
}