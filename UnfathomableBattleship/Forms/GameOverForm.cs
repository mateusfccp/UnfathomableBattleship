using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Forms;

public class GameOverForm : Form
{
    private MainForm? MainForm => Tag as MainForm;
    private readonly IGameManager _gameManager;
    private TableLayoutPanel mainLayoutPanel;
    private Label resultLabel;
    private Button button;
    private Label statusLabel;
    private Label statusResultLabel;
    private Label label1;
    private readonly IGame _game;

    public GameOverForm(IGameManager gameManager, IGame game, bool isVictory)
    {
        _gameManager = gameManager;
        _game = game;

        InitializeComponent();

        if (isVictory)
        {
            try { new System.Media.SoundPlayer(@"C:\Windows\Media\tada.wav").Play(); } catch { }
        }
        else
        {
            try { new System.Media.SoundPlayer(@"C:\Windows\Media\chord.wav").Play(); } catch { }
        }

        int totalTurns = 0;
        int successfulHits = 0;

        for (int x = 0; x < _game.Description.Configuration.BoardSize.Width; x++)
        {
            for (int y = 0; y < _game.Description.Configuration.BoardSize.Height; y++)
            {
                if (_game.EnemyBoard[x, y])
                {
                    totalTurns++;
                    if (IsHit(new Point(x, y), _game.EnemyShips)) successfulHits++;
                }
            }
        }

        var accuracy = totalTurns > 0 ? (successfulHits * 100.0 / totalTurns) : 0;

        resultLabel.Text = isVictory ? "¡VICTORIA!" : "DERROTA";
        resultLabel.ForeColor = isVictory ? Color.LimeGreen : Color.IndianRed;

        statusLabel.Text = $"Jugador:\n" +
                           $"Dificultad:\n" +
                           $"Inicio:\n" +
                           $"Fin:\n" +
                           $"Tiempo de juego:\n" +
                           $"Turnos totales:\n" +
                           $"Precisión:";

        statusResultLabel.Text = $"{_game.Description.Username}\n" +
                           $"{_game.Description.Configuration.Mode}\n" +
                           $"{_game.Description.StartTime:dd/MM/yyyy HH:mm}\n" +
                           $"{DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                           $"{_game.Description.ElapsedTime:hh\\:mm\\:ss}\n" +
                           $"{totalTurns}\n" +
                           $"{accuracy:0.00}%";
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

    private void InitializeComponent()
    {
        mainLayoutPanel = new TableLayoutPanel();
        statusResultLabel = new Label();
        label1 = new Label();
        resultLabel = new Label();
        button = new Button();
        statusLabel = new Label();
        mainLayoutPanel.SuspendLayout();
        SuspendLayout();
        // 
        // mainLayoutPanel
        // 
        mainLayoutPanel.ColumnCount = 3;
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8F));
        mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        mainLayoutPanel.Controls.Add(statusResultLabel, 2, 1);
        mainLayoutPanel.Controls.Add(label1, 1, 1);
        mainLayoutPanel.Controls.Add(resultLabel, 0, 0);
        mainLayoutPanel.Controls.Add(button, 0, 2);
        mainLayoutPanel.Controls.Add(statusLabel, 0, 1);
        mainLayoutPanel.Dock = DockStyle.Fill;
        mainLayoutPanel.Location = new Point(0, 0);
        mainLayoutPanel.Name = "mainLayoutPanel";
        mainLayoutPanel.Padding = new Padding(8);
        mainLayoutPanel.RowCount = 3;
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
        mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        mainLayoutPanel.Size = new Size(284, 261);
        mainLayoutPanel.TabIndex = 0;
        // 
        // statusResultLabel
        // 
        statusResultLabel.Anchor = AnchorStyles.Left;
        statusResultLabel.AutoSize = true;
        statusResultLabel.Location = new Point(149, 78);
        statusResultLabel.Name = "statusResultLabel";
        statusResultLabel.Size = new Size(54, 105);
        statusResultLabel.TabIndex = 4;
        statusResultLabel.Text = "<status>\r\n<status>\r\n<status>\r\n<status>\r\n<status>\r\n<status>\r\n<status>";
        statusResultLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.None;
        label1.AutoSize = true;
        label1.Location = new Point(142, 123);
        label1.Name = "label1";
        label1.Size = new Size(0, 15);
        label1.TabIndex = 3;
        // 
        // resultLabel
        // 
        resultLabel.Anchor = AnchorStyles.Top;
        resultLabel.AutoSize = true;
        mainLayoutPanel.SetColumnSpan(resultLabel, 3);
        resultLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        resultLabel.Location = new Point(53, 8);
        resultLabel.Name = "resultLabel";
        resultLabel.Size = new Size(178, 45);
        resultLabel.TabIndex = 0;
        resultLabel.Text = "<victoria>";
        // 
        // button
        // 
        button.Anchor = AnchorStyles.Bottom;
        mainLayoutPanel.SetColumnSpan(button, 3);
        button.Location = new Point(104, 227);
        button.Name = "button";
        button.Size = new Size(75, 23);
        button.TabIndex = 1;
        button.Text = "Volver";
        button.UseVisualStyleBackColor = true;
        button.Click += button_Click;
        // 
        // statusLabel
        // 
        statusLabel.Anchor = AnchorStyles.Right;
        statusLabel.AutoSize = true;
        statusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        statusLabel.Location = new Point(32, 78);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(103, 105);
        statusLabel.TabIndex = 2;
        statusLabel.Text = "Jugador:\r\nDificultad:\r\nInicio:\r\nFin:\r\nTiempo de juego:\r\nTurnos totales:\r\nPrecisión:";
        statusLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // GameOverForm
        // 
        ClientSize = new Size(284, 261);
        Controls.Add(mainLayoutPanel);
        Name = "GameOverForm";
        mainLayoutPanel.ResumeLayout(false);
        mainLayoutPanel.PerformLayout();
        ResumeLayout(false);

    }

    private void button_Click(object sender, EventArgs e)
    {
        Dispose();
    }
}