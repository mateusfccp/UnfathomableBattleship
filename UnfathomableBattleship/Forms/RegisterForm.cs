using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Exceptions;

namespace UnfathomableBattleship.Forms
{
    public partial class RegisterForm : Form
    {
        private IAuthenticationService _authenticationService;
        public RegisterForm(IAuthenticationService authService)
        {
            InitializeComponent();
            MyInitializeComponet();
            _authenticationService = authService;
            
        }
        private void MyInitializeComponet()
        {
            
            lbTitle.Location = new Point(ClientSize.Height/2,10);
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
                _authenticationService.CreateUser(tbUser.Text.Trim(), tbPassword.Text);
                MessageBox.Show($"Tu cuenta ha sido creda exitosamente {tbUser.Text.Trim()}!", "Registro completado", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
