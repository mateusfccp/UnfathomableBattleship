namespace UnfathomableBattleship
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
            usernameTextbox = new TextBox();
            usernameLabel = new Label();
            passwordLabel = new Label();
            passwordTextBox = new TextBox();
            tableLayoutPanel = new TableLayoutPanel();
            loginButton = new Button();
            createAccountButton = new Button();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // usernameTextbox
            // 
            usernameTextbox.Location = new Point(284, 314);
            usernameTextbox.Name = "usernameTextbox";
            usernameTextbox.Size = new Size(200, 23);
            usernameTextbox.TabIndex = 0;
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(284, 296);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(47, 15);
            usernameLabel.TabIndex = 2;
            usernameLabel.Text = "Usuario";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(284, 348);
            passwordLabel.Margin = new Padding(3, 8, 3, 0);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(67, 15);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Contraseña";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(284, 366);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(200, 23);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackgroundImage = Properties.Resources.Background;
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
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(8, 8);
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
            tableLayoutPanel.Size = new Size(768, 545);
            tableLayoutPanel.TabIndex = 5;
            // 
            // loginButton
            // 
            loginButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            loginButton.AutoSize = true;
            loginButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            loginButton.Location = new Point(284, 404);
            loginButton.Margin = new Padding(3, 12, 3, 3);
            loginButton.Name = "loginButton";
            loginButton.Padding = new Padding(2);
            loginButton.Size = new Size(200, 29);
            loginButton.TabIndex = 5;
            loginButton.Text = "Login";
            loginButton.UseVisualStyleBackColor = true;
            loginButton.Click += loginButton_Click;
            // 
            // createAccountButton
            // 
            createAccountButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            createAccountButton.AutoSize = true;
            createAccountButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            createAccountButton.Location = new Point(284, 439);
            createAccountButton.Name = "createAccountButton";
            createAccountButton.Padding = new Padding(2);
            createAccountButton.Size = new Size(200, 29);
            createAccountButton.TabIndex = 6;
            createAccountButton.Text = "Crear cuenta";
            createAccountButton.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "LoginForm";
            Padding = new Padding(8);
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
    }
}
