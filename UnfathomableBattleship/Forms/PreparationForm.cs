using System.Drawing.Imaging;
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

    private List<Ship> _shipsToPlace;
    private ShipOrientation _currentOrientation = ShipOrientation.Horizontal;

    private TableLayoutPanel mainLayoutPanel;
    private PictureBox canvasPictureBox;
    private FlowLayoutPanel buttonFlowLayout;
    private Button startButton;
    private Button backButton;
    private FlowLayoutPanel instructionsFlowLayout;
    private Label shipCountLabel;
    private Label instructionsLabel;
    private System.Windows.Forms.Timer _timer;

    public PreparationForm(IGameManager gameManager, GameConfiguration config)
    {
        _gameManager = gameManager;
        _config = config;

        InitializeComponent();

        _game = _gameManager.NewGame(config);
        _shipsToPlace = [.. config.Ships];

        Text = "Fase de Preparación";
        KeyPreview = true;

        canvasPictureBox.MouseEnter += (s, e) => canvasPictureBox.Focus();

        _canvas = new GameCanvas(canvasPictureBox, config.BoardSize);
        _canvas.TileClicked += OnLeftClick;
        _canvas.RightTileClicked += OnRightClick;

        _board = new Board(config.BoardSize, _game.PlayerShips, _game.PlayerBoard, false);

        _timer = new System.Windows.Forms.Timer { Interval = 16 };
        _timer.Tick += GameLoop;
        _timer.Start();

        UpdateBoard();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        canvasPictureBox.Select();
    }

    private void GameLoop(object? sender, EventArgs e)
    {
        _canvas.Update();
        _board.Tick();
        Render();
    }

    private void Render()   // Renderizado del barco fantasma.
    {
        _canvas.Present();
        _canvas.Graphics.Clear(Color.Black);
        _board.Draw(_canvas.Graphics, Point.Empty);

        if (_shipsToPlace.Count > 0 && _canvas.CursorPosition is Point pos)
        {
            var previewShip = new Ship(_shipsToPlace[0].Length, _currentOrientation);
            DrawGhostShip(_canvas.Graphics, pos, previewShip);  //Se le pasa la posicion del mouse.
        }
    }

    private void DrawGhostShip(Graphics graphics, Point pos, Ship ship) // Logica del renderizado del barco fantasma.
    {
        var shipGO = new ShipGameObject(ship);
        var pixelPos = new Point(pos.X * GameForm.TileDimension, pos.Y * GameForm.TileDimension);
        var isValid = ((Game)_game).IsValidPlacement(pos.X, pos.Y, ship, _game.PlayerShips);

        using var tempBmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height); 
        using var tempG = Graphics.FromImage(tempBmp);
        shipGO.Draw(tempG, pixelPos); 

        float[][] matrixItems = {   // Esta matriz define la opacidad del barco fantasma
            [1, 0, 0, 0, 0],        // Se basa funcionando como un vector R, G, B, A, W donde A representa opacidad
            [0, 1, 0, 0, 0],        // En C# el ColorMatrix funciona haciendo una multiplicacion de matrices sobre cada pixel de la imagen original.
            [0, 0, 1, 0, 0],       // Es como aplicarle filtros a la imagen en base a un preset creado desde antes.
            [0, 0, 0, 0.6f, 0],
            [0, 0, 0, 0, 1]
        };
        var colorMatrix = new ColorMatrix(matrixItems); 
        var imageAtt = new ImageAttributes();
        imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap); // Se definen los atributos de la imagen en base a la matriz que generamos antes.

        graphics.DrawImage(tempBmp, new Rectangle(0, 0, tempBmp.Width, tempBmp.Height), 0, 0, tempBmp.Width, tempBmp.Height, GraphicsUnit.Pixel, imageAtt); // Dibujo de la imagen del barco fantasma.

        var width = (ship.Orientation == ShipOrientation.Horizontal ? ship.Length : 1) * GameForm.TileDimension;
        var height = (ship.Orientation == ShipOrientation.Vertical ? ship.Length : 1) * GameForm.TileDimension;
        var color = isValid ? Color.LimeGreen : Color.Red;
        using var pen = new Pen(color, 2);
        graphics.DrawRectangle(pen, new Rectangle(pixelPos.X, pixelPos.Y, width, height)); // Dibujo del rectangulo de posicion correcta.
    }

    private void OnLeftClick(object? sender, Point pos)     // Logica para añadir barcos con click izquierdo.
    {
        if (_shipsToPlace.Count == 0) return;

        var shipToPlace = new Ship(_shipsToPlace[0].Length, _currentOrientation);
        if (((Game)_game).PlacePlayerShip(pos, shipToPlace))
        {
            _shipsToPlace.RemoveAt(0);
            UpdateBoard();
        }
    }

    private void OnRightClick(object? sender, Point pos)    // Logica para remover barcos con click derecho.
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

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)    // Logica para rotar barcos.
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

    private void UpdateBoard()      // Actualizar tablero al colocar barcos.
    {
        _board = new Board(_config.BoardSize, _game.PlayerShips, _game.PlayerBoard, false);
        shipCountLabel.Text = $"Barcos restantes: {_shipsToPlace.Count}";
        startButton.Enabled = _shipsToPlace.Count == 0;
    }

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreparationForm));
        mainLayoutPanel = new TableLayoutPanel();
        canvasPictureBox = new PictureBox();
        buttonFlowLayout = new FlowLayoutPanel();
        startButton = new Button();
        backButton = new Button();
        instructionsFlowLayout = new FlowLayoutPanel();
        shipCountLabel = new Label();
        instructionsLabel = new Label();
        mainLayoutPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)canvasPictureBox).BeginInit();
        buttonFlowLayout.SuspendLayout();
        instructionsFlowLayout.SuspendLayout();
        SuspendLayout();
        // 
        // mainLayoutPanel
        // 
        mainLayoutPanel.ColumnCount = 2;
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        mainLayoutPanel.Controls.Add(canvasPictureBox, 0, 1);
        mainLayoutPanel.Controls.Add(buttonFlowLayout, 1, 0);
        mainLayoutPanel.Controls.Add(instructionsFlowLayout, 0, 0);
        mainLayoutPanel.Dock = DockStyle.Fill;
        mainLayoutPanel.Location = new Point(0, 0);
        mainLayoutPanel.Name = "mainLayoutPanel";
        mainLayoutPanel.RowCount = 2;
        mainLayoutPanel.RowStyles.Add(new RowStyle());
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainLayoutPanel.Size = new Size(768, 544);
        mainLayoutPanel.TabIndex = 0;
        // 
        // canvasPictureBox
        // 
        canvasPictureBox.Anchor = AnchorStyles.None;
        canvasPictureBox.BackColor = SystemColors.ActiveBorder;
        mainLayoutPanel.SetColumnSpan(canvasPictureBox, 2);
        canvasPictureBox.Location = new Point(224, 135);
        canvasPictureBox.Name = "canvasPictureBox";
        canvasPictureBox.Size = new Size(320, 320);
        canvasPictureBox.TabIndex = 0;
        canvasPictureBox.TabStop = false;
        // 
        // buttonFlowLayout
        // 
        buttonFlowLayout.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        buttonFlowLayout.AutoSize = true;
        buttonFlowLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        buttonFlowLayout.Controls.Add(startButton);
        buttonFlowLayout.Controls.Add(backButton);
        buttonFlowLayout.Location = new Point(518, 3);
        buttonFlowLayout.Name = "buttonFlowLayout";
        buttonFlowLayout.Size = new Size(247, 40);
        buttonFlowLayout.TabIndex = 1;
        // 
        // startButton
        // 
        startButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        startButton.AutoSize = true;
        startButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        startButton.BackColor = SystemColors.GradientActiveCaption;
        startButton.Cursor = Cursors.Hand;
        startButton.Enabled = false;
        startButton.Location = new Point(3, 3);
        startButton.Name = "startButton";
        startButton.Size = new Size(161, 30);
        startButton.TabIndex = 0;
        startButton.Text = "⚔️ Comenzar batalla";
        startButton.UseVisualStyleBackColor = false;
        startButton.Click += startButton_Click;
        // 
        // backButton
        // 
        backButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        backButton.AutoSize = true;
        backButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        backButton.BackColor = SystemColors.GradientActiveCaption;
        backButton.Cursor = Cursors.Hand;
        backButton.Location = new Point(170, 3);
        backButton.Name = "backButton";
        backButton.Size = new Size(74, 30);
        backButton.TabIndex = 1;
        backButton.Text = "⮌ Volver";
        backButton.UseVisualStyleBackColor = false;
        backButton.Click += backButton_Click;
        // 
        // instructionsFlowLayout
        // 
        instructionsFlowLayout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        instructionsFlowLayout.AutoSize = true;
        instructionsFlowLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        instructionsFlowLayout.Controls.Add(shipCountLabel);
        instructionsFlowLayout.Controls.Add(instructionsLabel);
        instructionsFlowLayout.FlowDirection = FlowDirection.TopDown;
        instructionsFlowLayout.Location = new Point(3, 3);
        instructionsFlowLayout.Name = "instructionsFlowLayout";
        instructionsFlowLayout.Size = new Size(378, 40);
        instructionsFlowLayout.TabIndex = 2;
        // 
        // shipCountLabel
        // 
        shipCountLabel.AutoSize = true;
        shipCountLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        shipCountLabel.Location = new Point(3, 0);
        shipCountLabel.Name = "shipCountLabel";
        shipCountLabel.Size = new Size(195, 20);
        shipCountLabel.TabIndex = 0;
        shipCountLabel.Text = "Barcos restantes: <count>";
        // 
        // instructionsLabel
        // 
        instructionsLabel.AutoSize = true;
        instructionsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        instructionsLabel.Location = new Point(3, 20);
        instructionsLabel.Name = "instructionsLabel";
        instructionsLabel.Size = new Size(287, 20);
        instructionsLabel.TabIndex = 1;
        instructionsLabel.Text = "Rotar: R o Flechas | Quitar: Clic Derecho";
        // 
        // PreparationForm
        // 
        BackColor = Color.SteelBlue;
        ClientSize = new Size(768, 544);
        Controls.Add(mainLayoutPanel);
        FormBorderStyle = FormBorderStyle.None;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "PreparationForm";
        mainLayoutPanel.ResumeLayout(false);
        mainLayoutPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)canvasPictureBox).EndInit();
        buttonFlowLayout.ResumeLayout(false);
        buttonFlowLayout.PerformLayout();
        instructionsFlowLayout.ResumeLayout(false);
        instructionsFlowLayout.PerformLayout();
        ResumeLayout(false);

    }

    private void startButton_Click(object sender, EventArgs e)
    {
        _timer.Stop();
        MainForm?.SwitchForm(new GameForm(_gameManager, _game));
    }

    private void backButton_Click(object sender, EventArgs e)
    {
        _timer.Stop();
        MainForm?.SwitchForm(new MainMenuForm(_gameManager));
    }
}