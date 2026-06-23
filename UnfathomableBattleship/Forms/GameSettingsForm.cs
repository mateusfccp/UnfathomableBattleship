using System.Windows.Forms.VisualStyles;
using UnfathomableBattleship.Enums;
using UnfathomableBattleship.Interfaces;
using UnfathomableBattleship.Models;

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
            modeGomboBox.Items.Clear();
            modeGomboBox.DisplayMember = "Name";
            modeGomboBox.ValueMember = "Id";
            modeGomboBox.DataSource =
                Enum
                .GetValues<GameMode>()
                .Select(gameMode => new { Value = gameMode, Display = gameMode.ToDisplayString() })
                .ToList();
            modeGomboBox.DisplayMember = "Display";
            modeGomboBox.ValueMember = "Value";
            modeGomboBox.SelectedIndex = 2;

            GridXBox.ValueChanged += ActualizarLimitesDeBarcos;
            GridYBox.ValueChanged += ActualizarLimitesDeBarcos;
            PatrulleroCountBox.ValueChanged += ActualizarLimitesDeBarcos;
            DestructorCountBox.ValueChanged += ActualizarLimitesDeBarcos;
            AcorazadoCountBox.ValueChanged += ActualizarLimitesDeBarcos;

            ActualizarLimitesDeBarcos(null, EventArgs.Empty);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            GameMode Gamemode = (GameMode)modeGomboBox.SelectedValue;
            Size BoardSize = new Size((int)GridXBox.Value, (int)GridYBox.Value);
            List<Ship> Ships = new List<Ship>();
            for (int i = 0; i < PatrulleroCountBox.Value; i++) Ships.Add(new Ship(1, ShipOrientation.Horizontal));
            for (int i = 0; i < DestructorCountBox.Value; i++) Ships.Add(new Ship(2, ShipOrientation.Horizontal));
            for (int i = 0; i < AcorazadoCountBox.Value; i++) Ships.Add(new Ship(3, ShipOrientation.Horizontal));

            if (Ships.Count == 0)
            {
                MessageBox.Show("Debes agregar al menos un barco al tablero para poder continuar.", "Faltan barcos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GameConfiguration config = new GameConfiguration(Gamemode, BoardSize, Ships);
            MainForm?.SwitchForm(new PreparationForm(_gameManager, config));
            this.Hide();
            this.Dispose();
        }

        private void ActualizarLimitesDeBarcos(object sender, EventArgs e)
        {
            // Calculamos el espacio total del tablero
            int areaTotal = (int)(GridXBox.Value * GridYBox.Value);

            // Calculamos cuánto espacio ocupa cada tipo de barco actualmente
            int areaPatrulleros = (int)PatrulleroCountBox.Value * 1;
            int areaDestructores = (int)DestructorCountBox.Value * 2;
            int areaAcorazados = (int)AcorazadoCountBox.Value * 3;

            // Calculamos el espacio disponible para CADA barco (ignorando su propia área actual)
            int maxPatrulleros = (areaTotal - areaDestructores - areaAcorazados) / 1;
            int maxDestructores = (areaTotal - areaPatrulleros - areaAcorazados) / 2;
            int maxAcorazados = (areaTotal - areaPatrulleros - areaDestructores) / 3;

            // Actualizamos los máximos de forma segura
            SetSafeMaximum(PatrulleroCountBox, maxPatrulleros);
            SetSafeMaximum(DestructorCountBox, maxDestructores);
            SetSafeMaximum(AcorazadoCountBox, maxAcorazados);
        }

        private void SetSafeMaximum(NumericUpDown nud, int nuevoMaximo)
        {
            // Evitamos que el máximo sea un número negativo
            if (nuevoMaximo < 0) nuevoMaximo = 0;

            // Si el valor actual es mayor al nuevo límite, lo reducimos primero para evitar crasheos
            if (nud.Value > nuevoMaximo)
            {
                nud.Value = nuevoMaximo;
            }

            nud.Maximum = nuevoMaximo;
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Hide();
            Dispose();
        }
    }
}
