using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

public class Board : IGameObject
{
    private readonly bool _hiddenMode;
    private readonly WaterTile[,] _waterTiles;
    private readonly ShipGameObject?[,] _ships;
    private readonly bool[,] _boardDestruction;
    private readonly Dictionary<ShipGameObject, Point> _shipOrigins = [];
    private readonly List<Explosion> _explosions = [];
    private record Explosion(Sprite Sprite, Point Coordinate);

    private static readonly Sprite SmallFireSprite = new(
        Resources.fire,
        new()
        {
            ["small"] = new SpriteAnimation(10, [196, 197, 198, 199]),
        },
        "small",
        new Size(32, 32)
    );

    private static readonly Sprite BigFireSprite = new(
        Resources.fire,
        new()
        {
            ["big"] = new SpriteAnimation(10, [131, 132, 133, 134]),
        },
        "big",
        new Size(32, 32)
    );

    private static readonly Sprite Bubbles = new(
        Resources.water,
        new()
        {
            ["idle"] = new SpriteAnimation(60, [56, 45, 68, 45]),
        },
        "idle",
        new Size(16, 16)
    );

    public Size Size { get; }

    public Board(Size size, Dictionary<Point, Ship> ships, bool[,] boardDestruction, bool hiddenMode)
    {
        _hiddenMode = hiddenMode;
        Size = size;
        _boardDestruction = boardDestruction;
        _waterTiles = new WaterTile[Size.Width, Size.Height];
        _ships = new ShipGameObject[Size.Width, Size.Height];

        for (var column = 0; column < Size.Width; column++)
        {
            for (var line = 0; line < Size.Height; line++)
            {
                _waterTiles[column, line] = new WaterTile();
            }
        }

        foreach (var pair in ships)
        {
            var origin = pair.Key;
            var ship = pair.Value;
            var shipGameObject = new ShipGameObject(ship);

            _shipOrigins[shipGameObject] = origin;
            for (var i = 0; i < ship.Length; i++)
            {
                var currentX = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                var currentY = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);

                if (currentX >= 0 && currentX < Size.Width && currentY >= 0 && currentY < Size.Height)
                {
                    _ships[currentX, currentY] = shipGameObject;
                }
            }
        }
    }

    /// <summary>
    /// Play an explosion animation on the given tile.
    /// </summary>
    /// <param name="tileCoordinate">The tile coordinate of the explosion.</param>
    /// <param name="onFinish">The action to happen when the animation ends.</param>
    /// <returns></returns>
    public void PlayExplosion(Point tileCoordinate, Action onFinish)
    {
        var isShip = _ships[tileCoordinate.X, tileCoordinate.Y] != null;
        var image = isShip ? Resources.fire : Resources.fire_blue;
        var frames = isShip ? new List<int> { 276, 277, 278, 279 } : new List<int> { 296, 297, 298, 299 };

        var explosionSprite = new Sprite(
            image,
            new Dictionary<string, SpriteAnimation>
            {
                ["explode"] = new SpriteAnimation(5, frames),
            },
            "explode",
            new Size(32, 32)
        );

        explosionSprite.Play(() =>
        {
            _explosions.RemoveAll(e => e.Sprite == explosionSprite);
            onFinish();
        });

        _explosions.Add(new Explosion(explosionSprite, tileCoordinate));
    }

    public void Draw(Graphics graphics, Point _)
    {
        // Tiles
        for (var column = 0; column < _waterTiles.GetLength(0); column++)
        {
            for (var line = 0; line < _waterTiles.GetLength(1); line++)
            {
                var tile = _waterTiles[column, line];
                var point = new Point(column * GameForm.TileDimension, line * GameForm.TileDimension);
                tile.Draw(graphics, point);
            }
        }

        // Grid lines
        for (var i = 0; i < Size.Width; i++)
        {
            var p1 = new Point(i * GameForm.TileDimension, 0);
            var p2 = new Point(i * GameForm.TileDimension, Size.Height * GameForm.TileDimension);
            graphics.DrawLine(Pens.LightSlateGray, p1, p2);
        }

        for (var j = 0; j < Size.Height; j++)
        {
            var p1 = new Point(0, j * GameForm.TileDimension);
            var p2 = new Point(Size.Width * GameForm.TileDimension, j * GameForm.TileDimension);
            graphics.DrawLine(Pens.LightSlateGray, p1, p2);
        }

        // Ships
        foreach (var pair in _shipOrigins)
        {
            var shipGameObject = pair.Key;

            if (IsShipSunken(shipGameObject) || _hiddenMode) continue;

            var origin = pair.Value;
            var point = new Point(origin.X * GameForm.TileDimension, origin.Y * GameForm.TileDimension);
            shipGameObject.Draw(graphics, point);
        }

        // Hit places && hiding mist
        for (var column = 0; column < Size.Width; column++)
        {
            for (var line = 0; line < Size.Height; line++)
            {
                if (!_boardDestruction[column, line]) continue;

                var shipGameObject = _ships[column, line];
                var point = new Point(column * GameForm.TileDimension, line * GameForm.TileDimension);

                if (shipGameObject != null)
                {
                    var damageLevel = GetHitCount(shipGameObject);

                    if (damageLevel == shipGameObject.Ship.Length)
                    {
                        var rect = new Rectangle(point, GameForm.TileSize);
                        var color = Color.FromArgb(200, Color.IndianRed);
                        using var pen = new Pen(color, 2.0f);
                        graphics.DrawRectangle(pen, rect);

                        point.Offset(GameForm.TileDimension / 2, GameForm.TileDimension / 2);
                        point.Offset(-Bubbles.Rectangle.Width / 2, -Bubbles.Rectangle.Height / 2);
                        graphics.DrawSprite(Bubbles, point);
                    }
                    else
                    {
                        point.Offset(0, -8);
                        var sprite = damageLevel switch
                        {
                            1 => SmallFireSprite,
                            _ => BigFireSprite
                        };
                        graphics.DrawSprite(sprite, point);
                    }
                }
                else
                {
                    var rect = new Rectangle(point, GameForm.TileSize);
                    var color = Color.FromArgb(100, Color.WhiteSmoke);
                    using var pen = new Pen(color, 2.0f);
                    graphics.DrawRoundedRectangle(pen, rect, GameForm.TileSize);

                    point.Offset(GameForm.TileDimension / 2, GameForm.TileDimension / 2);
                    point.Offset(-Bubbles.Rectangle.Width / 2, -Bubbles.Rectangle.Height / 2);
                    graphics.DrawSprite(Bubbles, point);
                }
            }
        }

        // Explosions
        foreach (var explosion in _explosions.ToList())
        {
            var point = new Point(explosion.Coordinate.X * GameForm.TileDimension, explosion.Coordinate.Y * GameForm.TileDimension);
            graphics.DrawSprite(explosion.Sprite, point);
        }
    }

    public void Tick()
    {
        SmallFireSprite.Tick();
        BigFireSprite.Tick();
        Bubbles.Tick();
        foreach (var tile in _waterTiles)
        {
            tile.Tick();
        }

        foreach (var ship in _ships)
        {
            ship?.Tick();
        }

        foreach (var explosion in _explosions.ToList())
        {
            explosion.Sprite.Tick();
        }
    }

    private int GetHitCount(ShipGameObject targetShip)
    {
        return _shipOrigins.TryGetValue(targetShip, out Point origin)
            ? CalculateHitsFromOrigin(origin.X, origin.Y, targetShip)
            : 0;
    }

    private int CalculateHitsFromOrigin(int startX, int startY, ShipGameObject shipGameObject)
    {
        var hits = 0;
        var length = shipGameObject.Ship.Length;
        var orientation = shipGameObject.Ship.Orientation;

        for (var i = 0; i < length; i++)
        {
            var currentX = startX + (orientation == ShipOrientation.Horizontal ? i : 0);
            var currentY = startY + (orientation == ShipOrientation.Vertical ? i : 0);

            if (currentX < 0 || currentX >= Size.Width || currentY < 0 || currentY >= Size.Height) continue;
            if (_boardDestruction[currentX, currentY])
            {
                hits++;
            }
        }

        return hits;
    }

    private bool IsShipSunken(ShipGameObject shipGameObject)
    {
        var hitCount = GetHitCount(shipGameObject);
        return hitCount == shipGameObject.Ship.Length;
    }
}
