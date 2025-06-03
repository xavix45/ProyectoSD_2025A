using Protocolo;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using CapaConexion;

namespace Servidor
{
    internal class Program
    {
        EscrutinioDAL escrutinioDAL = new EscrutinioDAL();
        static void Main()
        {
            // Reemplazar la inicialización con tipo de destino por una inicialización explícita  
            TcpListener listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("[Servidor] Escuchando en el puerto 5000...");

            while (true)
            {
                TcpClient cliente = listener.AcceptTcpClient();
                new Thread(() => ManejarCliente(cliente)).Start();
            }
        }

        static void ManejarCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream();
            byte[] mensajeInicial = Encoding.UTF8.GetBytes("Conectado al servidor Conejo Nacional Electoral Ficticio.\n");
            stream.Write(mensajeInicial, 0, mensajeInicial.Length);

            byte[] buffer = new byte[4096];
            int bytesLeidos;

            while ((bytesLeidos = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string data = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);
                string[] partes = data.Trim().Split('|');
                string comando = partes[0];
                string respuesta;

                if (comando == "ASIGNACIONMESA")
                {
                    respuesta = AsignacionMesa.Asignar(partes[1], partes[2]);
                }
                else if (comando == "REGISTRODATOS")
                {
                    respuesta = RegistroDatos.Registrar(
                        int.Parse(partes[1]),
                        Array.ConvertAll(partes[2].Split(','), int.Parse),
                        int.Parse(partes[3]),
                        int.Parse(partes[4])
                    );
                }
                else if (comando == "CIERREMESA")
                {
                    respuesta = CierreMesa.Cerrar(int.Parse(partes[1]));
                }
                else
                {
                    respuesta = "ERROR: Comando no reconocido";
                }

                byte[] salida = Encoding.UTF8.GetBytes(respuesta + "\n");
                stream.Write(salida, 0, salida.Length);
            }

            cliente.Close();
        }
    }
}
