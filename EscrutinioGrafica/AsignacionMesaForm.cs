using System;
using Negocio;
using Entidades;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EscrutinioGrafica
{
    public partial class AsignacionMesaForm : Form
    {
        public AsignacionMesaForm()
        {
            InitializeComponent();
            dtpFecha.MaxDate = DateTime.Now;
        }

        // Cambiar el nombre del objeto para evitar conflictos con el espacio de nombres 'Negocio'
        private EscrutinioCN escrutinioNegocio = new EscrutinioCN();

        private void CargarLocalidades()
        {

            // Obtener las localidades desde la capa de negocio  
            List<Localidad> localidades = escrutinioNegocio.ObtenerLocalidades();

            // Limpiar el ComboBox antes de llenarlo  
            cmbLocalidad.DataSource = null;
            cmbLocalidad.DataSource = localidades;

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
            CargarLocalidades();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAsignacionMesa_Click(object sender, EventArgs e)
        {
            Localidad localidad = cmbLocalidad.SelectedItem as Localidad;
            DateTime fecha = dtpFecha.Value.Date;
            var (resultadoAsignacion, mesaAsignada) = escrutinioNegocio.AsignarMesa(fecha, localidad.Nombre);
            if (resultadoAsignacion == "OK")
            {
                MessageBox.Show("Mesa asignada correctamente:", "Asignación de mesa");
                txtNumeroMesa.Text = mesaAsignada.NumeroMesa.ToString();
                txtVotantesAsignados.Text = mesaAsignada.Votantes.ToString();
                lstCandidatos.Items.Clear();
                var candidatos = escrutinioNegocio.ObtenerCandidatos();
                lstCandidatos.DataSource = null;
                lstCandidatos.DataSource = candidatos;

            }
            else
            {
                MessageBox.Show($"Error al asignar mesa: {resultadoAsignacion}", "Asignación de mesa");
                return;
            }
        }
    }
}
