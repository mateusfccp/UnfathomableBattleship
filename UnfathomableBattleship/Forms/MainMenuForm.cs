using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Services;

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

    private void NewGameButton_Click(object sender, EventArgs e)
    {
        using var createForm = new GameSettingsForm(_gameManager);
        createForm.Tag = MainForm;
        createForm.ShowDialog();
    }

    private void QuickGameButton_Click(object sender, EventArgs e)
    {
        try
        {
            MainForm?.SwitchForm(new GameForm(_gameManager, _gameManager.QuickGame()));
            this.Hide();
            this.Dispose();
        }
        catch
        {
            MessageBox.Show("No se ha encontrado partida para la partida rápida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
