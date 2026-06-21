using UnfathomableBattleship.Exceptions;
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
        if (string.IsNullOrWhiteSpace(usernameTextbox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
        {
            MessageBox.Show("Por favor, complete ambos espacios");
            return;
        }
        try
        {
            IGameManager gameManager = _authenticationService.Login(usernameTextbox.Text, passwordTextBox.Text);
            MainForm?.SwitchForm(new MainMenuForm(gameManager));
        }
        catch (AuthenticationFailedException)
        {
            MessageBox.Show("Usuario y/o contraseña incorrecta","Error de inicio de sesión", MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        catch(Exception ex)
        {
            MessageBox.Show($"Error inesperado: {ex.Message}", "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }

    private void createAccountButton_Click(object sender, EventArgs e)
    {
        using (var createForm = new RegisterForm(_authenticationService))
        {
            createForm.ShowDialog();
        }
    }
}
