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
            mainLayoutPanel = new TableLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            flowLayoutPanel4 = new FlowLayoutPanel();
            flowLayoutPanel5 = new FlowLayoutPanel();
            flowLayoutPanel6 = new FlowLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)GridXBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridYBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PatrulleroCountBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AcorazadoCountBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DestructorCountBox).BeginInit();
            mainLayoutPanel.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // GridXBox
            // 
            GridXBox.Location = new Point(122, 3);
            GridXBox.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            GridXBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GridXBox.Name = "GridXBox";
            GridXBox.Size = new Size(39, 23);
            GridXBox.TabIndex = 0;
            GridXBox.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // GridYBox
            // 
            GridYBox.Location = new Point(188, 3);
            GridYBox.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            GridYBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GridYBox.Name = "GridYBox";
            GridYBox.Size = new Size(39, 23);
            GridYBox.TabIndex = 1;
            GridYBox.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // PatrulleroCountBox
            // 
            PatrulleroCountBox.Anchor = AnchorStyles.Top;
            PatrulleroCountBox.Location = new Point(144, 3);
            PatrulleroCountBox.Name = "PatrulleroCountBox";
            PatrulleroCountBox.Size = new Size(107, 23);
            PatrulleroCountBox.TabIndex = 2;
            PatrulleroCountBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // AcorazadoCountBox
            // 
            AcorazadoCountBox.Anchor = AnchorStyles.Top;
            AcorazadoCountBox.Location = new Point(149, 3);
            AcorazadoCountBox.Name = "AcorazadoCountBox";
            AcorazadoCountBox.Size = new Size(107, 23);
            AcorazadoCountBox.TabIndex = 4;
            AcorazadoCountBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DestructorCountBox
            // 
            DestructorCountBox.Anchor = AnchorStyles.Top;
            DestructorCountBox.Location = new Point(154, 3);
            DestructorCountBox.Name = "DestructorCountBox";
            DestructorCountBox.Size = new Size(107, 23);
            DestructorCountBox.TabIndex = 3;
            DestructorCountBox.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // GridSizeLabel
            // 
            GridSizeLabel.Anchor = AnchorStyles.Left;
            GridSizeLabel.AutoSize = true;
            GridSizeLabel.Location = new Point(3, 7);
            GridSizeLabel.Margin = new Padding(3, 0, 8, 0);
            GridSizeLabel.Name = "GridSizeLabel";
            GridSizeLabel.Size = new Size(108, 15);
            GridSizeLabel.TabIndex = 5;
            GridSizeLabel.Text = "Tamaño de Tablero";
            // 
            // ByLabel
            // 
            ByLabel.Anchor = AnchorStyles.Left;
            ByLabel.AutoSize = true;
            ByLabel.Location = new Point(167, 7);
            ByLabel.Name = "ByLabel";
            ByLabel.Size = new Size(15, 15);
            ByLabel.TabIndex = 6;
            ByLabel.Text = "×";
            // 
            // B3Label
            // 
            B3Label.Anchor = AnchorStyles.None;
            B3Label.AutoSize = true;
            B3Label.Location = new Point(3, 7);
            B3Label.Margin = new Padding(3, 0, 8, 0);
            B3Label.Name = "B3Label";
            B3Label.Size = new Size(135, 15);
            B3Label.TabIndex = 8;
            B3Label.Text = "Cantidad de Acorazados";
            // 
            // B2Label
            // 
            B2Label.Anchor = AnchorStyles.Left;
            B2Label.AutoSize = true;
            B2Label.Location = new Point(3, 7);
            B2Label.Margin = new Padding(3, 0, 8, 0);
            B2Label.Name = "B2Label";
            B2Label.Size = new Size(140, 15);
            B2Label.TabIndex = 9;
            B2Label.Text = "Cantidad de Destructores";
            // 
            // B1Label
            // 
            B1Label.Anchor = AnchorStyles.Left;
            B1Label.AutoSize = true;
            B1Label.Location = new Point(3, 7);
            B1Label.Margin = new Padding(3, 0, 8, 0);
            B1Label.Name = "B1Label";
            B1Label.Size = new Size(130, 15);
            B1Label.TabIndex = 10;
            B1Label.Text = "Cantidad de Patrulleros";
            // 
            // DificultadLabel
            // 
            DificultadLabel.Anchor = AnchorStyles.None;
            DificultadLabel.AutoSize = true;
            DificultadLabel.Location = new Point(3, 7);
            DificultadLabel.Margin = new Padding(3, 0, 8, 0);
            DificultadLabel.Name = "DificultadLabel";
            DificultadLabel.Size = new Size(58, 15);
            DificultadLabel.TabIndex = 11;
            DificultadLabel.Text = "Dificultad";
            // 
            // DificultadComboBox
            // 
            DificultadComboBox.Anchor = AnchorStyles.Top;
            DificultadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DificultadComboBox.FormattingEnabled = true;
            DificultadComboBox.Location = new Point(72, 3);
            DificultadComboBox.Name = "DificultadComboBox";
            DificultadComboBox.Size = new Size(121, 23);
            DificultadComboBox.TabIndex = 12;
            DificultadComboBox.SelectedIndexChanged += DificultadComboBox_SelectedIndexChanged;
            // 
            // TitleLabel
            // 
            TitleLabel.Anchor = AnchorStyles.None;
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 20F);
            TitleLabel.Location = new Point(36, 31);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(311, 37);
            TitleLabel.TabIndex = 13;
            TitleLabel.Text = "Configuración de Partida";
            // 
            // CancelarButton
            // 
            CancelarButton.Location = new Point(3, 3);
            CancelarButton.Name = "CancelarButton";
            CancelarButton.Size = new Size(112, 33);
            CancelarButton.TabIndex = 14;
            CancelarButton.Text = "Cancelar";
            CancelarButton.UseVisualStyleBackColor = false;
            CancelarButton.Click += CancelarButton_Click;
            // 
            // OKButton
            // 
            OKButton.Location = new Point(121, 3);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(112, 33);
            OKButton.TabIndex = 15;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = false;
            OKButton.Click += OKButton_Click;
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 1;
            mainLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            mainLayoutPanel.Controls.Add(TitleLabel, 0, 0);
            mainLayoutPanel.Controls.Add(flowLayoutPanel2, 0, 2);
            mainLayoutPanel.Controls.Add(flowLayoutPanel3, 0, 3);
            mainLayoutPanel.Controls.Add(flowLayoutPanel4, 0, 4);
            mainLayoutPanel.Controls.Add(flowLayoutPanel5, 0, 5);
            mainLayoutPanel.Controls.Add(flowLayoutPanel6, 0, 6);
            mainLayoutPanel.Controls.Add(flowLayoutPanel1, 0, 8);
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.Location = new Point(0, 0);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 9;
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.Size = new Size(384, 361);
            mainLayoutPanel.TabIndex = 16;
            mainLayoutPanel.Paint += mainLayoutPanel_Paint;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Anchor = AnchorStyles.Top;
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(GridSizeLabel);
            flowLayoutPanel2.Controls.Add(GridXBox);
            flowLayoutPanel2.Controls.Add(ByLabel);
            flowLayoutPanel2.Controls.Add(GridYBox);
            flowLayoutPanel2.Location = new Point(77, 103);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(230, 29);
            flowLayoutPanel2.TabIndex = 14;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.Top;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(B1Label);
            flowLayoutPanel3.Controls.Add(PatrulleroCountBox);
            flowLayoutPanel3.Location = new Point(65, 138);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(254, 29);
            flowLayoutPanel3.TabIndex = 15;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.None;
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.Controls.Add(B2Label);
            flowLayoutPanel4.Controls.Add(DestructorCountBox);
            flowLayoutPanel4.Location = new Point(60, 173);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(264, 29);
            flowLayoutPanel4.TabIndex = 16;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.None;
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel5.Controls.Add(B3Label);
            flowLayoutPanel5.Controls.Add(AcorazadoCountBox);
            flowLayoutPanel5.Location = new Point(62, 208);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(259, 29);
            flowLayoutPanel5.TabIndex = 17;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Anchor = AnchorStyles.Top;
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel6.Controls.Add(DificultadLabel);
            flowLayoutPanel6.Controls.Add(DificultadComboBox);
            flowLayoutPanel6.Location = new Point(94, 243);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(196, 29);
            flowLayoutPanel6.TabIndex = 18;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(CancelarButton);
            flowLayoutPanel1.Controls.Add(OKButton);
            flowLayoutPanel1.Location = new Point(145, 319);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(236, 39);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // GameSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 361);
            Controls.Add(mainLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "GameSettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuración de Partida";
            ((System.ComponentModel.ISupportInitialize)GridXBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridYBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PatrulleroCountBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)AcorazadoCountBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DestructorCountBox).EndInit();
            mainLayoutPanel.ResumeLayout(false);
            mainLayoutPanel.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
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
        private TableLayoutPanel mainLayoutPanel;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private FlowLayoutPanel flowLayoutPanel5;
        private FlowLayoutPanel flowLayoutPanel6;
    }
}