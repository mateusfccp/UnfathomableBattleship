namespace UnfathomableBattleship.Forms
{
    partial class GameSettingsForm
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
            GridXBox = new NumericUpDown();
            GridYBox = new NumericUpDown();
            PatrulleroCountBox = new NumericUpDown();
            AcorazadoCountBox = new NumericUpDown();
            DestructorCountBox = new NumericUpDown();
            GridSizeLabel = new Label();
            ByLabel = new Label();
            B3Label = new Label();
            B2Label = new Label();
            B1Label = new Label();
            DificultadLabel = new Label();
            DificultadComboBox = new ComboBox();
            TitleLabel = new Label();
            CancelarButton = new Button();
            OKButton = new Button();
            ((System.ComponentModel.ISupportInitialize)GridXBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridYBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PatrulleroCountBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AcorazadoCountBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DestructorCountBox).BeginInit();
            SuspendLayout();
            // 
            // GridXBox
            // 
            GridXBox.Location = new Point(222, 113);
            GridXBox.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            GridXBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GridXBox.Name = "GridXBox";
            GridXBox.Size = new Size(39, 23);
            GridXBox.TabIndex = 0;
            GridXBox.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // GridYBox
            // 
            GridYBox.Location = new Point(290, 113);
            GridYBox.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            GridYBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GridYBox.Name = "GridYBox";
            GridYBox.Size = new Size(39, 23);
            GridYBox.TabIndex = 1;
            GridYBox.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // PatrulleroCountBox
            // 
            PatrulleroCountBox.Location = new Point(222, 158);
            PatrulleroCountBox.Name = "PatrulleroCountBox";
            PatrulleroCountBox.Size = new Size(107, 23);
            PatrulleroCountBox.TabIndex = 2;
            // 
            // AcorazadoCountBox
            // 
            AcorazadoCountBox.Location = new Point(222, 216);
            AcorazadoCountBox.Name = "AcorazadoCountBox";
            AcorazadoCountBox.Size = new Size(107, 23);
            AcorazadoCountBox.TabIndex = 4;
            // 
            // DestructorCountBox
            // 
            DestructorCountBox.Location = new Point(222, 187);
            DestructorCountBox.Name = "DestructorCountBox";
            DestructorCountBox.Size = new Size(107, 23);
            DestructorCountBox.TabIndex = 3;
            // 
            // GridSizeLabel
            // 
            GridSizeLabel.AutoSize = true;
            GridSizeLabel.Location = new Point(105, 115);
            GridSizeLabel.Name = "GridSizeLabel";
            GridSizeLabel.Size = new Size(111, 15);
            GridSizeLabel.TabIndex = 5;
            GridSizeLabel.Text = "Tamaño de Tablero:";
            // 
            // ByLabel
            // 
            ByLabel.AutoSize = true;
            ByLabel.Location = new Point(268, 117);
            ByLabel.Name = "ByLabel";
            ByLabel.Size = new Size(14, 15);
            ByLabel.TabIndex = 6;
            ByLabel.Text = "X";
            // 
            // B3Label
            // 
            B3Label.AutoSize = true;
            B3Label.Location = new Point(78, 218);
            B3Label.Name = "B3Label";
            B3Label.Size = new Size(138, 15);
            B3Label.TabIndex = 8;
            B3Label.Text = "Cantidad de Acorazados:";
            // 
            // B2Label
            // 
            B2Label.AutoSize = true;
            B2Label.Location = new Point(73, 189);
            B2Label.Name = "B2Label";
            B2Label.Size = new Size(143, 15);
            B2Label.TabIndex = 9;
            B2Label.Text = "Cantidad de Destructores:";
            // 
            // B1Label
            // 
            B1Label.AutoSize = true;
            B1Label.Location = new Point(83, 160);
            B1Label.Name = "B1Label";
            B1Label.Size = new Size(133, 15);
            B1Label.TabIndex = 10;
            B1Label.Text = "Cantidad de Patrulleros:";
            // 
            // DificultadLabel
            // 
            DificultadLabel.AutoSize = true;
            DificultadLabel.Location = new Point(122, 262);
            DificultadLabel.Name = "DificultadLabel";
            DificultadLabel.Size = new Size(61, 15);
            DificultadLabel.TabIndex = 11;
            DificultadLabel.Text = "Dificultad:";
            // 
            // DificultadComboBox
            // 
            DificultadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DificultadComboBox.FormattingEnabled = true;
            DificultadComboBox.Location = new Point(186, 257);
            DificultadComboBox.Name = "DificultadComboBox";
            DificultadComboBox.Size = new Size(121, 23);
            DificultadComboBox.TabIndex = 12;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 20F);
            TitleLabel.Location = new Point(64, 38);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(311, 37);
            TitleLabel.TabIndex = 13;
            TitleLabel.Text = "Configuración de Partida";
            // 
            // CancelarButton
            // 
            CancelarButton.BackColor = SystemColors.Info;
            CancelarButton.Location = new Point(186, 366);
            CancelarButton.Name = "CancelarButton";
            CancelarButton.Size = new Size(112, 33);
            CancelarButton.TabIndex = 14;
            CancelarButton.Text = "Cancelar";
            CancelarButton.UseVisualStyleBackColor = false;
            CancelarButton.Click += CancelarButton_Click;
            // 
            // OKButton
            // 
            OKButton.BackColor = SystemColors.MenuHighlight;
            OKButton.Location = new Point(310, 366);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(112, 33);
            OKButton.TabIndex = 15;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = false;
            OKButton.Click += OKButton_Click;
            // 
            // GameSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 411);
            Controls.Add(OKButton);
            Controls.Add(CancelarButton);
            Controls.Add(TitleLabel);
            Controls.Add(DificultadComboBox);
            Controls.Add(DificultadLabel);
            Controls.Add(B1Label);
            Controls.Add(B2Label);
            Controls.Add(B3Label);
            Controls.Add(ByLabel);
            Controls.Add(GridSizeLabel);
            Controls.Add(DestructorCountBox);
            Controls.Add(AcorazadoCountBox);
            Controls.Add(PatrulleroCountBox);
            Controls.Add(GridYBox);
            Controls.Add(GridXBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameSettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameSettingsForm";
            ((System.ComponentModel.ISupportInitialize)GridXBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridYBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PatrulleroCountBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)AcorazadoCountBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DestructorCountBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown GridXBox;
        private NumericUpDown GridYBox;
        private NumericUpDown PatrulleroCountBox;
        private NumericUpDown DestructorCountBox;
        private NumericUpDown AcorazadoCountBox;
        private Label GridSizeLabel;
        private Label ByLabel;
        private Label B1Label;
        private Label B2Label;
        private Label B3Label;
        private Label DificultadLabel;
        private ComboBox DificultadComboBox;
        private Label TitleLabel;
        private Button CancelarButton;
        private Button OKButton;
    }
}