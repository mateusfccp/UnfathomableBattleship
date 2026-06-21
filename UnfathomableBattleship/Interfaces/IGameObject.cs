namespace UnfathomableBattleship.Interfaces;

/// <summary>
/// A game object that can be drawn to the game canvas.
/// </summary>
public interface IGameObject
{
    /// <summary>
    /// Draw this object to the graphics.
    /// </summary>
    /// <param name="graphics">The graphics to which the object will be drawn.</param>
    /// <param name="point">The position in the canvas where it will be drawn.</param>
    public void Draw(Graphics graphics, Point point);

    /// <summary>
    /// Updates the game object according to the clock tick.
    /// </summary>
    public void Tick();
}

/// <summary>
/// Extensions on game objects.
/// </summary>
public static class IGameObjectExtensions
{
    /// <summary>
    /// Draws a game object to a graphics.
    /// </summary>
    /// <param name="graphics">The graphics to which the object will be drawn.</param>
    /// <param name="gameObject">The object to draw.</param>
    /// <param name="point">The position in the canvas where it will be drawn.</param>
    public static void DrawGameObject(this Graphics graphics, IGameObject gameObject, Point point)
    {
        gameObject.Draw(graphics, point);   
    }
}