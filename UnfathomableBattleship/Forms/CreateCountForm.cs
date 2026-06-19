using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Exceptions;

namespace UnfathomableBattleship.Forms
{
    public partial class CreateCountForm : Form
    {
        private IAuthenticationService _authenticationService;
        public CreateCountForm(IAuthenticationService authService)
        {
            InitializeComponent();
            _authenticationService = authService;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUser.Text) || string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos antes de continuar.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _authenticationService.CreateUser(tbUser.Text, tbPassword.Text);
                Hide();
                MessageBox.Show("Your account was succesfully created", "Succesfull Register", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
            }
            catch (RegistrationFailedException)
            {
                lbUserExists.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Critic error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            if (lbUserExists.Visible) lbUserExists.Visible = false;
        }

        private void checkBoxSeePassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = checkBoxSeePassword.Checked ? false : true;
        }
    }
}
