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
            canvasPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox).BeginInit();
            SuspendLayout();
            // 
            // canvasPictureBox
            // 
            canvasPictureBox.Dock = DockStyle.Fill;
            canvasPictureBox.Location = new Point(0, 0);
            canvasPictureBox.Name = "canvasPictureBox";
            canvasPictureBox.Size = new Size(800, 450);
            canvasPictureBox.TabIndex = 0;
            canvasPictureBox.TabStop = false;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(canvasPictureBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "GameForm";
            Text = "GameForm";
            ((System.ComponentModel.ISupportInitialize)canvasPictureBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox canvasPictureBox;
    }
}