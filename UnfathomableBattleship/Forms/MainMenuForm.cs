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
        MainForm?.SwitchForm(new GameForm(_gameManager, new MockGame()));
    }
}

