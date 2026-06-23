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
            QuickGameButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // NewGameButton
            // 
            NewGameButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NewGameButton.Location = new Point(326, 281);
            NewGameButton.Name = "NewGameButton";
            NewGameButton.Size = new Size(131, 55);
            NewGameButton.TabIndex = 1;
            NewGameButton.Text = "New Game";
            NewGameButton.UseVisualStyleBackColor = true;
            NewGameButton.Click += NewGameButton_Click;
            // 
            // LoadGameButton
            // 
            LoadGameButton.Location = new Point(326, 403);
            LoadGameButton.Name = "LoadGameButton";
            LoadGameButton.Size = new Size(131, 55);
            LoadGameButton.TabIndex = 2;
            LoadGameButton.Text = "Load Game";
            LoadGameButton.UseVisualStyleBackColor = true;
            // 
            // QuickGameButton
            // 
            QuickGameButton.Location = new Point(326, 342);
            QuickGameButton.Name = "QuickGameButton";
            QuickGameButton.Size = new Size(131, 55);
            QuickGameButton.TabIndex = 3;
            QuickGameButton.Text = "Quick Game";
            QuickGameButton.UseVisualStyleBackColor = true;
            QuickGameButton.Click += QuickGameButton_Click;
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
            tableLayoutPanel1.Controls.Add(QuickGameButton, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Size = new Size(784, 561);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.login_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 561);
            Controls.Add(tableLayoutPanel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "MainMenuForm";
            Text = "MainMenuForm";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button NewGameButton;
        private Button LoadGameButton;
        private Button QuickGameButton;
        private TableLayoutPanel tableLayoutPanel1;
    }
}