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
            if (!escrutinioCN.EstaMesaCerrada(mesa.IdMesa))
            {

                DialogResult resultado = MessageBox.Show("¿Seguro quieres cerrar la mesa?", "Cierre de mesa", MessageBoxButtons.OKCancel);
                if (resultado == DialogResult.OK)
                {
                    escrutinioCN.CerrarMesa(mesa.IdMesa);
                }
                else
                {
                    return;
                }
            }
            else 
            {
                MessageBox.Show("Esta mesa esta cerrada", "Cierre de mesa");
                return;
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
