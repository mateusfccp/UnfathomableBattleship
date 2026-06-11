using UnfathomableBattleship.Forms;

namespace UnfathomableBattleship;

public partial class LoginForm : Form
{
    MainForm? MainForm => Tag as MainForm;

    public LoginForm()
    {
        InitializeComponent();
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
        MainForm?.SwitchForm(new MainMenuForm());
    }
}
