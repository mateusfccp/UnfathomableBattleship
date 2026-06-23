using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Properties;

namespace UnfathomableBattleship.Forms;

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

    private Label _lblGameMode;
    private Label _lblPlayerShips;
    private Label _lblEnemyShips;

    public const int TileDimension = 32;
    public static readonly Size TileSize = new(TileDimension, TileDimension);

    private TimeSpan ElapsedTime => _game.Description.ElapsedTime + _gameStopwatch.Elapsed;

    public GameForm(IGameManager gameManager, IGame game)
    {
        _gameManager = gameManager;
        _game = game;
        DoubleBuffered = true;

        InitializeComponent();

        this.BackColor = Color.FromArgb(26, 26, 36);
        timerValueLabel.ForeColor = Color.White;
        playerNameLabel.ForeColor = Color.White;
        playerLabel.ForeColor = Color.White;
        timerLabel.ForeColor = Color.White;

        playerNameLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        timerValueLabel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        playerLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
        timerLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);

        saveButton.FlatStyle = FlatStyle.Flat;
        saveButton.BackColor = Color.SeaGreen;
        saveButton.ForeColor = Color.White;
        saveButton.Cursor = Cursors.Hand;

        exitButton.FlatStyle = FlatStyle.Flat;
        exitButton.BackColor = Color.IndianRed;
        exitButton.ForeColor = Color.White;
        exitButton.Cursor = Cursors.Hand;

        _lblGameMode = new Label { ForeColor = Color.LightSkyBlue, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        _lblPlayerShips = new Label { ForeColor = Color.LimeGreen, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        _lblEnemyShips = new Label { ForeColor = Color.IndianRed, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };

        _lblGameMode.Text = $"Modo: {_game.Description.Configuration.Mode}";

        infoLayoutPanel.Controls.Add(_lblGameMode);
        infoLayoutPanel.Controls.Add(_lblPlayerShips);
        infoLayoutPanel.Controls.Add(_lblEnemyShips);

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

        _playerBoard = new Board(_game.Description.Configuration.BoardSize, _game.PlayerShips, _game.PlayerBoard, false);
        _enemyBoard = new Board(_game.Description.Configuration.BoardSize, _game.EnemyShips, _game.EnemyBoard, true);
    }

    private void GameLoop(object? sender, EventArgs eventArgs)
    {
        UpdateGame();
        RenderGame();
    }

    private bool IsHit(Point target, Dictionary<Point, Ship> ships)
    {
        foreach (var kvp in ships)
        {
            var origin = kvp.Key;
            var ship = kvp.Value;
            for (int i = 0; i < ship.Length; i++)
            {
                int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                if (x == target.X && y == target.Y) return true;
            }
        }
        return false;
    }

    private int CountSunkShips(Dictionary<Point, Ship> ships, bool[,] board)
    {
        int sunk = 0;
        foreach (var kvp in ships)
        {
            var origin = kvp.Key;
            var ship = kvp.Value;
            int hits = 0;
            for (int i = 0; i < ship.Length; i++)
            {
                int x = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                int y = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                if (board[x, y]) hits++;
            }
            if (hits == ship.Length) sunk++;
        }
        return sunk;
    }

    private void TriggerEndGameExplosions(Board targetBoard, bool isVictory)
    {
        var rnd = new Random();
        var explosionTimer = new System.Windows.Forms.Timer { Interval = 100 };
        explosionTimer.Tick += (s, e) =>
        {
            int x = rnd.Next(_game.Description.Configuration.BoardSize.Width);
            int y = rnd.Next(_game.Description.Configuration.BoardSize.Height);
            targetBoard.PlayExplosion(new Point(x, y), () => { });
        };
        explosionTimer.Start();

        var delayTimer = new System.Windows.Forms.Timer { Interval = 2000 };
        delayTimer.Tick += (s, e) =>
        {
            explosionTimer.Stop();
            delayTimer.Stop();
            ShowGameOver(isVictory);
        };
        delayTimer.Start();
    }

    private void EnemyCanvasTileClicked(object? sender, Point eventArgs)
    {
        if (_isAnimating) return;
        if (_game.EnemyBoard[eventArgs.X, eventArgs.Y]) return;

        _isAnimating = true;

        if (IsHit(eventArgs, _game.EnemyShips)) System.Media.SystemSounds.Exclamation.Play();
        else System.Media.SystemSounds.Hand.Play();

        _enemyBoard.PlayExplosion(eventArgs, () =>
        {
            var enemyAttack = _game.AttackCell(eventArgs);

            if (CountSunkShips(_game.EnemyShips, _game.EnemyBoard) == _game.EnemyShips.Count)
            {
                TriggerEndGameExplosions(_enemyBoard, true);
                return;
            }

            if (enemyAttack is Point attack)
            {
                if (IsHit(attack, _game.PlayerShips)) System.Media.SystemSounds.Exclamation.Play();
                else System.Media.SystemSounds.Hand.Play();

                _playerBoard.PlayExplosion(attack, () =>
                {
                    if (CountSunkShips(_game.PlayerShips, _game.PlayerBoard) == _game.PlayerShips.Count)
                    {
                        TriggerEndGameExplosions(_playerBoard, false);
                        return;
                    }

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

        int playerRemaining = _game.PlayerShips.Count - CountSunkShips(_game.PlayerShips, _game.PlayerBoard);
        int enemyRemaining = _game.EnemyShips.Count - CountSunkShips(_game.EnemyShips, _game.EnemyBoard);

        _lblPlayerShips.Text = $"Aliados Vivos: {playerRemaining}";
        _lblEnemyShips.Text = $"Enemigos Vivos: {enemyRemaining}";

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

        _playerBoard.Draw(_playerCanvas.Graphics, Point.Empty);
        _enemyBoard.Draw(_enemyCanvas.Graphics, Point.Empty);

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
            ShowGameOver(false);
        }
    }

    private void ShowGameOver(bool isVictory)
    {
        MainForm?.SwitchForm(new GameOverForm(_gameManager, _game, isVictory));
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
            >= 3 => Resources.ship_l,
            _ => throw new NotImplementedException(),
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