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
            menuLayoutPanel = new FlowLayoutPanel();
            saveButton = new Button();
            ((System.ComponentModel.ISupportInitialize)playerCanvasPictureBox).BeginInit();
            mainLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)enemyCanvasPictureBox).BeginInit();
            menuLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // playerCanvasPictureBox
            // 
            playerCanvasPictureBox.Anchor = AnchorStyles.None;
            playerCanvasPictureBox.Location = new Point(46, 148);
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
            mainLayoutPanel.Controls.Add(menuLayoutPanel, 0, 0);
            mainLayoutPanel.Controls.Add(playerCanvasPictureBox, 0, 1);
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
            enemyCanvasPictureBox.Location = new Point(438, 148);
            enemyCanvasPictureBox.Name = "enemyCanvasPictureBox";
            enemyCanvasPictureBox.Size = new Size(300, 300);
            enemyCanvasPictureBox.TabIndex = 2;
            enemyCanvasPictureBox.TabStop = false;
            // 
            // menuLayoutPanel
            // 
            menuLayoutPanel.AutoSize = true;
            mainLayoutPanel.SetColumnSpan(menuLayoutPanel, 2);
            menuLayoutPanel.Controls.Add(saveButton);
            menuLayoutPanel.Location = new Point(3, 3);
            menuLayoutPanel.Name = "menuLayoutPanel";
            menuLayoutPanel.Size = new Size(81, 29);
            menuLayoutPanel.TabIndex = 1;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(3, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(75, 23);
            saveButton.TabIndex = 0;
            saveButton.Text = "💾 Guardar";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
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
            menuLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private PictureBox playerCanvasPictureBox;
        private TableLayoutPanel mainLayoutPanel;
        private FlowLayoutPanel menuLayoutPanel;
        private Button saveButton;
        private PictureBox enemyCanvasPictureBox;
    }
}