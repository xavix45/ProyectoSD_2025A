// ************************************************************************
// Prueba 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Interfaz gráfica para asignar una mesa electoral a una localidad y fecha específica.
// * Interactúa con el servidor TCP mediante una conexión persistente proporcionada por el formulario principal.
// Recomendaciones:
// * Desacoplar la lógica de negocio del formulario para permitir pruebas unitarias.
// * Validar campos adicionales como formato de fecha o disponibilidad de candidatos.
// ************************************************************************

using System;
using Entidades;
using System.Windows.Forms;
using System.Collections.Generic;

namespace EscrutinioGrafica
{
    public partial class AsignacionMesaForm : Form
    {
        // Referencia al formulario principal para usar la conexión TCP persistente
        private Escrutinio padre;
        List<Mesa> mesasPorLocalidad;

        // Constructor recibe el formulario principal como parámetro
        public AsignacionMesaForm(Escrutinio formularioPadre)
        {
            InitializeComponent();

            padre = formularioPadre;

            // Limita la fecha máxima seleccionable al día actual para evitar fechas futuras
            dtpFecha.MaxDate = DateTime.Now;
        }

        // Evento que se dispara cuando el formulario carga
        private void AsignacionMesaForm_Load(object sender, EventArgs e)
        {
            CargarLocalidades();  // Carga las localidades en el combo box
            lstCandidatos.Items.Clear(); // Limpia la lista de candidatos al iniciar
        }

        // Método que obtiene localidades desde el servidor usando la conexión persistente del padre y las carga en el combo
        private void CargarLocalidades()
        {
            // Enviar comando usando el método del formulario principal
            string respuesta = padre.EnviarComando("OBTENERLOCALIDADES");

            // Convierte la respuesta en lista de objetos Localidad
            List<Localidad> localidades = UtilidadesProtocolo.DeserializarLocalidades(respuesta);

            // Asigna la lista al combo para que el usuario pueda seleccionar
            cmbLocalidad.DataSource = localidades;
            cmbLocalidad.DisplayMember = "Nombre";    // Mostrar el nombre visible
            cmbLocalidad.ValueMember = "IdLocalidad"; // Valor interno del combo
            cmbLocalidadConsulta.DataSource = localidades; // Asignar también al combo de consulta si es necesario
        }

        // Evento que se activa al hacer clic en "Asignar mesa"
        private void btnAsignacionMesa_Click(object sender, EventArgs e)
        {
            // Validar que el usuario haya seleccionado una localidad
            if (cmbLocalidad.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una localidad.");
                return;
            }

            // Obtener valores seleccionados
            string localidad = ((Localidad)cmbLocalidad.SelectedItem).Nombre;
            DateTime fecha = dtpFecha.Value.Date;

            // Crear el comando para el servidor usando el protocolo definido
            string comando = $"ASIGNACIONMESA|{fecha:yyyy-MM-dd}|{localidad}";

            // Usar el método EnviarComando del formulario principal para comunicarse con el servidor
            string respuesta = padre.EnviarComando(comando);

            if (respuesta.StartsWith("OK|"))
            {
                // La respuesta tiene formato: OK|idMesa|numeroMesa|votantes
                var partes = respuesta.Split('|');
                txtNumeroMesa.Text = partes[2];         // Mostrar número de mesa asignado
                txtVotantesAsignados.Text = partes[3];  // Mostrar número de votantes

                // Obtener lista de candidatos para mostrar
                string respCandidatos = padre.EnviarComando("OBTENERCANDIDATOS");
                List<Candidato> candidatos = UtilidadesProtocolo.DeserializarCandidatos(respCandidatos);

                lstCandidatos.Items.Clear();
                foreach (var c in candidatos)
                {
                    lstCandidatos.Items.Add(c.Nombre);  // Agrega nombres a la lista visual
                }

                MessageBox.Show("Mesa asignada correctamente.");
            }
            else
            {
                // Si la respuesta no empieza con OK, mostrar mensaje de error
                MessageBox.Show($"Error al asignar mesa: {respuesta}");
            }
        }

        // Evento botón cerrar, cierra el formulario actual
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cb_LocalidadConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void cmbLocalidadConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocalidad.SelectedItem == null) return;

            int idLocalidad = ((Localidad)cmbLocalidad.SelectedItem).IdLocalidad;
            string comando = $"OBTENERMESASPORLOCALIDAD|{idLocalidad}";
            string respuesta = padre.EnviarComando(comando);
            mesasPorLocalidad = UtilidadesProtocolo.DeserializarMesas(respuesta);

            cmbNumeroMesa.DataSource = mesasPorLocalidad;
            cmbNumeroMesa.DisplayMember = "NumeroMesa";
            cmbNumeroMesa.ValueMember = "IdMesa";

            //// Mostrar votantes de la primera mesa (si hay alguna)
            //if (mesasPorLocalidad.Count > 0)
            //    txtVotantes.Text = mesasPorLocalidad[0].Votantes.ToString();
            //else
            //    txtVotantes.Text = "0";
        }

        private void btn_Consulta_Click(object sender, EventArgs e)
        {
            var mesa = (Mesa)cmbNumeroMesa.SelectedItem;
            // Obtener los votos extra (blancos, nulos, ausentes)
            string comando = $"CONSULTAR_MESA||{mesa.IdMesa}";
            string respuesta = padre.EnviarComando(comando);

            if (respuesta.StartsWith("OK|"))
            {
                // Parsear la respuesta para obtener los valores de los votos extras
                var partes = respuesta.Split('|');
                if (partes.Length == 4)
                {
                    bool estado = bool.Parse(partes[1]);
                    int blancos = int.Parse(partes[2]);
                    int nulos = int.Parse(partes[3]);
                    int ausentes = int.Parse(partes[4]);

                    tb_ConsultaMesa.Text = $"{estado} {blancos} {nulos} {ausentes}";
                }
            }
        }
    }

    
}
