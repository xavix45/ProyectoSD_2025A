// ************************************************************************
// Proyecto 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Interfaz gráfica que permite cerrar una mesa electoral previamente asignada.
// * Se conecta al servidor TCP para consultar localidades, mesas y ejecutar cierre.
// Recomendaciones:
// * Mostrar confirmación visual de que la mesa estaba abierta antes del cierre.
// * Bloquear intento de cierre si la mesa ya fue cerrada (validación previa).
// ************************************************************************

using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Entidades;

    namespace EscrutinioGrafica
    {
        public partial class CerrarMesaForm : Form
        {
            private Escrutinio padre;              // Referencia al formulario principal para usar conexión persistente
            List<Localidad> localidades;
            List<Mesa> mesasPorLocalidad;

            // Constructor modificado para recibir referencia al formulario principal
            public CerrarMesaForm(Escrutinio formularioPadre)
            {
                InitializeComponent();
                padre = formularioPadre;
            }

            // Evento carga del formulario
            private void CerrarMesaForm_Load(object sender, EventArgs e)
            {
                CargarLocalidades();
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

            // Evento cambio selección localidad
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
            }

            // Botón para cerrar la mesa seleccionada
            private void btnCerrarMesa_Click(object sender, EventArgs e)
            {
                if (cmbNumeroMesa.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione una mesa.");
                    return;
                }

                int idMesa = ((Mesa)cmbNumeroMesa.SelectedItem).IdMesa;
                string comando = $"CIERREMESA|{idMesa}";

                // Enviar comando para cerrar la mesa
                string respuesta = padre.EnviarComando(comando);

                if (respuesta == "OK")
                {
                    MessageBox.Show("Mesa cerrada correctamente.");
                }
                else
                {
                    MessageBox.Show($"Error al cerrar mesa: {respuesta}");
                }
            }

            // Botón cancelar/cerrar ventana
            private void btnCerrarVentana_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        }


    }
