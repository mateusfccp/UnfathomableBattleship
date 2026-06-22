namespace UnfathomableBattleship.Forms
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            usernameTextbox = new TextBox();
            usernameLabel = new Label();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            tableLayoutPanel = new TableLayoutPanel();
            loginButton = new Button();
            createAccountButton = new Button();
            lbFeedBackUser = new Label();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // usernameTextbox
            // 
            usernameTextbox.BackColor = SystemColors.ActiveCaption;
            usernameTextbox.Location = new Point(325, 409);
            usernameTextbox.Margin = new Padding(15, 5, 15, 5);
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.Size = new Size(228, 27);
            usernameTextbox.TabIndex = 0;
            usernameTextbox.KeyPress += UsernameTextbox_KeyPress;
            // 
            // usernameLabel
            // 
            usernameLabel.Anchor = AnchorStyles.None;
            usernameLabel.AutoSize = true;
            usernameLabel.BackColor = Color.Transparent;
            usernameLabel.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            usernameLabel.ForeColor = Color.WhiteSmoke;
            usernameLabel.Location = new Point(400, 379);
            usernameLabel.Margin = new Padding(10, 15, 10, 0);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(77, 25);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Usuario";
            // 
            // passwordLabel
            // 
            passwordLabel.Anchor = AnchorStyles.None;
            passwordLabel.AutoSize = true;
            passwordLabel.BackColor = Color.Transparent;
            passwordLabel.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            passwordLabel.ForeColor = Color.WhiteSmoke;
            passwordLabel.Location = new Point(385, 456);
            passwordLabel.Margin = new Padding(10, 15, 10, 15);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(108, 25);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Contraseña";
            // 
            // passwordTextBox
            // 
            passwordTextBox.BackColor = SystemColors.ActiveCaption;
            passwordTextBox.Location = new Point(325, 501);
            passwordTextBox.Margin = new Padding(15, 5, 15, 5);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(228, 27);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.Anchor = AnchorStyles.None;
            tableLayoutPanel.BackgroundImage = Properties.Resources.login_background;
            tableLayoutPanel.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(passwordTextBox, 1, 4);
            tableLayoutPanel.Controls.Add(usernameTextbox, 1, 2);
            tableLayoutPanel.Controls.Add(usernameLabel, 1, 1);
            tableLayoutPanel.Controls.Add(passwordLabel, 1, 3);
            tableLayoutPanel.Controls.Add(loginButton, 1, 5);
            tableLayoutPanel.Controls.Add(createAccountButton, 1, 6);
            tableLayoutPanel.Controls.Add(lbFeedBackUser, 2, 2);
            tableLayoutPanel.Location = new Point(9, 11);
            tableLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle());
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.Size = new Size(878, 726);
            tableLayoutPanel.TabIndex = 5;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.None;
            loginButton.AutoSize = true;
            loginButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            loginButton.BackColor = Color.SteelBlue;
            loginButton.Cursor = Cursors.Hand;
            loginButton.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            loginButton.Location = new Point(402, 538);
            loginButton.Margin = new Padding(5);
            loginButton.Name = "loginButton";
            loginButton.Padding = new Padding(2, 3, 2, 3);
            loginButton.Size = new Size(73, 41);
            loginButton.TabIndex = 5;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += LoginButton_Click;
            // 
            // createAccountButton
            // 
            createAccountButton.Anchor = AnchorStyles.None;
            createAccountButton.AutoSize = true;
            createAccountButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            createAccountButton.BackColor = Color.SteelBlue;
            createAccountButton.Cursor = Cursors.Hand;
            createAccountButton.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            createAccountButton.Location = new Point(372, 589);
            createAccountButton.Margin = new Padding(5);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.Padding = new Padding(2, 3, 2, 3);
            createAccountButton.Size = new Size(134, 41);
            createAccountButton.TabIndex = 6;
            createAccountButton.Text = "Crear cuenta";
            createAccountButton.UseVisualStyleBackColor = false;
            createAccountButton.Click += CreateAccountButton_Click;
            // 
            // lbFeedBackUser
            // 
            lbFeedBackUser.AutoSize = true;
            lbFeedBackUser.ForeColor = Color.Red;
            lbFeedBackUser.Location = new Point(571, 404);
            lbFeedBackUser.Name = "lbFeedBackUser";
            lbFeedBackUser.Size = new Size(0, 20);
            lbFeedBackUser.TabIndex = 7;
            lbFeedBackUser.Visible = false;
            // 
            // LoginForm
            // 
            AcceptButton = loginButton;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(896, 748);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "LoginForm";
            Padding = new Padding(9, 11, 9, 11);
            ShowInTaskbar = false;
            Text = "Form1";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox usernameTextbox;
        private Label usernameLabel;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private TableLayoutPanel tableLayoutPanel;
        private Button loginButton;
        private Button createAccountButton;
        private Label lbFeedBackUser;
    }
}
