using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Forms;

public class GameOverForm : Form
{
    private MainForm? MainForm => Tag as MainForm;
    private readonly IGameManager _gameManager;
    private readonly IGame _game;

    public GameOverForm(IGameManager gameManager, IGame game, bool isVictory)
    {
        _gameManager = gameManager;
        _game = game;

        if (isVictory)
        {
            try { new System.Media.SoundPlayer(@"C:\Windows\Media\tada.wav").Play(); } catch { }
        }
        else
        {
            try { new System.Media.SoundPlayer(@"C:\Windows\Media\chord.wav").Play(); } catch { }
        }

        this.BackColor = Color.FromArgb(26, 26, 36);

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
        double accuracy = totalTurns > 0 ? (successfulHits * 100.0 / totalTurns) : 0;

        var tlp = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            BackColor = Color.Transparent
        };
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));

        var lblTitle = new Label
        {
            Text = isVictory ? "¡VICTORIA!" : "DERROTA",
            Font = new Font("Segoe UI", 48, FontStyle.Bold),
            ForeColor = isVictory ? Color.LimeGreen : Color.IndianRed,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        string descText = $"Jugador: {_game.Description.Username}\n" +
                          $"Dificultad: {_game.Description.Configuration.Mode}\n" +
                          $"Inicio: {_game.Description.StartTime:dd/MM/yyyy HH:mm}\n" +
                          $"Fin: {DateTime.Now:dd/MM/yyyy HH:mm}\n" +
                          $"Tiempo de juego: {_game.Description.ElapsedTime:hh\\:mm\\:ss}\n" +
                          $"Turnos totales: {totalTurns}\n" +
                          $"Precisión: {accuracy:0.00}%";

        var lblStats = new Label
        {
            Text = descText,
            Font = new Font("Segoe UI", 16, FontStyle.Regular),
            ForeColor = Color.White,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        var btnMenu = new Button
        {
            Text = "Volver al Menú",
            Size = new Size(250, 50),
            Anchor = AnchorStyles.None,
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.SeaGreen,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnMenu.Click += (s, e) => MainForm?.SwitchForm(new MainMenuForm(_gameManager));

        tlp.Controls.Add(lblTitle, 0, 0);
        tlp.Controls.Add(lblStats, 0, 1);
        tlp.Controls.Add(btnMenu, 0, 2);

        this.Controls.Add(tlp);
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
}