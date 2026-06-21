namespace UnfathomableBattleship.Forms
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbUser = new TextBox();
            tbPassword = new TextBox();
            btCreate = new Button();
            lbUser = new Label();
            lbPassword = new Label();
            lbTitle = new Label();
            btCancel = new Button();
            lbUserExists = new Label();
            checkBoxSeePassword = new CheckBox();
            SuspendLayout();
            // 
            // tbUser
            // 
            tbUser.Location = new Point(137, 111);
            tbUser.MaxLength = 30;
            tbUser.Name = "tbUser";
            tbUser.Size = new Size(267, 27);
            tbUser.TabIndex = 0;
            tbUser.TextChanged += tbUser_TextChanged;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(137, 227);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(267, 27);
            tbPassword.TabIndex = 1;
            tbPassword.UseSystemPasswordChar = true;
            tbPassword.TextChanged += textBox2_TextChanged;
            // 
            // btCreate
            // 
            btCreate.Location = new Point(278, 285);
            btCreate.Name = "btCreate";
            btCreate.Size = new Size(126, 57);
            btCreate.TabIndex = 2;
            btCreate.Text = "Crear";
            btCreate.UseVisualStyleBackColor = true;
            btCreate.Click += btCreate_Click;
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbUser.Location = new Point(137, 76);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(218, 20);
            lbUser.TabIndex = 3;
            lbUser.Text = "Escriba su nombre de usuario:";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbPassword.Location = new Point(137, 195);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(163, 20);
            lbPassword.TabIndex = 4;
            lbPassword.Text = "Escriba su contraseña:";
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(200, 10);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(150, 28);
            lbTitle.TabIndex = 5;
            lbTitle.Text = "Crea tu cuenta";
            // 
            // btCancel
            // 
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.Location = new Point(137, 285);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(126, 57);
            btCancel.TabIndex = 6;
            btCancel.Text = "Cancelar";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // lbUserExists
            // 
            lbUserExists.AutoSize = true;
            lbUserExists.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbUserExists.ForeColor = Color.Red;
            lbUserExists.Location = new Point(137, 141);
            lbUserExists.Name = "lbUserExists";
            lbUserExists.Size = new Size(214, 20);
            lbUserExists.TabIndex = 7;
            lbUserExists.Text = "This user name already exists";
            lbUserExists.Visible = false;
            // 
            // checkBoxSeePassword
            // 
            checkBoxSeePassword.AutoSize = true;
            checkBoxSeePassword.Location = new Point(419, 230);
            checkBoxSeePassword.Name = "checkBoxSeePassword";
            checkBoxSeePassword.Size = new Size(52, 24);
            checkBoxSeePassword.TabIndex = 8;
            checkBoxSeePassword.Text = "Ver";
            checkBoxSeePassword.UseVisualStyleBackColor = true;
            checkBoxSeePassword.CheckedChanged += checkBoxSeePassword_CheckedChanged;
            // 
            // CreateCountForm
            // 
            AcceptButton = btCreate;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(562, 383);
            Controls.Add(checkBoxSeePassword);
            Controls.Add(lbUserExists);
            Controls.Add(btCancel);
            Controls.Add(lbTitle);
            Controls.Add(lbPassword);
            Controls.Add(lbUser);
            Controls.Add(btCreate);
            Controls.Add(tbPassword);
            Controls.Add(tbUser);
            Name = "CreateCountForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crear Cuenta";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbUser;
        private TextBox tbPassword;
        private Button btCreate;
        private Label lbUser;
        private Label lbPassword;
        private Label lbTitle;
        private Button btCancel;
        private Label lbUserExists;
        private CheckBox checkBoxSeePassword;
    }
}