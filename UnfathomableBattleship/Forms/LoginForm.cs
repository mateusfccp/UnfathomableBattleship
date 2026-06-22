using UnfathomableBattleship.Exceptions;
using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Forms;

public partial class LoginForm : Form
{
    private readonly IAuthenticationService _authenticationService;
    MainForm? MainForm => Tag as MainForm;

    public LoginForm(IAuthenticationService authService)
    {
        InitializeComponent();
        _authenticationService = authService;
        tableLayoutPanel.CellPaint += tableLayoutPanel_CellPaint;
    }
    /// <summary>
    /// To pint a semi-transparent black rectangle over the right side of the form, where the login controls are, to improve readability over the background image.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void tableLayoutPanel_CellPaint(object? sender, TableLayoutCellPaintEventArgs e)
    {
        if (e.Column == 1 && e.Row >= 1 && e.Row <= 6)
        {
            using SolidBrush brush = new SolidBrush(Color.FromArgb(180, 0, 0, 0));
            e.Graphics.FillRectangle(brush, e.CellBounds);
        }
    }
    private void LoginButton_Click(object sender, EventArgs e)
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
            MessageBox.Show("Usuario y/o contraseña incorrecta", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error inesperado: {ex.Message}", "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }

    private void CreateAccountButton_Click(object sender, EventArgs e)
    {
        using var createForm = new RegisterForm(_authenticationService);
        createForm.ShowDialog();
    }

    private void UsernameTextbox_KeyPress(object sender, KeyPressEventArgs e)
    {
    }
}
