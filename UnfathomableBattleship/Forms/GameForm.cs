using System.Diagnostics;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

public partial class GameForm : Form
{
    MainForm? MainForm => Tag as MainForm;

    private IGameManager gameManager;
    private IGame game;
    private System.Windows.Forms.Timer gameTimer;
    private Stopwatch gameStopwatch;

    private GameCanvas playerCanvas;
    private GameCanvas enemyCanvas;

    private Board playerBoard;
    private Board enemyBoard;

    public const int TileDimension = 32;
    public static readonly Size TileSize = new(TileDimension, TileDimension);

    private TimeSpan ElapsedTime
    {
        get
        {
            return game.Description.ElapsedTime + gameStopwatch.Elapsed;
        }
    }

    public GameForm(IGameManager gameManager)
    {
        this.gameManager = gameManager;
        game = new MockGame();
        DoubleBuffered = true;

        InitializeComponent();

        gameTimer = new()
        {
            Interval = 16
        };
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

        gameStopwatch = new();
        gameStopwatch.Start();

        playerNameLabel.Text = game.Description.Username;

        playerCanvas = new GameCanvas(playerCanvasPictureBox, game.Description.Configuration.BoardSize);
        enemyCanvas = new GameCanvas(enemyCanvasPictureBox, game.Description.Configuration.BoardSize);

        enemyCanvas.TileClicked += EnemyCanvasTileClicked;

        playerBoard = new Board(game.Description.Configuration.BoardSize, game.PlayerShips, game.PlayerBoard, false);
        enemyBoard = new Board(game.Description.Configuration.BoardSize, game.EnemyShips, game.EnemyBoard, true);
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
        timerValueLabel.Text = $"{ElapsedTime.Hours:00}:{ElapsedTime.Minutes:00}:{ElapsedTime.Seconds:00}";
        playerCanvas.Update();
        enemyCanvas.Update();
        playerBoard.Tick();
        enemyBoard.Tick();
    }

    private void RenderGame()
    {
        playerCanvas.Present();
        enemyCanvas.Present();

        playerCanvas.Graphics.Clear(Color.Black);
        enemyCanvas.Graphics.Clear(Color.Black);

        // Board
        playerBoard.Draw(playerCanvas.Graphics, Point.Empty);
        enemyBoard.Draw(enemyCanvas.Graphics, Point.Empty);

        // Cursor
        using var pen = new Pen(Brushes.Yellow, 2.0f);
        if (enemyCanvas.CursorPosition is Point position)
        {
            var point = new Point(position.X * TileDimension, position.Y * TileDimension);
            var rect = new Rectangle(point, TileSize);
            enemyCanvas.Graphics.DrawRectangle(pen, rect);
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
                "¡Ooops! Algo malo pasó y no pudimos guardar tu el juego!",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }


    }

    private void exitButton_Click(object sender, EventArgs e)
    {

        var result = MessageBox.Show(
            "¿Estás seguro que querés desistir, cobarde?",
            "Abandonar juego",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Yes)
        {
            ShowGameOver();
        }
    }

    private void ShowGameOver() {
        MessageBox.Show(
            "¡Que fracasado, perdiste el juego!",
            "Game Over"
        );

        // TODO: registrar derrota

        MainForm?.SwitchForm(new MainMenuForm(gameManager));
    }
}

class ShipGameObject : IGameObject
{
    public Ship Ship { get; init; }
    private readonly Sprite ShipSprite;
    private readonly Sprite FireSprite;
    private readonly bool[] Destroyed;
    private bool HasDestruction;

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

        Destroyed = new bool[Ship.Length];
        for (int i = 0; i < Ship.Length; i++)
        {
            Destroyed[i] = false;
        }
    }

    public void SetDestroyed(int tile)
    {
        Debug.Assert(tile < Ship.Length);
        Destroyed[tile] = true;
        HasDestruction = true;
    }

    public void Draw(Graphics graphics, Point point)
    {
        if (Ship.Orientation == ShipOrientation.Horizontal && Ship.Length > 1)
        {
            var state = graphics.Save();

            var width = Ship.Length * GameForm.TileDimension;
            var height = GameForm.TileDimension;
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

        // Draw fires
        if (HasDestruction)
        {
            point.Offset(0, -16);

            for (var i = 0; i < Ship.Length; i++)
            {
                if (Destroyed[i])
                {
                    graphics.DrawSprite(FireSprite, point);
                }

                switch (Ship.Orientation)
                {
                    case ShipOrientation.Horizontal:
                        point.Offset(32, 0);
                        break;
                    case ShipOrientation.Vertical:
                        point.Offset(0, 32);
                        break;
                }
            }
        }
    }

    public void Tick()
    {
        FireSprite.Tick();
    }

    public Size Size
    {
        get
        {
            return Ship.Orientation switch
            {
                ShipOrientation.Horizontal => new(Ship.Length, 1),
                ShipOrientation.Vertical => new(1, Ship.Length),
                _ => throw new NotImplementedException(),
            };
        }
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
                        ["idle"] = new SpriteAnimation(40, Variants[variantIndex]),
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
                var offsetPoint = new Point(point.X + i * GameForm.TileDimension / 2, point.Y + j * GameForm.TileDimension / 2);
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

    public Size Size => new(1, 1);
}

class MockGame : IGame
{
    public GameDescription Description
    {
        get
        {
            return new GameDescription(
                0,
                "Joaquín Forni",
                new DateTime(2026, 06, 10),
                new DateTime(2026, 06, 10),
                null, TimeSpan.Zero,
                GameState.InGame,
                new GameConfiguration(
                    GameMode.SinglePlayer,
                    new(8, 8),
                    []
                )
            );
        }
    }

    public bool[,] EnemyBoard { get; private set; }

    public bool[,] PlayerBoard { get; private set; }

    public Dictionary<Point, Ship> PlayerShips { get; } = [];

    public Dictionary<Point, Ship> EnemyShips { get; } = [];

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
            resultingAttack = new(Random.Shared.Next(Description.Configuration.BoardSize.Width), Random.Shared.Next(Description.Configuration.BoardSize.Width));
        } while (PlayerBoard[resultingAttack.X, resultingAttack.Y]);

        PlayerBoard[resultingAttack.X, resultingAttack.Y] = true;

        return resultingAttack;
    }

    public MockGame()
    {
        var width = Description.Configuration.BoardSize.Width;
        var height = Description.Configuration.BoardSize.Height;
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
