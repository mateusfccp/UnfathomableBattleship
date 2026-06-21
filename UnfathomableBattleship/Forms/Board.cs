using System.Drawing.Imaging;
using System.Security.Policy;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

public class Board : IGameObject
{
    private readonly bool HiddenMode;
    private readonly WaterTile[,] WaterTiles;
    private readonly ShipGameObject[,] Ships;
    private readonly bool[,] BoardDestruction;
    private readonly Dictionary<ShipGameObject, Point> ShipOrigins = [];

    private readonly static Sprite SmallFireSprite = new(
        Resources.fire,
        new()
        {
            ["small"] = new SpriteAnimation(10, [196, 197, 198, 199]),
        },
        "small",
        new Size(32, 32)
    );

    private readonly static Sprite BigFireSprite = new(
        Resources.fire,
        new()
        {
            ["big"] = new SpriteAnimation(10, [131, 132, 133, 134]),
        },
        "big",
        new Size(32, 32)
    );

    private readonly static Sprite Bubbles = new(
        Resources.water,
        new()
        {
            ["idle"] = new SpriteAnimation(60, [56, 45, 68, 45]),
        },
        "idle",
        new Size(16, 16)
    );

    public Size Size { get; init; }

    public Board(Size size, Dictionary<Point, Ship> ships, bool[,] boardDestruction, bool hiddenMode)
    {
        HiddenMode = hiddenMode;
        Size = size;
        BoardDestruction = boardDestruction;
        WaterTiles = new WaterTile[Size.Width, Size.Height];
        Ships = new ShipGameObject[Size.Width, Size.Height];

        for (var column = 0; column < Size.Width; column++)
        {
            for (var line = 0; line < Size.Height; line++)
            {
                WaterTiles[column, line] = new WaterTile();
            }
        }

        foreach (var pair in ships)
        {
            var origin = pair.Key;
            var ship = pair.Value;
            var shipGameObject = new ShipGameObject(ship);

            ShipOrigins[shipGameObject] = origin;
            for (var i = 0; i < ship.Length; i++)
            {
                var currentX = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                var currentY = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);

                if (currentX >= 0 && currentX < Size.Width && currentY >= 0 && currentY < Size.Height)
                {
                    Ships[currentX, currentY] = shipGameObject;
                }
            }
        }
    }

    public void Draw(Graphics graphics, Point _)
    {
        // Tiles
        for (int column = 0; column < WaterTiles.GetLength(0); column++)
        {
            for (int line = 0; line < WaterTiles.GetLength(1); line++)
            {
                var tile = WaterTiles[column, line];
                var point = new Point(column * GameForm.TileDimension, line * GameForm.TileDimension);
                tile.Draw(graphics, point);
            }
        }

        // Grid lines
        for (int i = 0; i < Size.Width; i++)
        {
            var p1 = new Point(i * GameForm.TileDimension, 0);
            var p2 = new Point(i * GameForm.TileDimension, Size.Height * GameForm.TileDimension);
            graphics.DrawLine(Pens.LightSlateGray, p1, p2);
        }

        for (int j = 0; j < Size.Height; j++)
        {
            var p1 = new Point(0, j * GameForm.TileDimension);
            var p2 = new Point(Size.Width * GameForm.TileDimension, j * GameForm.TileDimension);
            graphics.DrawLine(Pens.LightSlateGray, p1, p2);
        }

        // Ships
        foreach (var pair in ShipOrigins)
        {
            var shipGameObject = pair.Key;

            if (IsShipSunken(shipGameObject) || HiddenMode) continue;

            var origin = pair.Value;
            var point = new Point(origin.X * GameForm.TileDimension, origin.Y * GameForm.TileDimension);
            shipGameObject.Draw(graphics, point);
        }

        // Hit places && hiding mist
        for (int column = 0; column < Size.Width; column++)
        {
            for (int line = 0; line < Size.Height; line++)
            {
                if (BoardDestruction[column, line])
                {
                    var shipGameObject = Ships[column, line];
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
        }
    }

    public void Tick()
    {
        SmallFireSprite.Tick();
        BigFireSprite.Tick();
        Bubbles.Tick();
        foreach (var tile in WaterTiles) { tile.Tick(); }
        foreach (var ship in Ships) { ship?.Tick(); }
    }

    private int GetHitCount(ShipGameObject targetShip)
    {
        if (ShipOrigins.TryGetValue(targetShip, out Point origin))
        {
            return CalculateHitsFromOrigin(origin.X, origin.Y, targetShip);
        }
        else
        {
            return 0;
        }
    }

    private int CalculateHitsFromOrigin(int startX, int startY, ShipGameObject shipGameObject)
    {
        int hits = 0;
        int length = shipGameObject.Ship.Length;
        var orientation = shipGameObject.Ship.Orientation;

        for (var i = 0; i < length; i++)
        {
            int currentX = startX + (orientation == ShipOrientation.Horizontal ? i : 0);
            int currentY = startY + (orientation == ShipOrientation.Vertical ? i : 0);

            if (currentX >= 0 && currentX < Size.Width && currentY >= 0 && currentY < Size.Height)
            {
                if (BoardDestruction[currentX, currentY])
                {
                    hits++;
                }
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
