namespace UnfathomableBattleship.Forms
{
    partial class MainMenuForm
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
            NewGameButton = new Button();
            LoadGameButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // NewGameButton
            // 
            NewGameButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NewGameButton.Location = new Point(373, 376);
            NewGameButton.Margin = new Padding(3, 4, 3, 4);
            NewGameButton.Name = "NewGameButton";
            NewGameButton.Size = new Size(150, 73);
            NewGameButton.TabIndex = 1;
            NewGameButton.Text = "New Game";
            NewGameButton.UseVisualStyleBackColor = true;
            NewGameButton.Click += NewGameButton_Click;
            // 
            // LoadGameButton
            // 
            LoadGameButton.Location = new Point(373, 538);
            LoadGameButton.Margin = new Padding(3, 4, 3, 4);
            LoadGameButton.Name = "LoadGameButton";
            LoadGameButton.Size = new Size(150, 73);
            LoadGameButton.TabIndex = 2;
            LoadGameButton.Text = "Load Game";
            LoadGameButton.UseVisualStyleBackColor = true;
            LoadGameButton.Click += LoadGameButton_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackgroundImage = Properties.Resources.login_background;
            tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(LoadGameButton, 1, 3);
            tableLayoutPanel1.Controls.Add(NewGameButton, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 133F));
            tableLayoutPanel1.Size = new Size(896, 748);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(896, 748);
            Controls.Add(tableLayoutPanel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainMenuForm";
            Text = "MainMenuForm";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button NewGameButton;
        private Button LoadGameButton;
        private TableLayoutPanel tableLayoutPanel1;
    }
}