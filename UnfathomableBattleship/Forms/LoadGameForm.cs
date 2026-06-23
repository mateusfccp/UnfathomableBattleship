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

            flowLayoutPanel.Controls.Clear();

            foreach (var partida in partidas)
            {
                // Creamos el panel contenedor de la tarjeta
                Panel card = new Panel
                {
                    Width = flowLayoutPanel.Width - 25,
                    Height = 80,
                    BackColor = Color.FromArgb(50, 70, 100),
                    Margin = new Padding(10)
                };
                Label infoLabel = new Label
                {
                    Text = $"Partida #{partida.Id} - Estado: {partida.State}\nÚltima vez: {partida.LastUpdate:dd/MM/yyyy HH:mm}\nTiempo jugado: {partida.ElapsedTime.Minutes} min",
                    ForeColor = Color.White,
                    AutoSize = true,
                    Location = new Point(10, 15),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                // Creamos el botón para cargar
                Button btnCargar = new Button
                {
                    Text = "Cargar",
                    BackColor = Color.MediumSpringGreen,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 40),
                    Location = new Point(card.Width - 120, 20),
                    Cursor = Cursors.Hand,
                    Tag = partida.Id
                };

                // Le asignamos el evento click al botón
                btnCargar.Click += BtnCargar_Click;

                // Metemos los textos y el botón a la tarjeta, y la tarjeta al FlowLayoutPanel
                card.Controls.Add(infoLabel);
                card.Controls.Add(btnCargar);
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
    }
}
