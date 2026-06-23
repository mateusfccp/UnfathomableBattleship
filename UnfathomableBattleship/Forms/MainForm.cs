using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Forms;

public partial class MainForm : Form
{
    private IAuthenticationService AuthenticationService { get; }
    public MainForm(IAuthenticationService authService)
    {
        InitializeComponent();
        AuthenticationService = authService;
        SwitchForm(new LoginForm(AuthenticationService));
    }

    public void SwitchForm(Form subForm)
    {
        foreach (Control control in contentPanel.Controls)
        {
            control.Tag = null;
            contentPanel.Controls.Remove(control);
            control.Dispose();
        }
        
        subForm.TopLevel = false;
        subForm.FormBorderStyle = FormBorderStyle.None;
        subForm.Dock = DockStyle.Fill;
        subForm.Tag = this;

        contentPanel.Controls.Add(subForm);

        subForm.Show();
    }
}
