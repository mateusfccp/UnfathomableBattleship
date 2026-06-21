namespace UnfathomableBattleship.Forms
{
    partial class GameForm
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
            playerCanvasPictureBox = new PictureBox();
            mainLayoutPanel = new TableLayoutPanel();
            enemyCanvasPictureBox = new PictureBox();
            infoLayoutPanel = new FlowLayoutPanel();
            playerLabel = new Label();
            playerNameLabel = new Label();
            timerLabel = new Label();
            timerValueLabel = new Label();
            buttonLayoutPanel = new FlowLayoutPanel();
            saveButton = new Button();
            exitButton = new Button();
            ((System.ComponentModel.ISupportInitialize)playerCanvasPictureBox).BeginInit();
            mainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enemyCanvasPictureBox).BeginInit();
            infoLayoutPanel.SuspendLayout();
            buttonLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // playerCanvasPictureBox
            // 
            playerCanvasPictureBox.Anchor = AnchorStyles.None;
            playerCanvasPictureBox.Location = new Point(46, 149);
            playerCanvasPictureBox.Name = "playerCanvasPictureBox";
            playerCanvasPictureBox.Size = new Size(300, 300);
            playerCanvasPictureBox.TabIndex = 0;
            playerCanvasPictureBox.TabStop = false;
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 2;
            mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayoutPanel.Controls.Add(enemyCanvasPictureBox, 1, 1);
            mainLayoutPanel.Controls.Add(infoLayoutPanel, 0, 0);
            mainLayoutPanel.Controls.Add(playerCanvasPictureBox, 0, 1);
            mainLayoutPanel.Controls.Add(buttonLayoutPanel, 1, 0);
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.Location = new Point(0, 0);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 2;
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayoutPanel.Size = new Size(784, 561);
            mainLayoutPanel.TabIndex = 2;
            // 
            // enemyCanvasPictureBox
            // 
            enemyCanvasPictureBox.Anchor = AnchorStyles.None;
            enemyCanvasPictureBox.Location = new Point(438, 149);
            enemyCanvasPictureBox.Name = "enemyCanvasPictureBox";
            enemyCanvasPictureBox.Size = new Size(300, 300);
            enemyCanvasPictureBox.TabIndex = 2;
            enemyCanvasPictureBox.TabStop = false;
            // 
            // infoLayoutPanel
            // 
            infoLayoutPanel.AutoSize = true;
            infoLayoutPanel.Controls.Add(playerLabel);
            infoLayoutPanel.Controls.Add(playerNameLabel);
            infoLayoutPanel.Controls.Add(timerLabel);
            infoLayoutPanel.Controls.Add(timerValueLabel);
            infoLayoutPanel.Location = new Point(3, 3);
            infoLayoutPanel.Name = "infoLayoutPanel";
            infoLayoutPanel.Size = new Size(271, 15);
            infoLayoutPanel.TabIndex = 1;
            // 
            // playerLabel
            // 
            playerLabel.Anchor = AnchorStyles.Left;
            playerLabel.AutoSize = true;
            playerLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playerLabel.Location = new Point(3, 0);
            playerLabel.Name = "playerLabel";
            playerLabel.Size = new Size(57, 15);
            playerLabel.TabIndex = 1;
            playerLabel.Text = "Jugador: ";
            // 
            // playerNameLabel
            // 
            playerNameLabel.Anchor = AnchorStyles.Left;
            playerNameLabel.AutoSize = true;
            playerNameLabel.Location = new Point(66, 0);
            playerNameLabel.Name = "playerNameLabel";
            playerNameLabel.Size = new Size(88, 15);
            playerNameLabel.TabIndex = 2;
            playerNameLabel.Text = "<player name>";
            // 
            // timerLabel
            // 
            timerLabel.Anchor = AnchorStyles.Left;
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            timerLabel.Location = new Point(160, 0);
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new Size(55, 15);
            timerLabel.TabIndex = 3;
            timerLabel.Text = "Tiempo: ";
            // 
            // timerValueLabel
            // 
            timerValueLabel.Anchor = AnchorStyles.Left;
            timerValueLabel.AutoSize = true;
            timerValueLabel.Location = new Point(221, 0);
            timerValueLabel.Name = "timerValueLabel";
            timerValueLabel.Size = new Size(47, 15);
            timerValueLabel.TabIndex = 4;
            timerValueLabel.Text = "<time>";
            // 
            // buttonLayoutPanel
            // 
            buttonLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLayoutPanel.AutoSize = true;
            buttonLayoutPanel.Controls.Add(saveButton);
            buttonLayoutPanel.Controls.Add(exitButton);
            buttonLayoutPanel.Location = new Point(604, 3);
            buttonLayoutPanel.Name = "buttonLayoutPanel";
            buttonLayoutPanel.Size = new Size(177, 31);
            buttonLayoutPanel.TabIndex = 3;
            buttonLayoutPanel.WrapContents = false;
            // 
            // saveButton
            // 
            saveButton.AutoSize = true;
            saveButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            saveButton.Location = new Point(3, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(74, 25);
            saveButton.TabIndex = 0;
            saveButton.Text = "💾 Guardar";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // exitButton
            // 
            exitButton.AutoSize = true;
            exitButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            exitButton.Location = new Point(83, 3);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(91, 25);
            exitButton.TabIndex = 1;
            exitButton.Text = "💀 Abandonar";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(mainLayoutPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GameForm";
            Text = "GameForm";
            ((System.ComponentModel.ISupportInitialize)playerCanvasPictureBox).EndInit();
            mainLayoutPanel.ResumeLayout(false);
            mainLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)enemyCanvasPictureBox).EndInit();
            infoLayoutPanel.ResumeLayout(false);
            infoLayoutPanel.PerformLayout();
            buttonLayoutPanel.ResumeLayout(false);
            buttonLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox playerCanvasPictureBox;
        private TableLayoutPanel mainLayoutPanel;
        private FlowLayoutPanel infoLayoutPanel;
        private Button saveButton;
        private PictureBox enemyCanvasPictureBox;
        private Label playerLabel;
        private Label playerNameLabel;
        private Label timerLabel;
        public Label timerValueLabel;
        private FlowLayoutPanel buttonLayoutPanel;
        private Button exitButton;
    }
}