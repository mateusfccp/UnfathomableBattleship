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
            tableLayoutPanel1 = new TableLayoutPanel();
            lbTitle = new Label();
            lbUser = new Label();
            tbUser = new TextBox();
            btCancel = new Button();
            lbPassword = new Label();
            lbUserExists = new Label();
            btCreate = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tbPassword = new TextBox();
            checkBoxSeePassword = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(lbTitle, 1, 0);
            tableLayoutPanel1.Controls.Add(lbUser, 1, 1);
            tableLayoutPanel1.Controls.Add(tbUser, 1, 2);
            tableLayoutPanel1.Controls.Add(btCancel, 1, 7);
            tableLayoutPanel1.Controls.Add(lbPassword, 1, 4);
            tableLayoutPanel1.Controls.Add(lbUserExists, 1, 3);
            tableLayoutPanel1.Controls.Add(btCreate, 1, 6);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 5);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(8, 8);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(366, 367);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // lbTitle
            // 
            lbTitle.Anchor = AnchorStyles.None;
            lbTitle.AutoSize = true;
            lbTitle.BackColor = Color.Transparent;
            lbTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbTitle.Location = new Point(78, 41);
            lbTitle.Margin = new Padding(10);
            lbTitle.Name = "lbTitle";
            lbTitle.Size = new Size(207, 38);
            lbTitle.TabIndex = 23;
            lbTitle.Text = "Crea tu cuenta";
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.BackColor = Color.Transparent;
            lbUser.Dock = DockStyle.Left;
            lbUser.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbUser.Location = new Point(36, 120);
            lbUser.Margin = new Padding(0);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(266, 25);
            lbUser.TabIndex = 21;
            lbUser.Text = "Escriba su nombre de usuario:";
            // 
            // tbUser
            // 
            tbUser.BackColor = SystemColors.ActiveCaption;
            tbUser.Dock = DockStyle.Fill;
            tbUser.Location = new Point(39, 148);
            tbUser.MaxLength = 30;
            tbUser.Name = "tbUser";
            tbUser.Size = new Size(286, 27);
            tbUser.TabIndex = 18;
            tbUser.TextChanged += tbUser_TextChanged;
            // 
            // btCancel
            // 
            btCancel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btCancel.BackColor = Color.LightCoral;
            btCancel.Cursor = Cursors.Hand;
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btCancel.Location = new Point(41, 319);
            btCancel.Margin = new Padding(5);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(282, 40);
            btCancel.TabIndex = 24;
            btCancel.Text = "Cancelar";
            btCancel.UseVisualStyleBackColor = false;
            btCancel.Click += btCancel_Click;
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.BackColor = Color.Transparent;
            lbPassword.Dock = DockStyle.Left;
            lbPassword.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbPassword.Location = new Point(36, 198);
            lbPassword.Margin = new Padding(0);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(200, 25);
            lbPassword.TabIndex = 22;
            lbPassword.Text = "Escriba su contraseña:";
            // 
            // lbUserExists
            // 
            lbUserExists.Anchor = AnchorStyles.None;
            lbUserExists.AutoSize = true;
            lbUserExists.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbUserExists.ForeColor = Color.FromArgb(192, 0, 0);
            lbUserExists.Location = new Point(62, 178);
            lbUserExists.Name = "lbUserExists";
            lbUserExists.Size = new Size(239, 20);
            lbUserExists.TabIndex = 25;
            lbUserExists.Text = "Este nombre de usuario ya existe";
            lbUserExists.Visible = false;
            // 
            // btCreate
            // 
            btCreate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btCreate.BackColor = Color.MediumSpringGreen;
            btCreate.Cursor = Cursors.Hand;
            btCreate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btCreate.Location = new Point(41, 267);
            btCreate.Margin = new Padding(5);
            btCreate.Name = "btCreate";
            btCreate.Size = new Size(282, 40);
            btCreate.TabIndex = 20;
            btCreate.Text = "Crear";
            btCreate.UseVisualStyleBackColor = false;
            btCreate.Click += btCreate_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(tbPassword);
            flowLayoutPanel1.Controls.Add(checkBoxSeePassword);
            flowLayoutPanel1.Location = new Point(39, 226);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(286, 33);
            flowLayoutPanel1.TabIndex = 26;
            // 
            // tbPassword
            // 
            tbPassword.BackColor = SystemColors.ActiveCaption;
            tbPassword.Location = new Point(3, 3);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(220, 27);
            tbPassword.TabIndex = 19;
            tbPassword.UseSystemPasswordChar = true;
            // 
            // checkBoxSeePassword
            // 
            checkBoxSeePassword.AutoSize = true;
            checkBoxSeePassword.BackColor = Color.Transparent;
            checkBoxSeePassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            checkBoxSeePassword.Location = new Point(229, 3);
            checkBoxSeePassword.Name = "checkBoxSeePassword";
            checkBoxSeePassword.Size = new Size(53, 24);
            checkBoxSeePassword.TabIndex = 27;
            checkBoxSeePassword.Text = "👁";
            checkBoxSeePassword.UseVisualStyleBackColor = false;
            checkBoxSeePassword.CheckedChanged += checkBoxSeePassword_CheckedChanged;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(382, 383);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RegisterForm";
            Padding = new Padding(8);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Crear Cuenta";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btCancel;
        private Label lbPassword;
        private Label lbUser;
        private TextBox tbUser;
        private Label lbUserExists;
        private Label lbTitle;
        private Button btCreate;
        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox tbPassword;
        private CheckBox checkBoxSeePassword;
    }
}