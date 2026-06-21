using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

public partial class GameForm : Form
{
    private IGame game;
    private System.Windows.Forms.Timer gameTimer;

    private GameCanvas playerCanvas;
    private GameCanvas enemyCanvas;

    private WaterTile[,] PlayerWaterTiles;
    private WaterTile[,] EnemyWaterTiles;
    private ShipGameObject[,] PlayerShips;
    private ShipGameObject[,] EnemyShips;

    public const int TileSize = 32;

    public GameForm()
    {
        game = new MockGame(8, 8);

        InitializeComponent();

        gameTimer = new()
        {
            Interval = 16
        };
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

        playerCanvas = new GameCanvas(playerCanvasPictureBox, game.BoardSize);
        enemyCanvas = new GameCanvas(enemyCanvasPictureBox, game.BoardSize);

        enemyCanvas.TileClicked += EnemyCanvasTileClicked;

        // Initialize water tiles
        PlayerWaterTiles = new WaterTile[game.BoardSize.Width, game.BoardSize.Height];
        EnemyWaterTiles = new WaterTile[game.BoardSize.Width, game.BoardSize.Height];

        for (var line = 0; line < game.BoardSize.Height; line++)
        {
            for (var column = 0; column < game.BoardSize.Width; column++)
            {
                PlayerWaterTiles[column, line] = new WaterTile();
                EnemyWaterTiles[column, line] = new WaterTile();
            }
        }

        // Initialize ships
        PlayerShips = new ShipGameObject[game.BoardSize.Width, game.BoardSize.Height];
        EnemyShips = new ShipGameObject[game.BoardSize.Width, game.BoardSize.Height];

        for (var line = 0; line < game.BoardSize.Height; line++)
        {
            for (var column = 0; column < game.BoardSize.Width; column++)
            {
                var point = new Point(column, line);
                if (game.PlayerShips.ContainsKey(point) && game.PlayerShips[point] is Ship playerShip)
                {
                    PlayerShips[column, line] = new ShipGameObject(playerShip);
                }

                if (game.EnemyShips.ContainsKey(point) && game.EnemyShips[point] is Ship enemyShip)
                {
                    EnemyShips[column, line] = new ShipGameObject(enemyShip);
                }
            }
        }
    }

    private void GameLoop(object? sender, EventArgs eventArgs)
    {
        UpdateGame();
        RenderGame();

    }

    private void EnemyCanvasTileClicked(object? sender, Point eventArgs)
    {
        game.AttackCell(eventArgs);
    }

    private void UpdateGame()
    {
        playerCanvas.Update();
        enemyCanvas.Update();

        for (var column = 0; column < game.BoardSize.Width; column++)
        {
            for (var line = 0; line < game.BoardSize.Height; line++)

            {
                PlayerWaterTiles[column, line].Tick();
                EnemyWaterTiles[column, line].Tick();
                PlayerShips[column, line]?.Tick();
                EnemyShips[column, line]?.Tick();
            }
        }
    }

    private void RenderGame()
    {
        playerCanvas.Present();
        enemyCanvas.Present();

        playerCanvas.Graphics.Clear(Color.Black);
        enemyCanvas.Graphics.Clear(Color.Black);

        // Layer 1: Board
        DrawBoard(enemyCanvas.Graphics, game.BoardSize, game.EnemyBoard, EnemyShips, EnemyWaterTiles, Brushes.LightGray);
        DrawBoard(playerCanvas.Graphics, game.BoardSize, game.PlayerBoard, PlayerShips, PlayerWaterTiles, Brushes.LightGreen);

        // Layer 2: Cursor
        using var pen = new Pen(Brushes.Yellow, 2.0f);
        if (enemyCanvas.CursorPosition is Point position)
        {
            enemyCanvas.Graphics.DrawRectangle(pen, position.X * TileSize, position.Y * TileSize, TileSize, TileSize);
        }
    }

    private void DrawBoard(Graphics graphics, Size size, bool[,] board, ShipGameObject[,] ships, WaterTile[,] waterTiles, Brush borderBrush)
    {
        // Tiles
        for (int column = 0; column < waterTiles.GetLength(0); column++)
        {
            for (int line = 0; line < waterTiles.GetLength(1); line++)
            {
                var tile = waterTiles[column, line];
                var point = new Point(column * TileSize, line * TileSize);
                tile.Draw(graphics, point);
            }
        }

        using var linesPen = new Pen(borderBrush, 1.0f);

        // Grid lines
        for (int i = 0; i < size.Width; i++)
        {
            var p1 = new Point(i * TileSize, 0);
            var p2 = new Point(i * TileSize, size.Height * TileSize);
            graphics.DrawLine(linesPen, p1, p2);
        }

        for (int j = 0; j < size.Height; j++)
        {
            var p1 = new Point(0, j * TileSize);
            var p2 = new Point(size.Width * TileSize, j * TileSize);
            graphics.DrawLine(linesPen, p1, p2);
        }

        // Ships
        for (int column = 0; column < ships.GetLength(0); column++)
        {
            for (int line = 0; line < ships.GetLength(1); line++)
            {
                var ship = ships[column, line];

                if (ship != null)
                {
                    var point = new Point(column * TileSize, line * TileSize);
                    ship.Draw(graphics, point);
                }
            }
        }

        // Hit places
        for (int column = 0; column < board.GetLength(0); column++)
        {
            for (int line = 0; line < board.GetLength(1); line++)
            {
                if (board[column, line])
                {
                    var point = new Point(column * TileSize, line * TileSize);
                    var rect = new Rectangle(point, new Size(TileSize, TileSize));
                    graphics.DrawString("❌", DefaultFont, Brushes.White, rect, new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    });
                }

            }
        }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        try
        {
            game.Save();
            MessageBox.Show(
                "¡El juego fue guardado con éxito!",
                "¡Éxito!",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information
            );
        }
        catch
        {
            MessageBox.Show(
                "Ooops! Algo malo pasó y no pudimos guardar tu el juego!",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }


    }
}

class ShipGameObject : IGameObject
{
    private readonly Ship Ship;
    private readonly Sprite ShipSprite;
    private readonly Sprite FireSprite;

    public ShipGameObject(Ship ship)
    {
        Ship = ship;

        var image = ship.Length switch
        {
            1 => Resources.ship_s,
            2 => Resources.ship_m,
            3 => Resources.ship_l,
            _ => throw new NotImplementedException($"No sprites available for a ship with length {ship.Length}."),
        };

        ShipSprite = new Sprite(image);

        FireSprite = new Sprite(
            Resources.fire,
            new()
            {
                ["small"] = new SpriteAnimation(6, [196, 197, 198, 199]),
                ["big"] = new SpriteAnimation(6, [131, 132, 133, 134]),
            },
            "big",
            new Size(32, 32)
        );
    }

    public void Draw(Graphics graphics, Point point)
    {
        if (Ship.Orientation == ShipOrientation.Horizontal && Ship.Length > 1)
        {
            var state = graphics.Save();

            var width = Ship.Length * GameForm.TileSize;
            var height = GameForm.TileSize;
            var centerX = point.X + (width / 2f);
            var centerY = point.Y + (height / 2f);

            graphics.TranslateTransform(centerX, centerY);
            graphics.RotateTransform(90);

            var offsetPoint = new Point(-ShipSprite.SpriteSheet.Width / 2, -ShipSprite.SpriteSheet.Height / 2);
            graphics.DrawSprite(ShipSprite, offsetPoint);

            graphics.Restore(state);
        }
        else
        {
            graphics.DrawSprite(ShipSprite, point);
        }

        point.Offset(0, -16);
        graphics.DrawSprite(FireSprite, point);
    }

    public void Tick()
    {
        FireSprite.Tick();
    }
}

class WaterTile : IGameObject
{
    private Sprite[,] sprites;
    private static List<int> Variant1 => [33, 34, 35];
    private static List<int> Variant2 => [34, 35, 33];
    private static List<int> Variant3 => [35, 33, 34];
    private static readonly List<List<int>> Variants = [Variant1, Variant2, Variant3];

    public WaterTile()
    {
        sprites = new Sprite[2, 2];
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                var variantIndex = Random.Shared.Next(3);
                sprites[i, j] = new Sprite(
                    Resources.water,
                    new()
                    {
                        ["idle"] = new SpriteAnimation(60, Variants[variantIndex]),
                    },
                    "idle",
                    new Size(16, 16)
                );
            }
        }
    }

    public void Draw(Graphics graphics, Point point)
    {
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                var offsetPoint = new Point(point.X + i * GameForm.TileSize / 2, point.Y + j * GameForm.TileSize / 2);
                graphics.DrawSprite(sprites[i, j], offsetPoint);
            }
        }
    }

    public void Tick()
    {
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                sprites[i, j].Tick();
            }
        }
    }
}

class MockGame : IGame
{

    public bool[,] EnemyBoard { get; private set; }

    public bool[,] PlayerBoard { get; private set; }

    public Dictionary<Point, Ship> PlayerShips { get; } = [];

    public Dictionary<Point, Ship> EnemyShips { get; } = [];

    public Size BoardSize => new Size(8, 8);

    public GameState State { get; } = GameState.InGame;

    public void Save() => Console.WriteLine("Game saved!");

    public Point? AttackCell(Point position)
    {
        try
        {
            EnemyBoard[position.X, position.Y] = true;
        }
        catch
        {
            return null;
        }

        Point resultingAttack;

        do
        {
            resultingAttack = new(Random.Shared.Next(BoardSize.Width), Random.Shared.Next(BoardSize.Width));
        } while (PlayerBoard[resultingAttack.X, resultingAttack.Y]);

        PlayerBoard[resultingAttack.X, resultingAttack.Y] = true;

        return resultingAttack;
    }

    public MockGame(int width, int height)
    {
        EnemyBoard = new bool[width, height];
        PlayerBoard = new bool[width, height];

        for (int i = 0; i < 4; i++)
        {
            var x = Random.Shared.Next(0, width);
            var y = Random.Shared.Next(0, height);
            var point = new Point(x, y);

            PlayerShips[point] = CreateRandomShip();
        }

        for (int i = 0; i < 4; i++)
        {
            var x = Random.Shared.Next(0, width);
            var y = Random.Shared.Next(0, height);
            var point = new Point(x, y);

            EnemyShips[point] = CreateRandomShip();
        }
    }

    static Ship CreateRandomShip()
    {
        var length = Random.Shared.Next(1, 4);
        var orientation = (ShipOrientation)Random.Shared.Next(2);

        return new Ship(length, orientation);
    }
}
