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

    private void LoadGameButton_Click(object sender, EventArgs e)
    {
        using var loadForm = new LoadGameForm(_gameManager);
        loadForm.Tag = MainForm;
        loadForm.ShowDialog();
    }

    private void LoadGameButton_Click(object sender, EventArgs e)
    {
        MainForm?.SwitchForm(new LoadGameForm(_gameManager));
    }
}
