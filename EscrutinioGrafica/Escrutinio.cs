using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscrutinioGrafica
{
    public partial class Escrutinio : Form
    {
        // Miembros para la conexión TCP persistente
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public Escrutinio()
        {
            InitializeComponent();
            ConectarServidor();
        }

        // Conecta al servidor y prepara lectura/escritura
        private void ConectarServidor()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Leer mensaje de bienvenida
                string bienvenida = reader.ReadLine();
                Console.WriteLine("Servidor dice: " + bienvenida);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar al servidor: " + ex.Message);
                // Opcional: cerrar app o deshabilitar botones
            }
        }

        // Método para enviar comandos y recibir respuesta
        public string EnviarComando(string comando)
        {
            try
            {
                writer.WriteLine(comando);
                string respuesta = reader.ReadLine();
                return respuesta ?? "ERROR: respuesta vacía";
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        // Pasar la referencia de este formulario a los hijos para que usen EnviarComando

        private void btnAsignacion_Click(object sender, EventArgs e)
        {
            var asignacionMesa = new AsignacionMesaForm(this);
            asignacionMesa.ShowDialog();
        }

        private void btnRegistroDatos_Click(object sender, EventArgs e)
        {
            var registro = new RegistroDatosForm(this);
            registro.ShowDialog();
        }

        private void btnCerrarMesa_Click(object sender, EventArgs e)
        {
            var cerrar = new CerrarMesaForm(this);
            cerrar.ShowDialog();
        }

        // Cerrar conexión al cerrar el formulario principal
        private void Escrutinio_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                reader?.Close();
                writer?.Close();
                stream?.Close();
                client?.Close();
            }
            catch { }
        }
    }

}
