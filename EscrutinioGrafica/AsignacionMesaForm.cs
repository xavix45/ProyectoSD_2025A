using System;
using Negocio;
using Entidades;
using System.Windows.Forms;

namespace EscrutinioGrafica
{
    public partial class AsignacionMesaForm : Form
    {
        public AsignacionMesaForm()
        {
            InitializeComponent();
        }

        // Cambiar el nombre del objeto para evitar conflictos con el espacio de nombres 'Negocio'
        private EscrutinioCN escrutinioNegocio = new EscrutinioCN();

        private void CargarLocalidades()
        {
            // Obtener las localidades desde la capa de negocio  
            var localidades = escrutinioNegocio.ObtenerLocalidades();

            // Limpiar el ComboBox antes de llenarlo  
            cmbLocalidad.Items.Clear();

            // Llenar el ComboBox con los nombres de las localidades  
            foreach (var localidad in localidades)
            {
                cmbLocalidad.Items.Add(localidad.Nombre); // Mostrar solo el nombre de la localidad  
            }

            // Opcional: Si quieres seleccionar el primer elemento por defecto  
            if (cmbLocalidad.Items.Count > 0)
                cmbLocalidad.SelectedIndex = 0;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AsignacionMesaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
