namespace UnfathomableBattleship;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        SwitchForm(new LoginForm());
    }

    public void SwitchForm(Form subForm)
    {
        foreach (Control control in contentPanel.Controls)
        {
            control.Tag = null;
            contentPanel.Controls.Remove(control);
        }
        
        subForm.TopLevel = false;
        subForm.FormBorderStyle = FormBorderStyle.None;
        subForm.Dock = DockStyle.Fill;
        subForm.Tag = this;

        contentPanel.Controls.Add(subForm);

        subForm.Show();
    }
}
