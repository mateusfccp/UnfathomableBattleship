using UnfathomableBattleship.Forms;
using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship;

public partial class LoginForm : Form
{
    private IAuthenticationService _authenticationService;
    MainForm? MainForm => Tag as MainForm;

    public LoginForm(IAuthenticationService authService)
    {
        InitializeComponent();
        _authenticationService = authService;
    }

    private void loginButton_Click(object sender, EventArgs e)
    {
        MainForm?.SwitchForm(new MainMenuForm());
    }

    private void createAccountButton_Click(object sender, EventArgs e)
    {
        using (var createForm = new CreateCountForm(_authenticationService))
        {
            createForm.ShowDialog();
        }
    }
}
