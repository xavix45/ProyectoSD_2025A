using Entidades;
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
        private Escrutinio padre;              // Referencia al formulario principal para usar conexión persistente
        List<Localidad> localidades;           // Lista para guardar localidades obtenidas
        List<Mesa> mesasPorLocalidad;          // Lista para mesas filtradas por localidad
        List<Candidato> candidatos;            // Lista de candidatos

        // Constructor modificado para recibir referencia al formulario principal
        public RegistroDatosForm(Escrutinio formularioPadre)
        {
            InitializeComponent();
            padre = formularioPadre;
            cmbLocalidad.SelectedIndexChanged += cmbCandidato_SelectedIndexChanged;
            cmbNumeroMesa.SelectedIndexChanged += cmbCandidato_SelectedIndexChanged;
        }

        // Obtiene localidades desde servidor usando conexión persistente
        private void CargarLocalidades()
        {
            string respuesta = padre.EnviarComando("OBTENERLOCALIDADES");
            localidades = UtilidadesProtocolo.DeserializarLocalidades(respuesta);
            cmbLocalidad.DataSource = localidades;
            cmbLocalidad.DisplayMember = "Nombre";
            cmbLocalidad.ValueMember = "IdLocalidad";
        }

        // Obtiene candidatos desde servidor usando conexión persistente
        private void CargarCandidatos()
        {
            string respuesta = padre.EnviarComando("OBTENERCANDIDATOS");
            candidatos = UtilidadesProtocolo.DeserializarCandidatos(respuesta);
            cmbCandidato.DataSource = candidatos;
        }

        // Evento cambio de selección en localidad
        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocalidad.SelectedItem == null) return;

            int idLocalidad = ((Localidad)cmbLocalidad.SelectedItem).IdLocalidad;
            string comando = $"OBTENERMESASPORLOCALIDAD|{idLocalidad}";
            string respuesta = padre.EnviarComando(comando);
            mesasPorLocalidad = UtilidadesProtocolo.DeserializarMesas(respuesta);

            cmbNumeroMesa.DataSource = mesasPorLocalidad;
            cmbNumeroMesa.DisplayMember = "NumeroMesa";
            cmbNumeroMesa.ValueMember = "IdMesa";

            // Mostrar votantes de la primera mesa (si hay alguna)
            if (mesasPorLocalidad.Count > 0)
                txtVotantes.Text = mesasPorLocalidad[0].Votantes.ToString();
            else
                txtVotantes.Text = "0";
        }

        // Evento cambio selección mesa
        private void cmbNumeroMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNumeroMesa.SelectedItem == null) return;

            var mesa = (Mesa)cmbNumeroMesa.SelectedItem;
            txtVotantes.Text = mesa.Votantes.ToString();

            // Obtener los votos extra (blancos, nulos, ausentes)
            string comando = $"OBTENERVOTOSEXTRASMESA|{mesa.IdMesa}";
            string respuesta = padre.EnviarComando(comando);

            if (respuesta.StartsWith("OK|"))
            {
                // Parsear la respuesta para obtener los valores de los votos extras
                var partes = respuesta.Split('|');
                if (partes.Length == 4)
                {
                    int blancos = int.Parse(partes[1]);
                    int nulos = int.Parse(partes[2]);
                    int ausentes = int.Parse(partes[3]);

                    // Asignar los valores a los NumericUpDown
                    nudVotosBlancos.Value = blancos;
                    nudVotosNulos.Value = nulos;
                    nudAusentes.Value = ausentes;
                }
            }
        }

        // Evento cambio selección candidato
        private void cmbCandidato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNumeroMesa.SelectedItem == null || cmbCandidato.SelectedItem == null) return;

            int idMesa = ((Mesa)cmbNumeroMesa.SelectedItem).IdMesa;
            int idCandidato = ((Candidato)cmbCandidato.SelectedItem).IdCandidato;

            // Comando para obtener votos válidos actuales para ese candidato y mesa
            string comando = $"OBTENERVOTOSVALIDOS|{idMesa}|{idCandidato}";
            string respuesta = padre.EnviarComando(comando);

            if (respuesta.StartsWith("OK|"))
            {
                string[] partes = respuesta.Split('|');
                if (int.TryParse(partes[1], out int votosValidos))
                {
                    nudVotosValidos.Value = votosValidos; // Actualiza el NumericUpDown
                }
            }
        }

        // Evento click en botón guardar votos
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbNumeroMesa.SelectedItem == null || cmbCandidato.SelectedItem == null)
            {
                MessageBox.Show("Seleccione mesa y candidato.");
                return;
            }

            // Obtener valores de la UI
            int idMesa = ((Mesa)cmbNumeroMesa.SelectedItem).IdMesa;
            int idCandidato = ((Candidato)cmbCandidato.SelectedItem).IdCandidato;
            int votosValidos = (int)nudVotosValidos.Value;
            int blancos = (int)nudVotosBlancos.Value;
            int nulos = (int)nudVotosNulos.Value;
            int ausentes = (int)nudAusentes.Value;

            // Construir string con votos válidos para el comando
            string votosCsv = votosValidos.ToString();

            // Enviar comando para registrar votos
            string comando = $"GUARDARACTUALIZARVOTOS|{idMesa}|{idCandidato}|{votosCsv}|{blancos}|{nulos}|{ausentes}";
            string respuesta = padre.EnviarComando(comando);

            if (respuesta.StartsWith("OK"))
            {
                MessageBox.Show("Votos registrados correctamente.");
            }
            else
            {
                MessageBox.Show($"Error al registrar votos: {respuesta}");
            }
        }

        // Evento botón cerrar formulario
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RegistroDatosForm_Load_1(object sender, EventArgs e)
        {
            CargarLocalidades();  // Carga localidades en combo
            CargarCandidatos();   // Carga candidatos en combo
        }
    }
}
