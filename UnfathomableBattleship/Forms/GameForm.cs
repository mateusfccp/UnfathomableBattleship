using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

public partial class GameForm : Form
{
    private IGame game;
    private System.Windows.Forms.Timer gameTimer;
    private Bitmap canvas;
    private Graphics graphics;
    private HashSet<Keys> pressedKeys = [];
    private Point mousePosition = new();
    private bool isLeftMouseDown;
    private bool wasLeftMouseDown;
    private WaterTile[,] PlayerWaterTiles;
    private WaterTile[,] EnemyWaterTiles;


    public const int TileSize = 32;

    private Point CursorPosition
    {
        get
        {
            var mouseTileX = mousePosition.X / TileSize;
            var mouseTileY = mousePosition.Y / TileSize;
            return new Point(mouseTileX, mouseTileY);
        }
    }

    private bool IsLeftMouseJustPressed => isLeftMouseDown && !wasLeftMouseDown;

    public GameForm(IGame? game = null)
    {
        this.game = game ?? new MockGame(8, 8);

        InitializeComponent();

        gameTimer = new()
        {
            Interval = 16
        };
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

        KeyPreview = true;
        KeyDown += GameForm_KeyDown;
        KeyUp += GameForm_KeyUp;

        canvasPictureBox.MouseMove += PictureBoxCanvas_MouseMove;
        canvasPictureBox.MouseDown += PictureBoxCanvas_MouseDown;
        canvasPictureBox.MouseUp += PictureBoxCanvas_MouseUp;
        canvasPictureBox.Resize += CanvasPictureBox_Resize;

        Select();
        Load += (s, e) => Select();

        // Initialize water tiles
        PlayerWaterTiles = new WaterTile[this.game.BoardSize.Width, this.game.BoardSize.Height];
        EnemyWaterTiles = new WaterTile[this.game.BoardSize.Width, this.game.BoardSize.Height];

        for (var line = 0; line < this.game.BoardSize.Height; line++)
        {
            for (var column = 0; column < this.game.BoardSize.Width; column++)
            {
                PlayerWaterTiles[column, line] = new WaterTile();
                EnemyWaterTiles[column, line] = new WaterTile();
            }
        }
    }

    private void BuildCanvas()
    {
        if (canvasPictureBox.Width <= 0 || canvasPictureBox.Height <= 0) return;

        graphics?.Dispose();
        canvas?.Dispose();

        canvas = new Bitmap(canvasPictureBox.Width, canvasPictureBox.Height);
        graphics = Graphics.FromImage(canvas);
    }

    private void CanvasPictureBox_Resize(object? sender, EventArgs e)
    {
        BuildCanvas();
    }

    private void PictureBoxCanvas_MouseMove(object? sender, MouseEventArgs eventArgs)
    {
        mousePosition = eventArgs.Location;
    }

    private void PictureBoxCanvas_MouseDown(object? sender, MouseEventArgs eventArgs)
    {
        if (eventArgs.Button == MouseButtons.Left)
        {
            isLeftMouseDown = true;
        }
    }

    private void PictureBoxCanvas_MouseUp(object? sender, MouseEventArgs eventArgs)
    {
        if (eventArgs.Button == MouseButtons.Left)
        {
            isLeftMouseDown = false;
        }
    }

    private void GameForm_KeyDown(object? sender, KeyEventArgs eventArgs)
    {
        if (!pressedKeys.Contains(eventArgs.KeyCode))
        {
            pressedKeys.Add(eventArgs.KeyCode);
        }
    }

    private void GameForm_KeyUp(object? sender, KeyEventArgs eventArgs)
    {
        pressedKeys.Remove(eventArgs.KeyCode);
    }

    private void GameLoop(object? sender, EventArgs eventArgs)
    {
        UpdateGame();
        RenderGame();
        canvasPictureBox.Image = canvas;
    }

    private void UpdateGame()
    {
        for (var column = 0; column < game.BoardSize.Width; column++)
        {
            for (var line = 0; line < game.BoardSize.Height; line++)

            {
                PlayerWaterTiles[column, line].Tick();
                EnemyWaterTiles[column, line].Tick();
            }
        }

        if (IsLeftMouseJustPressed)
        {
            game.AttackCell(CursorPosition);
        }

        wasLeftMouseDown = isLeftMouseDown;
    }

    private void RenderGame()
    {
        graphics.Clear(Color.Black);

        // Layer 1: Board
        DrawBoard(Point.Empty, game.BoardSize, game.EnemyBoard, game.EnemyShips, EnemyWaterTiles, Brushes.Red);
        DrawBoard(new Point(0, game.BoardSize.Height * TileSize + TileSize), game.BoardSize, game.PlayerBoard, game.PlayerShips, PlayerWaterTiles, Brushes.LightGreen);

        // Layer 2: Cursor
        using var pen = new Pen(Brushes.Yellow, 2.0f);
        graphics.DrawRectangle(pen, CursorPosition.X * TileSize, CursorPosition.Y * TileSize, TileSize, TileSize);
    }

    private void DrawBoard(Point position, Size size, bool[,] board, Dictionary<Point, Ship> ships, WaterTile[,] waterTiles, Brush borderBrush)
    {
        // Tiles
        for (int column = 0; column < waterTiles.GetLength(0); column++)
        {
            for (int line = 0; line < waterTiles.GetLength(1); line++)
            {
                var tile = waterTiles[column, line];
                var point = new Point(column * TileSize, line * TileSize);
                point.Offset(position);
                tile.Draw(graphics, point);
            }
        }

        // Border
        using var borderPen = new Pen(borderBrush, 1.0f);
        var borderRect = new Rectangle(position, size * TileSize);
        graphics.DrawRectangle(borderPen, borderRect);

        // Interlines
        //for (int i = 0; i < size.Width; i++)
        //{
        //    var p1 = new Point(i * TileSize, 0);
        //    var p2 = new Point(i * TileSize, size.Height * TileSize);
        //    p1.Offset(position);
        //    p2.Offset(position);
        //    graphics.DrawLine(borderPen, p1, p2);
        //}

        //for (int j = 0; j < size.Height; j++)
        //{
        //    var p1 = new Point(0, j * TileSize);
        //    var p2 = new Point(size.Width * TileSize, j * TileSize);
        //    p1.Offset(position);
        //    p2.Offset(position);
        //    graphics.DrawLine(borderPen, p1, p2);
        //}

        // Ships
        int id = 0;
        foreach (var ship in ships)
        {
            var shipPosition = new Point(ship.Key.X * TileSize, ship.Key.Y * TileSize);
            shipPosition.Offset(position);
            DrawShip(shipPosition, id, ship.Value);
            id = id + 1;
        }

        // Hit places
        for (int column = 0; column < board.GetLength(0); column++)
        {
            for (int line = 0; line < board.GetLength(1); line++)
            {
                if (board[column, line])
                {
                    var point = new Point(column * TileSize, line * TileSize);
                    point.Offset(position);
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

    private void DrawShip(Point position, int id, Ship ship)
    {
        var width = ship.Orientation switch
        {
            ShipOrientation.Horizontal => ship.Length * TileSize,
            ShipOrientation.Vertical => TileSize,
        };

        var height = ship.Orientation switch
        {
            ShipOrientation.Horizontal => TileSize,
            ShipOrientation.Vertical => ship.Length * TileSize,
        };
        var rect = new Rectangle(position, new Size(width, height));

        // Background
        graphics.FillRectangle(Brushes.DarkGray, rect);

        // Border
        using var borderPen = new Pen(Brushes.Gray, 2.0f);
        graphics.DrawRectangle(borderPen, rect);

        // ID
        for (int i = 0; i < ship.Length; i++)
        {
            graphics.DrawString(
                id.ToString(),
                DefaultFont,
                Brushes.White,
                rect,
                new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                }
            );
        }
    }
}

class WaterTile
{
    private Sprite[,] sprites;
    private static List<int> Variant1 => [19, 20];
    private static List<int> Variant2 => [20, 19];

    public WaterTile()
    {
        sprites = new Sprite[2, 2];
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                var variant = Random.Shared.Next(2) == 0 ? Variant1 : Variant2;
                sprites[i, j] = new Sprite(
                    Resources.water,
                    new Size(16, 16),
                    new()
                    {
                        ["idle"] = new SpriteAnimation(60, variant),
                    },
                    "idle"
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
