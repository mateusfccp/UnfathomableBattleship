using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Forms;

public class PreparationForm : Form
{
    private MainForm? MainForm => Tag as MainForm;
    private readonly IGameManager _gameManager;
    private readonly IGame _game;
    private readonly GameConfiguration _config;

    private readonly GameCanvas _canvas;
    private Board _board;
    private PictureBox _pb;

    private List<Ship> _shipsToPlace;
    private ShipOrientation _currentOrientation = ShipOrientation.Horizontal;

    private Button _btnContinue;
    private Label _lblStatus;
    private Label _lblInstructions;

    private System.Windows.Forms.Timer _timer;

    public PreparationForm(IGameManager gameManager, GameConfiguration config)
    {
        _gameManager = gameManager;
        _config = config;

        _game = _gameManager.NewGame(config);
        _shipsToPlace = new List<Ship>(config.Ships);

        this.Text = "Fase de Preparación";
        this.BackColor = Color.FromArgb(26, 26, 36);
        this.KeyPreview = true;

        _lblStatus = new Label
        {
            Text = $"Barcos restantes: {_shipsToPlace.Count}",
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 20)
        };
        this.Controls.Add(_lblStatus);

        _lblInstructions = new Label
        {
            Text = "Rotar: R o Flechas | Quitar: Clic Derecho",
            ForeColor = Color.LightGray,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            AutoSize = true,
            Location = new Point(20, 50)
        };
        this.Controls.Add(_lblInstructions);

        _btnContinue = new Button
        {
            Text = "Comenzar Batalla",
            Enabled = false,
            Size = new Size(150, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.SeaGreen
        };
        _btnContinue.Click += BtnContinue_Click;
        this.Controls.Add(_btnContinue);

        _pb = new PictureBox
        {
            Size = new Size(config.BoardSize.Width * GameForm.TileDimension, config.BoardSize.Height * GameForm.TileDimension),
            Anchor = AnchorStyles.None
        };

        _pb.MouseEnter += (s, e) => _pb.Focus();
        this.Controls.Add(_pb);

        _canvas = new GameCanvas(_pb, config.BoardSize);
        _canvas.TileClicked += OnLeftClick;
        _canvas.RightTileClicked += OnRightClick;

        _board = new Board(config.BoardSize, _game.PlayerShips, _game.PlayerBoard, false);

        _timer = new System.Windows.Forms.Timer { Interval = 16 };
        _timer.Tick += GameLoop;
        _timer.Start();

        this.Resize += PreparationForm_Resize;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        CenterElements();
        _pb.Select();
    }

    private void PreparationForm_Resize(object? sender, EventArgs e)
    {
        CenterElements();
    }

    private void CenterElements()
    {
        _pb.Location = new Point((this.ClientSize.Width - _pb.Width) / 2, (this.ClientSize.Height - _pb.Height) / 2 + 20);
        _btnContinue.Location = new Point(this.ClientSize.Width - _btnContinue.Width - 20, 15);
    }

    private void GameLoop(object? sender, EventArgs e)
    {
        _canvas.Update();
        _board.Tick();
        Render();
    }

    private void Render()
    {
        _canvas.Present();
        _canvas.Graphics.Clear(Color.Black);
        _board.Draw(_canvas.Graphics, Point.Empty);

        if (_shipsToPlace.Count > 0 && _canvas.CursorPosition is Point pos)
        {
            var previewShip = new Ship(_shipsToPlace[0].Length, _currentOrientation);
            DrawGhostShip(_canvas.Graphics, pos, previewShip);
        }
    }

    private void DrawGhostShip(Graphics g, Point pos, Ship ship)
    {
        var shipGO = new ShipGameObject(ship);
        var pixelPos = new Point(pos.X * GameForm.TileDimension, pos.Y * GameForm.TileDimension);
        var isValid = ((Game)_game).IsValidPlacement(pos.X, pos.Y, ship, _game.PlayerShips);

        using var tempBmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        using var tempG = Graphics.FromImage(tempBmp);
        shipGO.Draw(tempG, pixelPos);

        float[][] matrixItems = {
            new float[] {1, 0, 0, 0, 0},
            new float[] {0, 1, 0, 0, 0},
            new float[] {0, 0, 1, 0, 0},
            new float[] {0, 0, 0, 0.6f, 0},
            new float[] {0, 0, 0, 0, 1}
        };
        var colorMatrix = new ColorMatrix(matrixItems);
        var imageAtt = new ImageAttributes();
        imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

        g.DrawImage(tempBmp, new Rectangle(0, 0, tempBmp.Width, tempBmp.Height), 0, 0, tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, imageAtt);

        var width = (ship.Orientation == ShipOrientation.Horizontal ? ship.Length : 1) * GameForm.TileDimension;
        var height = (ship.Orientation == ShipOrientation.Vertical ? ship.Length : 1) * GameForm.TileDimension;
        var color = isValid ? Color.LimeGreen : Color.Red;
        using var pen = new Pen(color, 2);
        g.DrawRectangle(pen, new Rectangle(pixelPos.X, pixelPos.Y, width, height));
    }

    private void OnLeftClick(object? sender, Point pos)
    {
        if (_shipsToPlace.Count == 0) return;

        var shipToPlace = new Ship(_shipsToPlace[0].Length, _currentOrientation);
        if (((Game)_game).PlacePlayerShip(pos, shipToPlace))
        {
            _shipsToPlace.RemoveAt(0);
            UpdateBoard();
        }
    }

    private void OnRightClick(object? sender, Point pos)
    {
        foreach (var kvp in _game.PlayerShips.ToList())
        {
            var origin = kvp.Key;
            var ship = kvp.Value;

            bool hit = false;
            for (int i = 0; i < ship.Length; i++)
            {
                int cx = origin.X + (ship.Orientation == ShipOrientation.Horizontal ? i : 0);
                int cy = origin.Y + (ship.Orientation == ShipOrientation.Vertical ? i : 0);
                if (cx == pos.X && cy == pos.Y) hit = true;
            }

            if (hit)
            {
                ((Game)_game).RemovePlayerShip(origin);
                _shipsToPlace.Add(ship);
                UpdateBoard();
                break;
            }
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.R || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
        {
            _currentOrientation = _currentOrientation == ShipOrientation.Horizontal
                ? ShipOrientation.Vertical
                : ShipOrientation.Horizontal;
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void UpdateBoard()
    {
        _board = new Board(_config.BoardSize, _game.PlayerShips, _game.PlayerBoard, false);
        _lblStatus.Text = $"Barcos restantes: {_shipsToPlace.Count}";
        _btnContinue.Enabled = _shipsToPlace.Count == 0;
        _btnContinue.BackColor = _btnContinue.Enabled ? Color.LimeGreen : Color.SeaGreen;
    }

    private void BtnContinue_Click(object? sender, EventArgs e)
    {
        _timer.Stop();
        MainForm?.SwitchForm(new GameForm(_gameManager, _game));
    }
}