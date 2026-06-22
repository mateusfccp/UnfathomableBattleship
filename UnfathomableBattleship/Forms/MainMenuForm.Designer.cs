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
            SuspendLayout();
            // 
            // NewGameButton
            // 
            NewGameButton.Location = new Point(325, 195);
            NewGameButton.Margin = new Padding(3, 4, 3, 4);
            NewGameButton.Name = "NewGameButton";
            NewGameButton.Size = new Size(217, 99);
            NewGameButton.TabIndex = 1;
            NewGameButton.Text = "New Game";
            NewGameButton.UseVisualStyleBackColor = true;
            NewGameButton.Click += NewGameButton_Click;
            // 
            // LoadGameButton
            // 
            LoadGameButton.Location = new Point(325, 325);
            LoadGameButton.Margin = new Padding(3, 4, 3, 4);
            LoadGameButton.Name = "LoadGameButton";
            LoadGameButton.Size = new Size(217, 105);
            LoadGameButton.TabIndex = 2;
            LoadGameButton.Text = "Load Game";
            LoadGameButton.UseVisualStyleBackColor = true;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(LoadGameButton);
            Controls.Add(NewGameButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainMenuForm";
            Text = "MainMenuForm";
            ResumeLayout(false);
        }

        #endregion

        private Button NewGameButton;
        private Button LoadGameButton;
    }
}