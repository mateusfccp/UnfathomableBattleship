using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UnfathomableBattleship.Forms;

public partial class MainMenuForm : Form
{
    MainForm? MainForm => Tag as MainForm;

    public MainMenuForm()
    {
        InitializeComponent();
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        MainForm?.SwitchForm(new GameForm());
    }
}

