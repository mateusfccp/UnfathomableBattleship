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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettingsForm));
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
            modeGomboBox = new ComboBox();
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
            GridXBox.BackColor = SystemColors.GradientActiveCaption;
            GridXBox.Location = new Point(157, 4);
            GridXBox.Margin = new Padding(3, 4, 3, 4);
            GridXBox.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            GridXBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GridXBox.Name = "GridXBox";
            GridXBox.Size = new Size(45, 27);
            GridXBox.TabIndex = 0;
            GridXBox.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // GridYBox
            // 
            GridYBox.BackColor = SystemColors.GradientActiveCaption;
            GridYBox.Location = new Point(233, 4);
            GridYBox.Margin = new Padding(3, 4, 3, 4);
            GridYBox.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            GridYBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            GridYBox.Name = "GridYBox";
            GridYBox.Size = new Size(45, 27);
            GridYBox.TabIndex = 1;
            GridYBox.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // PatrulleroCountBox
            // 
            PatrulleroCountBox.Anchor = AnchorStyles.Top;
            PatrulleroCountBox.BackColor = SystemColors.GradientActiveCaption;
            PatrulleroCountBox.Location = new Point(187, 4);
            PatrulleroCountBox.Margin = new Padding(3, 4, 3, 4);
            PatrulleroCountBox.Name = "PatrulleroCountBox";
            PatrulleroCountBox.Size = new Size(122, 27);
            PatrulleroCountBox.TabIndex = 2;
            PatrulleroCountBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // AcorazadoCountBox
            // 
            AcorazadoCountBox.Anchor = AnchorStyles.Top;
            AcorazadoCountBox.BackColor = SystemColors.GradientActiveCaption;
            AcorazadoCountBox.Location = new Point(192, 4);
            AcorazadoCountBox.Margin = new Padding(3, 4, 3, 4);
            AcorazadoCountBox.Name = "AcorazadoCountBox";
            AcorazadoCountBox.Size = new Size(122, 27);
            AcorazadoCountBox.TabIndex = 4;
            AcorazadoCountBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // DestructorCountBox
            // 
            DestructorCountBox.Anchor = AnchorStyles.Top;
            DestructorCountBox.BackColor = SystemColors.GradientActiveCaption;
            DestructorCountBox.Location = new Point(201, 4);
            DestructorCountBox.Margin = new Padding(3, 4, 3, 4);
            DestructorCountBox.Name = "DestructorCountBox";
            DestructorCountBox.Size = new Size(122, 27);
            DestructorCountBox.TabIndex = 3;
            DestructorCountBox.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // GridSizeLabel
            // 
            GridSizeLabel.Anchor = AnchorStyles.Left;
            GridSizeLabel.AutoSize = true;
            GridSizeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            GridSizeLabel.Location = new Point(3, 7);
            GridSizeLabel.Margin = new Padding(3, 0, 9, 0);
            GridSizeLabel.Name = "GridSizeLabel";
            GridSizeLabel.Size = new Size(142, 20);
            GridSizeLabel.TabIndex = 5;
            GridSizeLabel.Text = "Tamaño de Tablero";
            // 
            // ByLabel
            // 
            ByLabel.Anchor = AnchorStyles.Left;
            ByLabel.AutoSize = true;
            ByLabel.Location = new Point(208, 7);
            ByLabel.Name = "ByLabel";
            ByLabel.Size = new Size(19, 20);
            ByLabel.TabIndex = 6;
            ByLabel.Text = "×";
            // 
            // B3Label
            // 
            B3Label.Anchor = AnchorStyles.None;
            B3Label.AutoSize = true;
            B3Label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            B3Label.Location = new Point(3, 7);
            B3Label.Margin = new Padding(3, 0, 9, 0);
            B3Label.Name = "B3Label";
            B3Label.Size = new Size(177, 20);
            B3Label.TabIndex = 8;
            B3Label.Text = "Cantidad de Acorazados";
            // 
            // B2Label
            // 
            B2Label.Anchor = AnchorStyles.Left;
            B2Label.AutoSize = true;
            B2Label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            B2Label.Location = new Point(3, 7);
            B2Label.Margin = new Padding(3, 0, 9, 0);
            B2Label.Name = "B2Label";
            B2Label.Size = new Size(186, 20);
            B2Label.TabIndex = 9;
            B2Label.Text = "Cantidad de Destructores";
            // 
            // B1Label
            // 
            B1Label.Anchor = AnchorStyles.Left;
            B1Label.AutoSize = true;
            B1Label.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            B1Label.Location = new Point(3, 7);
            B1Label.Margin = new Padding(3, 0, 9, 0);
            B1Label.Name = "B1Label";
            B1Label.Size = new Size(172, 20);
            B1Label.TabIndex = 10;
            B1Label.Text = "Cantidad de Patrulleros";
            // 
            // DificultadLabel
            // 
            DificultadLabel.Anchor = AnchorStyles.None;
            DificultadLabel.AutoSize = true;
            DificultadLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            DificultadLabel.Location = new Point(3, 8);
            DificultadLabel.Margin = new Padding(3, 0, 9, 0);
            DificultadLabel.Name = "DificultadLabel";
            DificultadLabel.Size = new Size(50, 20);
            DificultadLabel.TabIndex = 11;
            DificultadLabel.Text = "Modo";
            // 
            // modeGomboBox
            // 
            modeGomboBox.Anchor = AnchorStyles.Top;
            modeGomboBox.BackColor = SystemColors.GradientActiveCaption;
            modeGomboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modeGomboBox.FormattingEnabled = true;
            modeGomboBox.Location = new Point(65, 4);
            modeGomboBox.Margin = new Padding(3, 4, 3, 4);
            modeGomboBox.Name = "modeGomboBox";
            modeGomboBox.Size = new Size(138, 28);
            modeGomboBox.TabIndex = 12;
            // 
            // TitleLabel
            // 
            TitleLabel.Anchor = AnchorStyles.None;
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 20F);
            TitleLabel.Location = new Point(24, 43);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(390, 46);
            TitleLabel.TabIndex = 13;
            TitleLabel.Text = "Configuración de Partida";
            // 
            // CancelarButton
            // 
            CancelarButton.BackColor = Color.LightCoral;
            CancelarButton.Cursor = Cursors.Hand;
            CancelarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            CancelarButton.Location = new Point(3, 4);
            CancelarButton.Margin = new Padding(3, 4, 3, 4);
            CancelarButton.Name = "CancelarButton";
            CancelarButton.Size = new Size(128, 44);
            CancelarButton.TabIndex = 14;
            CancelarButton.Text = "Cancelar";
            CancelarButton.UseVisualStyleBackColor = false;
            CancelarButton.Click += CancelarButton_Click;
            // 
            // OKButton
            // 
            OKButton.BackColor = Color.MediumSpringGreen;
            OKButton.Cursor = Cursors.Hand;
            OKButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            OKButton.Location = new Point(137, 4);
            OKButton.Margin = new Padding(3, 4, 3, 4);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(128, 44);
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
            mainLayoutPanel.Margin = new Padding(3, 4, 3, 4);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 9;
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 133F));
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.RowStyles.Add(new RowStyle());
            mainLayoutPanel.Size = new Size(439, 481);
            mainLayoutPanel.TabIndex = 16;
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
            flowLayoutPanel2.Location = new Point(79, 137);
            flowLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(281, 35);
            flowLayoutPanel2.TabIndex = 14;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Anchor = AnchorStyles.Top;
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(B1Label);
            flowLayoutPanel3.Controls.Add(PatrulleroCountBox);
            flowLayoutPanel3.Location = new Point(63, 180);
            flowLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(312, 35);
            flowLayoutPanel3.TabIndex = 15;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Anchor = AnchorStyles.None;
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.Controls.Add(B2Label);
            flowLayoutPanel4.Controls.Add(DestructorCountBox);
            flowLayoutPanel4.Location = new Point(56, 223);
            flowLayoutPanel4.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(326, 35);
            flowLayoutPanel4.TabIndex = 16;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Anchor = AnchorStyles.None;
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel5.Controls.Add(B3Label);
            flowLayoutPanel5.Controls.Add(AcorazadoCountBox);
            flowLayoutPanel5.Location = new Point(61, 266);
            flowLayoutPanel5.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new Size(317, 35);
            flowLayoutPanel5.TabIndex = 17;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Anchor = AnchorStyles.Top;
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel6.Controls.Add(DificultadLabel);
            flowLayoutPanel6.Controls.Add(modeGomboBox);
            flowLayoutPanel6.Location = new Point(116, 309);
            flowLayoutPanel6.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new Size(206, 36);
            flowLayoutPanel6.TabIndex = 18;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(CancelarButton);
            flowLayoutPanel1.Controls.Add(OKButton);
            flowLayoutPanel1.Location = new Point(168, 425);
            flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(268, 52);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // GameSettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SteelBlue;
            ClientSize = new Size(439, 481);
            Controls.Add(mainLayoutPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
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
        private ComboBox modeGomboBox;
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