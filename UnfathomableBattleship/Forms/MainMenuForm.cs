using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

namespace UnfathomableBattleship.Forms;

public partial class MainMenuForm : Form
{
    private MainForm? MainForm => Tag as MainForm;
    private readonly IGameManager _gameManager;

    public MainMenuForm(IGameManager gameManager)
    {
        InitializeComponent();
        _gameManager = gameManager;
    }

    private void Label1_Click(object sender, EventArgs e)
    {
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        var barcos = new List<Ship>
        {
            new Ship(1, ShipOrientation.Horizontal)
        };

        var configuracion = new GameConfiguration(GameMode.FearAndHunger, new Size(10, 10), barcos);
        var partidaReal = _gameManager.NewGame(configuracion);

        MainForm?.SwitchForm(new PreparationForm(_gameManager, configuracion));
    }
}