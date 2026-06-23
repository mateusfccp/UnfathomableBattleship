using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;
using UnfathomableBattleship.Services;

namespace UnfathomableBattleship.Forms
{
    public partial class GameSettingsForm : Form
    {
        private readonly IGameManager _gameManager;
        private MainForm? MainForm => Tag as MainForm;
        public GameSettingsForm(IGameManager gameManager)
        {
            InitializeComponent();
            _gameManager = gameManager;
            InicializeElements();
        }
        private void InicializeElements()
        {
            DificultadComboBox.Items.Clear();
            DificultadComboBox.DisplayMember = "Name";
            DificultadComboBox.ValueMember = "Id";
            DificultadComboBox.DataSource = Enum.GetValues(typeof(GameMode));
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            GameMode Gamemode = (GameMode)DificultadComboBox.SelectedItem;
            Size BoardSize = new Size((int)GridXBox.Value, (int)GridYBox.Value);
            List<Ship> Ships = new List<Ship>();
            for (int i = 0; i < PatrulleroCountBox.Value; i++) Ships.Add(new Ship(1, ShipOrientation.Horizontal));
            for (int i = 0; i < DestructorCountBox.Value; i++) Ships.Add(new Ship(2, ShipOrientation.Horizontal));
            for (int i = 0; i < AcorazadoCountBox.Value; i++) Ships.Add(new Ship(3, ShipOrientation.Horizontal));
            GameConfiguration config = new GameConfiguration(Gamemode, BoardSize, Ships);
            MainForm?.SwitchForm(new PreparationForm(_gameManager, config)); // Porque no funciona???
            this.Hide();
        }

        private void QuickSettings_Click(object sender, EventArgs e)
        {
            _gameManager.QuickGame();
        }
    }
}
