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

        var lblTitle = new Label
        {
            Text = isVictory ? "¡VICTORIA!" : "DERROTA",
            Font = new Font("Segoe UI", 36, FontStyle.Bold),
            ForeColor = isVictory ? Color.LimeGreen : Color.IndianRed,
            AutoSize = true,
            Location = new Point(250, 100)
        };
        this.Controls.Add(lblTitle);

        var lblStats = new Label
        {
            Text = $"Turnos totales: {totalTurns}\nPrecisión: {accuracy:0.00}%",
            Font = new Font("Segoe UI", 18, FontStyle.Regular),
            ForeColor = Color.White,
            AutoSize = true,
            Location = new Point(280, 250)
        };
        this.Controls.Add(lblStats);

        var btnMenu = new Button
        {
            Text = "Volver al Menú",
            Size = new Size(200, 50),
            Location = new Point(290, 400),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.SeaGreen,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 12, FontStyle.Bold)
        };
        btnMenu.Click += (s, e) => MainForm?.SwitchForm(new MainMenuForm(_gameManager));
        this.Controls.Add(btnMenu);
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