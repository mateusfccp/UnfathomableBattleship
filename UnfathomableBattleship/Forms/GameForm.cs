using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

/// <summary>
/// The main form of the game.
/// </summary>
public partial class GameForm : Form
{
    MainForm? MainForm => Tag as MainForm;

    private readonly IGameManager _gameManager;
    private readonly IGame _game;
    private readonly Stopwatch _gameStopwatch;

    private readonly GameCanvas _playerCanvas;
    private readonly GameCanvas _enemyCanvas;

    private readonly Board _playerBoard;
    private readonly Board _enemyBoard;

    private bool _isAnimating;

    public const int TileDimension = 32;
    public static readonly Size TileSize = new(TileDimension, TileDimension);

    private TimeSpan ElapsedTime => _game.Description.ElapsedTime + _gameStopwatch.Elapsed;

    /// <summary>
    /// Creates a new game form.
    /// </summary>
    /// <param name="gameManager">The game manager related to the given game.</param>
    /// <param name="game">The game to be played.</param>
    public GameForm(IGameManager gameManager, IGame game)
    {
        _gameManager = gameManager;
        _game = game;
        DoubleBuffered = true;

        InitializeComponent();

        System.Windows.Forms.Timer gameTimer = new()
        {
            Interval = 16
        };
        gameTimer.Tick += GameLoop;
        gameTimer.Start();

        _gameStopwatch = new Stopwatch();
        _gameStopwatch.Start();

        playerNameLabel.Text = _game.Description.Username;

        _playerCanvas = new GameCanvas(playerCanvasPictureBox, _game.Description.Configuration.BoardSize);
        _enemyCanvas = new GameCanvas(enemyCanvasPictureBox, _game.Description.Configuration.BoardSize);

        _enemyCanvas.TileClicked += EnemyCanvasTileClicked;

        _playerBoard = new Board(_game.Description.Configuration.BoardSize, _game.PlayerShips, _game.PlayerBoard,
            false);
        _enemyBoard = new Board(_game.Description.Configuration.BoardSize, _game.EnemyShips, _game.EnemyBoard, true);
    }

    private void GameLoop(object? sender, EventArgs eventArgs)
    {
        UpdateGame();
        RenderGame();
    }

    private void EnemyCanvasTileClicked(object? sender, Point eventArgs)
    {
        if (_isAnimating) return;
        if (_game.EnemyBoard[eventArgs.X, eventArgs.Y]) return;

        _isAnimating = true;

        _enemyBoard.PlayExplosion(eventArgs, () =>
        {
            var enemyAttack = _game.AttackCell(eventArgs);

            if (enemyAttack is Point attack)
            {
                _playerBoard.PlayExplosion(attack, () =>
                {
                    _isAnimating = false;
                });
            }
            else
            {
                _isAnimating = false;
            }
        });
    }

    private void UpdateGame()
    {
        timerValueLabel.Text = $"{ElapsedTime.Hours:00}:{ElapsedTime.Minutes:00}:{ElapsedTime.Seconds:00}";
        _playerCanvas.Update();
        _enemyCanvas.Update();
        _playerBoard.Tick();
        _enemyBoard.Tick();
    }

    private void RenderGame()
    {
        _playerCanvas.Present();
        _enemyCanvas.Present();

        _playerCanvas.Graphics.Clear(Color.Black);
        _enemyCanvas.Graphics.Clear(Color.Black);

        // Board
        _playerBoard.Draw(_playerCanvas.Graphics, Point.Empty);
        _enemyBoard.Draw(_enemyCanvas.Graphics, Point.Empty);

        // Cursor
        if (!_isAnimating)
        {
            using var pen = new Pen(Brushes.Yellow, 2.0f);

            if (_enemyCanvas.CursorPosition is Point position)
            {
                var point = new Point(position.X * TileDimension, position.Y * TileDimension);
                var rect = new Rectangle(point, TileSize);
                _enemyCanvas.Graphics.DrawRectangle(pen, rect);
            }
        }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        try
        {
            _game.Save();
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

    private void ShowGameOver()
    {
        MessageBox.Show(
            "¡Que fracasado, perdiste el juego!",
            "Game Over"
        );

        // TODO: registrar derrota

        MainForm?.SwitchForm(new MainMenuForm(_gameManager));
    }
}

internal class ShipGameObject : IGameObject
{
    public Ship Ship { get; }
    private readonly Sprite _shipSprite;

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

        _shipSprite = new Sprite(image);
    }

    public void Draw(Graphics graphics, Point point)
    {
        if (Ship.Orientation == ShipOrientation.Horizontal && Ship.Length > 1)
        {
            var state = graphics.Save();

            var width = Ship.Length * GameForm.TileDimension;
            const int height = GameForm.TileDimension;
            var centerX = point.X + width / 2f;
            var centerY = point.Y + height / 2f;

            graphics.TranslateTransform(centerX, centerY);
            graphics.RotateTransform(90);

            var offsetPoint = new Point(-_shipSprite.SpriteSheet.Width / 2, -_shipSprite.SpriteSheet.Height / 2);
            graphics.DrawSprite(_shipSprite, offsetPoint);

            graphics.Restore(state);
        }
        else
        {
            graphics.DrawSprite(_shipSprite, point);
        }
    }

    public void Tick()
    {
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

internal class WaterTile : IGameObject
{
    private readonly Sprite[,] _sprites;
    private static readonly List<int> Variant1 = new() { 33, 34, 35 };
    private static readonly List<int> Variant2 = new() { 34, 35, 33 };
    private static readonly List<int> Variant3 = new() { 35, 33, 34 };
    private static readonly List<List<int>> Variants = new() { Variant1, Variant2, Variant3 };

    public WaterTile()
    {
        _sprites = new Sprite[2, 2];
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                var variantIndex = Random.Shared.Next(3);
                _sprites[i, j] = new Sprite(
                    Resources.water,
                    new Dictionary<string, SpriteAnimation>
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
                var offsetPoint = new Point(point.X + i * GameForm.TileDimension / 2,
                    point.Y + j * GameForm.TileDimension / 2);
                graphics.DrawSprite(_sprites[i, j], offsetPoint);
            }
        }
    }

    public void Tick()
    {
        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                _sprites[i, j].Tick();
            }
        }
    }

    public Size Size => new(1, 1);
}

public class MockGame : IGame
{
        public GameDescription Description =>
        new(
            0,
            "Joaquín Forni",
            new DateTime(2026, 06, 10),
            new DateTime(2026, 06, 10),
            null, TimeSpan.Zero,
            GameState.InGame,
            new GameConfiguration(
                GameMode.SinglePlayer,
                new(8, 8),
                new List<Ship>()
            )
        );

    public bool[,] EnemyBoard { get; }

    public bool[,] PlayerBoard { get; }

    public Dictionary<Point, Ship> PlayerShips { get; } = new Dictionary<Point, Ship>();

    public Dictionary<Point, Ship> EnemyShips { get; } = new Dictionary<Point, Ship>();

    public GameState State => GameState.InGame;

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
            resultingAttack = new(Random.Shared.Next(Description.Configuration.BoardSize.Width),
                Random.Shared.Next(Description.Configuration.BoardSize.Width));
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

        for (var i = 0; i < 4; i++)
        {
            var x = Random.Shared.Next(0, width);
            var y = Random.Shared.Next(0, height);
            var point = new Point(x, y);

            PlayerShips[point] = CreateRandomShip();
        }

        for (var i = 0; i < 4; i++)
        {
            var x = Random.Shared.Next(0, width);
            var y = Random.Shared.Next(0, height);
            var point = new Point(x, y);

            EnemyShips[point] = CreateRandomShip();
        }
    }

    private static Ship CreateRandomShip()
    {
        var length = Random.Shared.Next(1, 4);
        var orientation = (ShipOrientation)Random.Shared.Next(2);

        return new Ship(length, orientation);
    }
}
