using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscrutinioGrafica
{
    public partial class RegistroDatosForm : Form
    {
        private EscrutinioCN escrutinioNegocio = new EscrutinioCN();
        List<Mesa> mesasPorLocalidad;
        List<string> listaVacia = new List<string>();
        public RegistroDatosForm()
        {
            InitializeComponent();
            CargarLocalidades();
            CargarCandidatos();
            btnGuardar.Click += cmbCandidato_SelectedIndexChanged;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarLocalidades()
        {

            // Obtener las localidades desde la capa de negocio  
            List<Localidad> localidades = escrutinioNegocio.ObtenerLocalidades();

            // Limpiar el ComboBox antes de llenarlo  
            cmbLocalidad.DataSource = listaVacia;
            cmbLocalidad.DataSource = localidades;

        }

        private void CargarCandidatos()
        {
            List<Candidato> candidatos = escrutinioNegocio.ObtenerCandidatos();

            cmbCandidato.DataSource = listaVacia;
            cmbCandidato.DataSource = candidatos;
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            Localidad localidad = cmbLocalidad.SelectedItem as Localidad;          
            mesasPorLocalidad = escrutinioNegocio.ObtenerMesasPorLocalidad(localidad.IdLocalidad);
            cmbNumeroMesa.DataSource = listaVacia;
            cmbNumeroMesa.DataSource = mesasPorLocalidad;
            Mesa mesa = cmbNumeroMesa.SelectedItem as Mesa;
            txtVotantes.Text = mesa.Votantes.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idMesa = ((Mesa)cmbNumeroMesa.SelectedItem).IdMesa;
            int idCandidato = ((Candidato)cmbCandidato.SelectedItem).IdCandidato;
            if (!escrutinioNegocio.EstaMesaCerrada(idMesa))
            {
                
                int votosTotales = escrutinioNegocio.ObtenerEstadisticasMesa(idMesa).TotalVotos;
                if (votosTotales <= ((Mesa)cmbNumeroMesa.SelectedItem).Votantes)
                {
                    int votosValidos = (int)nudVotosValidos.Value;
                    int blancos = (int)nudVotosBlancos.Value;
                    int nulos = (int)nudVotosNulos.Value;
                    int ausentes = (int)nudAusentes.Value;

                    escrutinioNegocio.GuardarOActualizarVotos(idMesa, idCandidato, votosValidos, blancos, nulos, ausentes);
                    MessageBox.Show("Votos registrados correctamente.");
                }
                else
                {
                    MessageBox.Show("El número de votos no coincide con los votantes regsitrados");
                    return;
                }
            }
            
        }

        private void cmbCandidato_SelectedIndexChanged(object sender, EventArgs e)
        {
            Candidato candidato = cmbCandidato.SelectedItem as Candidato;
            Mesa mesa = cmbNumeroMesa.SelectedItem as Mesa;

            int votos = escrutinioNegocio.ObtenerVotosValidos(mesa.IdMesa, candidato.IdCandidato);


             nudVotosValidos.Value = votos;

        }

        private void cmbNumeroMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mesa mesa = cmbNumeroMesa.SelectedItem as Mesa;
            ReporteMesaDTO reporteMesaDTO = escrutinioNegocio.ObtenerVotosMesa(mesa.IdMesa);

            nudVotosNulos.Value = reporteMesaDTO.Nulos;
            nudVotosBlancos.Value = reporteMesaDTO.Blancos;
            nudAusentes.Value = reporteMesaDTO.Ausentes;
        }
    }
}
