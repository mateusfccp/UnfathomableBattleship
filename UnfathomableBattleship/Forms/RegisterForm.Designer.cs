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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
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
            tbUser.BackColor = SystemColors.ActiveCaption;
            tbUser.Location = new Point(40, 117);
            tbUser.MaxLength = 30;
            tbUser.Name = "tbUser";
            tbUser.Size = new Size(267, 27);
            tbUser.TabIndex = 0;
            tbUser.TextChanged += tbUser_TextChanged;
            // 
            // tbPassword
            // 
            tbPassword.BackColor = SystemColors.ActiveCaption;
            tbPassword.Location = new Point(40, 212);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(267, 27);
            tbPassword.TabIndex = 1;
            tbPassword.UseSystemPasswordChar = true;
            tbPassword.TextChanged += textBox2_TextChanged;
            // 
            // btCreate
            // 
            btCreate.BackColor = Color.MediumSpringGreen;
            btCreate.Cursor = Cursors.Hand;
            btCreate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btCreate.Location = new Point(181, 291);
            btCreate.Name = "btCreate";
            btCreate.Size = new Size(126, 57);
            btCreate.TabIndex = 2;
            btCreate.Text = "Crear";
            btCreate.UseVisualStyleBackColor = false;
            btCreate.Click += btCreate_Click;
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbUser.Location = new Point(40, 82);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(266, 25);
            lbUser.TabIndex = 3;
            lbUser.Text = "Escriba su nombre de usuario:";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbPassword.Location = new Point(40, 184);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(200, 25);
            lbPassword.TabIndex = 4;
            lbPassword.Text = "Escriba su contraseña:";
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.BackColor = Color.Transparent;
            lbTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(72, 15);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(207, 38);
            lbTitle.TabIndex = 5;
            lbTitle.Text = "Crea tu cuenta";
            // 
            // btCancel
            // 
            btCancel.BackColor = Color.LightCoral;
            btCancel.Cursor = Cursors.Hand;
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btCancel.Location = new Point(40, 291);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(126, 57);
            btCancel.TabIndex = 6;
            btCancel.Text = "Cancelar";
            btCancel.UseVisualStyleBackColor = false;
            btCancel.Click += btCancel_Click;
            // 
            // lbUserExists
            // 
            lbUserExists.AutoSize = true;
            lbUserExists.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbUserExists.ForeColor = Color.FromArgb(192, 0, 0);
            lbUserExists.Location = new Point(40, 147);
            lbUserExists.Name = "lbUserExists";
            lbUserExists.Size = new Size(239, 20);
            lbUserExists.TabIndex = 7;
            lbUserExists.Text = "Este nombre de usuario ya existe";
            lbUserExists.Visible = false;
            // 
            // checkBoxSeePassword
            // 
            checkBoxSeePassword.AutoSize = true;
            checkBoxSeePassword.BackColor = Color.Transparent;
            checkBoxSeePassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkBoxSeePassword.Location = new Point(313, 215);
            checkBoxSeePassword.Name = "checkBoxSeePassword";
            checkBoxSeePassword.Size = new Size(53, 24);
            checkBoxSeePassword.TabIndex = 8;
            checkBoxSeePassword.Text = "👁";
            checkBoxSeePassword.UseVisualStyleBackColor = false;
            checkBoxSeePassword.CheckedChanged += checkBoxSeePassword_CheckedChanged;
            // 
            // RegisterForm
            // 
            AcceptButton = btCreate;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(407, 383);
            Controls.Add(checkBoxSeePassword);
            Controls.Add(lbUserExists);
            Controls.Add(btCancel);
            Controls.Add(lbTitle);
            Controls.Add(lbPassword);
            Controls.Add(lbUser);
            Controls.Add(btCreate);
            Controls.Add(tbPassword);
            Controls.Add(tbUser);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterForm";
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