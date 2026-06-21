using UnfathomableBattleship.Interfaces;
namespace UnfathomableBattleship.Forms;

public partial class MainMenuForm : Form
{
    MainForm? MainForm => Tag as MainForm;
    private readonly IGameManager _gameManager;
    public MainMenuForm(IGameManager gameManager)
    {
        InitializeComponent();
        _gameManager = gameManager;
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        MainForm?.SwitchForm(new GameForm(_gameManager));
    }
}

