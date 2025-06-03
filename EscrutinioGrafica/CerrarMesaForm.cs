using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Entidades;

namespace EscrutinioGrafica
{
    public partial class CerrarMesaForm : Form
    {
        private EscrutinioCN escrutinioCN = new EscrutinioCN();
        List<string> listaVacia = new List<string>();

        public CerrarMesaForm()
        {
            InitializeComponent();
        }

        private void btnCerrarVentana_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CerrarMesaForm_Load(object sender, EventArgs e)
        {
            cargarLocalidad();
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Localidad localidad = cmbLocalidad.SelectedItem as Localidad;

            cargarMesaPorLocalidad(localidad.IdLocalidad);
        }

        private void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            Mesa mesa = cmbNumeroMesa.SelectedItem as Mesa;
            if (mesa == null)
            {
                MessageBox.Show("Seleccione una mesa válida.", "Cierre de mesa");
                return;
            }

            // Verificar si la mesa ya está cerrada
            if (escrutinioCN.EstaMesaCerrada(mesa.IdMesa))
            {
                MessageBox.Show("Esta mesa ya está cerrada.", "Cierre de mesa");
                return;
            }

            // Confirmar con el usuario
            DialogResult resultado = MessageBox.Show("¿Seguro quieres cerrar la mesa?", "Cierre de mesa", MessageBoxButtons.OKCancel);
            if (resultado != DialogResult.OK)
                return;

            try
            {
                // Intentar cerrar la mesa, la función debe devolver string con resultado (OK, ERROR, CERRADA)
                string respuesta = escrutinioCN.CerrarMesa(mesa.IdMesa);

                if (respuesta == "OK")
                {
                    MessageBox.Show("La mesa se cerró correctamente.", "Cierre de mesa");
                    // Aquí puedes actualizar UI, recargar lista, etc.
                }
                else if (respuesta == "CERRADA")
                {
                    MessageBox.Show("La mesa ya estaba cerrada previamente.", "Cierre de mesa");
                }
                else if (respuesta.StartsWith("ERROR"))
                {
                    MessageBox.Show($"No se pudo cerrar la mesa: {respuesta}", "Error");
                }
                else
                {
                    MessageBox.Show($"Respuesta inesperada del servidor: {respuesta}", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar la mesa: {ex.Message}", "Error");
            }
        }


        private void cargarLocalidad()
        {
            cmbLocalidad.DataSource = listaVacia;
            cmbLocalidad.DataSource = escrutinioCN.ObtenerLocalidades();
        }

        private void cargarMesaPorLocalidad(int idLocalidad) 
        {
            cmbNumeroMesa.DataSource = listaVacia;
            cmbNumeroMesa.DataSource = escrutinioCN.ObtenerMesasPorLocalidad(idLocalidad);
        }
    }
}
