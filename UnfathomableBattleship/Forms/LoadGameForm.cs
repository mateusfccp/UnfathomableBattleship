using UnfathomableBattleship.Interfaces;

namespace UnfathomableBattleship.Forms
{
    public partial class LoadGameForm : Form
    {
        private MainForm? MainForm => Tag as MainForm;
        private readonly IGameManager _gameManager;
        public LoadGameForm(IGameManager gameManager)
        {
            InitializeComponent();
            _gameManager = gameManager;
            LoadGames();
        }

        private void LoadGames()
        {
            var partidas = _gameManager.GetCurrentPlayerGames();
            foreach (Control control in flowLayoutPanel.Controls)
            {
                control.Dispose();
            }
            if ((partidas.Count == 0))
            {
                flowLayoutPanel.Controls.Clear();
                Panel card = new Panel
                {
                    Width = flowLayoutPanel.Width - 25,
                    Height = 90,
                    BackColor = Color.Gray,
                    Margin = new Padding(10)
                };
                Label infoLabel = new Label
                {
                    Text = "No hay partidas guardadas",
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(10, 15),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                card.Controls.Add(infoLabel);
                flowLayoutPanel.Controls.Add(card);
                return;
            }


            foreach (var partida in partidas)
            {
                Panel card = new Panel
                {
                    Width = flowLayoutPanel.Width - 25,
                    Height = 90,
                    BackColor = Color.FromArgb(50, 70, 100),
                    Margin = new Padding(10)
                };
                Label infoLabel = new Label
                {
                    Text = $"Partida #{partida.Id} - Estado: {partida.State}\nÚltima vez: {partida.LastUpdate:dd/MM/yyyy HH:mm}\nTiempo jugado: {(int)partida.ElapsedTime.TotalMinutes} min",
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(10, 15),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };
                Button btnCargar = new Button
                {
                    Text = "Cargar",
                    BackColor = Color.MediumSpringGreen,
                    Size = new Size(100, 40),
                    Location = new Point(card.Width - 120, 20),
                    Cursor = Cursors.Hand,
                    Tag = partida.Id
                };

                Button btnBorrar = new Button
                {
                    Text = "Borrar",
                    BackColor = Color.LightCoral,
                    Size = new Size(100, 40),
                    Location = new Point(btnCargar.Left - 120, 20),
                    Cursor = Cursors.Hand,
                    Tag = partida.Id
                };

                btnCargar.Click += BtnCargar_Click;
                btnBorrar.Click += BtnBorrar_Click;

                card.Controls.Add(infoLabel);
                card.Controls.Add(btnCargar);
                card.Controls.Add(btnBorrar);
                flowLayoutPanel.Controls.Add(card);
            }
        }

        private void BtnCargar_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("Desea cargar esta partida?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MainForm?.SwitchForm(new GameForm(_gameManager, _gameManager.LoadGame((sender as Button)?.Tag)));
                Hide();
                Dispose();
            }
        }

        private void BtnBorrar_Click(object? sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Desea borrar esta partida?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _gameManager.DeleteGame((sender as Button)?.Tag);
                    LoadGames();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al borrar la partida: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
