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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
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
            giveUpButton = new Button();
            backButton = new Button();
            fleetLabel = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            battleshipPictureBox = new PictureBox();
            battleshipCountLabel = new Label();
            destructorPictureBox = new PictureBox();
            destructorCountLabel = new Label();
            patrolPictureBox = new PictureBox();
            patrolCountLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)playerCanvasPictureBox).BeginInit();
            mainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enemyCanvasPictureBox).BeginInit();
            infoLayoutPanel.SuspendLayout();
            buttonLayoutPanel.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)battleshipPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)destructorPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)patrolPictureBox).BeginInit();
            SuspendLayout();
            // 
            // playerCanvasPictureBox
            // 
            playerCanvasPictureBox.Anchor = AnchorStyles.None;
            playerCanvasPictureBox.Location = new Point(52, 144);
            playerCanvasPictureBox.Margin = new Padding(3, 4, 3, 4);
            playerCanvasPictureBox.Name = "playerCanvasPictureBox";
            playerCanvasPictureBox.Size = new Size(343, 400);
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
            mainLayoutPanel.Controls.Add(fleetLabel, 0, 2);
            mainLayoutPanel.Controls.Add(flowLayoutPanel1, 0, 3);
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.Location = new Point(0, 0);
            mainLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 4;
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 85F));
            mainLayoutPanel.Size = new Size(896, 748);
            mainLayoutPanel.TabIndex = 2;
            // 
            // enemyCanvasPictureBox
            // 
            enemyCanvasPictureBox.Anchor = AnchorStyles.None;
            enemyCanvasPictureBox.Location = new Point(500, 144);
            enemyCanvasPictureBox.Margin = new Padding(3, 4, 3, 4);
            enemyCanvasPictureBox.Name = "enemyCanvasPictureBox";
            enemyCanvasPictureBox.Size = new Size(343, 400);
            enemyCanvasPictureBox.TabIndex = 2;
            enemyCanvasPictureBox.TabStop = false;
            // 
            // infoLayoutPanel
            // 
            infoLayoutPanel.Anchor = AnchorStyles.Left;
            infoLayoutPanel.AutoSize = true;
            infoLayoutPanel.Controls.Add(playerLabel);
            infoLayoutPanel.Controls.Add(playerNameLabel);
            infoLayoutPanel.Controls.Add(timerLabel);
            infoLayoutPanel.Controls.Add(timerValueLabel);
            infoLayoutPanel.Location = new Point(3, 13);
            infoLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            infoLayoutPanel.Name = "infoLayoutPanel";
            infoLayoutPanel.Size = new Size(339, 20);
            infoLayoutPanel.TabIndex = 1;
            // 
            // playerLabel
            // 
            playerLabel.Anchor = AnchorStyles.Left;
            playerLabel.AutoSize = true;
            playerLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playerLabel.Location = new Point(3, 0);
            playerLabel.Name = "playerLabel";
            playerLabel.Size = new Size(75, 20);
            playerLabel.TabIndex = 1;
            playerLabel.Text = "Jugador: ";
            // 
            // playerNameLabel
            // 
            playerNameLabel.Anchor = AnchorStyles.Left;
            playerNameLabel.AutoSize = true;
            playerNameLabel.Location = new Point(84, 0);
            playerNameLabel.Name = "playerNameLabel";
            playerNameLabel.Size = new Size(111, 20);
            playerNameLabel.TabIndex = 2;
            playerNameLabel.Text = "<player name>";
            // 
            // timerLabel
            // 
            timerLabel.Anchor = AnchorStyles.Left;
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            timerLabel.Location = new Point(201, 0);
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new Size(70, 20);
            timerLabel.TabIndex = 3;
            timerLabel.Text = "Tiempo: ";
            // 
            // timerValueLabel
            // 
            timerValueLabel.Anchor = AnchorStyles.Left;
            timerValueLabel.AutoSize = true;
            timerValueLabel.Location = new Point(277, 0);
            timerValueLabel.Name = "timerValueLabel";
            timerValueLabel.Size = new Size(59, 20);
            timerValueLabel.TabIndex = 4;
            timerValueLabel.Text = "<time>";
            // 
            // buttonLayoutPanel
            // 
            buttonLayoutPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLayoutPanel.AutoSize = true;
            buttonLayoutPanel.Controls.Add(saveButton);
            buttonLayoutPanel.Controls.Add(giveUpButton);
            buttonLayoutPanel.Controls.Add(backButton);
            buttonLayoutPanel.Location = new Point(603, 4);
            buttonLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            buttonLayoutPanel.Name = "buttonLayoutPanel";
            buttonLayoutPanel.Size = new Size(290, 38);
            buttonLayoutPanel.TabIndex = 3;
            buttonLayoutPanel.WrapContents = false;
            // 
            // saveButton
            // 
            saveButton.AutoSize = true;
            saveButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            saveButton.BackColor = SystemColors.GradientActiveCaption;
            saveButton.Cursor = Cursors.Hand;
            saveButton.Location = new Point(3, 4);
            saveButton.Margin = new Padding(3, 4, 3, 4);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(97, 30);
            saveButton.TabIndex = 0;
            saveButton.Text = "💾 Guardar";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;
            // 
            // giveUpButton
            // 
            giveUpButton.AutoSize = true;
            giveUpButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            giveUpButton.BackColor = SystemColors.GradientActiveCaption;
            giveUpButton.Cursor = Cursors.Hand;
            giveUpButton.Location = new Point(106, 4);
            giveUpButton.Margin = new Padding(3, 4, 3, 4);
            giveUpButton.Name = "giveUpButton";
            giveUpButton.Size = new Size(101, 30);
            giveUpButton.TabIndex = 1;
            giveUpButton.Text = "💀 Rendirse";
            giveUpButton.UseVisualStyleBackColor = false;
            giveUpButton.Click += giveUpButton_Click;
            // 
            // backButton
            // 
            backButton.AutoSize = true;
            backButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            backButton.BackColor = SystemColors.GradientActiveCaption;
            backButton.Cursor = Cursors.Hand;
            backButton.Location = new Point(213, 4);
            backButton.Margin = new Padding(3, 4, 3, 4);
            backButton.Name = "backButton";
            backButton.Size = new Size(74, 30);
            backButton.TabIndex = 2;
            backButton.Text = "⮌ Volver";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // fleetLabel
            // 
            fleetLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            fleetLabel.AutoSize = true;
            fleetLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            fleetLabel.Location = new Point(3, 643);
            fleetLabel.Name = "fleetLabel";
            fleetLabel.Size = new Size(108, 20);
            fleetLabel.TabIndex = 4;
            fleetLabel.Text = "Flota enemiga";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainLayoutPanel.SetColumnSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Controls.Add(battleshipPictureBox);
            flowLayoutPanel1.Controls.Add(battleshipCountLabel);
            flowLayoutPanel1.Controls.Add(destructorPictureBox);
            flowLayoutPanel1.Controls.Add(destructorCountLabel);
            flowLayoutPanel1.Controls.Add(patrolPictureBox);
            flowLayoutPanel1.Controls.Add(patrolCountLabel);
            flowLayoutPanel1.Location = new Point(3, 667);
            flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(890, 77);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // battleshipPictureBox
            // 
            battleshipPictureBox.Anchor = AnchorStyles.Left;
            battleshipPictureBox.Image = Properties.Resources.ship_l;
            battleshipPictureBox.Location = new Point(3, 4);
            battleshipPictureBox.Margin = new Padding(3, 4, 3, 4);
            battleshipPictureBox.Name = "battleshipPictureBox";
            battleshipPictureBox.Size = new Size(110, 67);
            battleshipPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            battleshipPictureBox.TabIndex = 0;
            battleshipPictureBox.TabStop = false;
            // 
            // battleshipCountLabel
            // 
            battleshipCountLabel.Anchor = AnchorStyles.Left;
            battleshipCountLabel.AutoSize = true;
            battleshipCountLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            battleshipCountLabel.Location = new Point(119, 19);
            battleshipCountLabel.Name = "battleshipCountLabel";
            battleshipCountLabel.Padding = new Padding(0, 0, 18, 0);
            battleshipCountLabel.Size = new Size(50, 37);
            battleshipCountLabel.TabIndex = 1;
            battleshipCountLabel.Text = "0";
            // 
            // destructorPictureBox
            // 
            destructorPictureBox.Anchor = AnchorStyles.Left;
            destructorPictureBox.Image = Properties.Resources.ship_m;
            destructorPictureBox.Location = new Point(175, 4);
            destructorPictureBox.Margin = new Padding(3, 4, 3, 4);
            destructorPictureBox.Name = "destructorPictureBox";
            destructorPictureBox.Size = new Size(73, 67);
            destructorPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            destructorPictureBox.TabIndex = 2;
            destructorPictureBox.TabStop = false;
            // 
            // destructorCountLabel
            // 
            destructorCountLabel.Anchor = AnchorStyles.Left;
            destructorCountLabel.AutoSize = true;
            destructorCountLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            destructorCountLabel.Location = new Point(254, 19);
            destructorCountLabel.Name = "destructorCountLabel";
            destructorCountLabel.Padding = new Padding(0, 0, 18, 0);
            destructorCountLabel.Size = new Size(50, 37);
            destructorCountLabel.TabIndex = 3;
            destructorCountLabel.Text = "0";
            // 
            // patrolPictureBox
            // 
            patrolPictureBox.Anchor = AnchorStyles.Left;
            patrolPictureBox.Image = Properties.Resources.ship_s;
            patrolPictureBox.Location = new Point(310, 4);
            patrolPictureBox.Margin = new Padding(3, 4, 3, 4);
            patrolPictureBox.Name = "patrolPictureBox";
            patrolPictureBox.Size = new Size(37, 67);
            patrolPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            patrolPictureBox.TabIndex = 4;
            patrolPictureBox.TabStop = false;
            // 
            // patrolCountLabel
            // 
            patrolCountLabel.Anchor = AnchorStyles.Left;
            patrolCountLabel.AutoSize = true;
            patrolCountLabel.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            patrolCountLabel.Location = new Point(353, 19);
            patrolCountLabel.Name = "patrolCountLabel";
            patrolCountLabel.Padding = new Padding(0, 0, 18, 0);
            patrolCountLabel.Size = new Size(50, 37);
            patrolCountLabel.TabIndex = 5;
            patrolCountLabel.Text = "0";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(896, 748);
            Controls.Add(mainLayoutPanel);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
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
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)battleshipPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)destructorPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)patrolPictureBox).EndInit();
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
        private Button giveUpButton;
        private Label fleetLabel;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox battleshipPictureBox;
        private Label battleshipCountLabel;
        private PictureBox destructorPictureBox;
        private Label destructorCountLabel;
        private PictureBox patrolPictureBox;
        private Label patrolCountLabel;
        private Button backButton;
    }
}