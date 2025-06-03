using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Negocio;
using Entidades;
using System.Collections.Generic;


namespace Servidor
{
    internal class Program
    {
        // Instancia de la capa de negocio para manejar las operaciones del sistema de escrutinio
        static EscrutinioCN negocio = new EscrutinioCN();

        static void Main()
        {
            // Crear un servidor TCP que escucha en todas las IPs locales (IPAddress.Any) y el puerto 5000
            TcpListener listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("[Servidor] Escuchando en el puerto 5000...");

            // Bucle infinito para aceptar múltiples clientes concurrentes
            while (true)
            {
                TcpClient cliente = listener.AcceptTcpClient();

                // Se crea un nuevo hilo para cada cliente que se conecta
                new Thread(() => ManejarCliente(cliente)).Start();
            }
        }

        // Maneja las solicitudes de cada cliente conectado
        static void ManejarCliente(TcpClient cliente)
        {
            NetworkStream stream = cliente.GetStream(); // Flujo de datos para comunicación con el cliente

            // Mensaje de bienvenida
            byte[] bienvenida = Encoding.UTF8.GetBytes("Conectado al servidor del CNE Ficticio.\n");
            stream.Write(bienvenida, 0, bienvenida.Length);

            byte[] buffer = new byte[4096]; // Buffer para lectura de datos
            int bytesLeidos;

            // Bucle que recibe datos del cliente hasta que se cierre la conexión
            while ((bytesLeidos = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string data = Encoding.UTF8.GetString(buffer, 0, bytesLeidos).Trim();

                // Se separa el comando recibido usando "|" como delimitador
                string[] partes = data.Split('|');
                string comando = partes[0];
                string respuesta;

                try
                {
                    // Cada comando realiza una función específica según la lógica de negocio
                    switch (comando)
                    {
                        case "ASIGNACIONMESA":
                            DateTime fecha = DateTime.Parse(partes[1]);
                            string localidad = partes[2];
                            var (resultado, mesa) = negocio.AsignarMesa(fecha, localidad);

                            // Si todo salió bien, se responde con los datos de la mesa asignada
                            respuesta = resultado == "OK"
                                ? $"OK|{mesa.IdMesa}|{mesa.NumeroMesa}|{mesa.Votantes}"
                                : $"ERROR|{resultado}";
                            break;

                        case "REGISTRODATOS":
                            int idMesa = int.Parse(partes[1]);
                            List<int> votos = new List<int>(Array.ConvertAll(partes[2].Split(','), int.Parse));
                            int blancos = int.Parse(partes[3]);
                            int nulos = int.Parse(partes[4]);
                            respuesta = negocio.RegistrarDatos(idMesa, votos, blancos, nulos);
                            break;

                        case "CIERREMESA":
                            respuesta = negocio.CerrarMesa(int.Parse(partes[1]));
                            break;

                        case "ESTADISTICASMESA":
                            int id = int.Parse(partes[1]);
                            var reporte = negocio.ObtenerEstadisticasMesa(id);

                            if (reporte != null)
                            {
                                // Se arma la respuesta con los datos principales
                                respuesta = $"OK|{reporte.NumeroMesa}|{reporte.Localidad}|{reporte.Votantes}|{reporte.Blancos}|{reporte.Nulos}|{reporte.Ausentes}|{reporte.TotalVotos}";

                                // Se agregan los votos por candidato como clave:valor separados por |
                                foreach (var par in reporte.VotosPorCandidato)
                                {
                                    respuesta += $"|{par.Key}:{par.Value}";
                                }
                            }
                            else
                            {
                                respuesta = "ERROR|No encontrado";
                            }
                            break;

                        case "OBTENERLOCALIDADES":
                            var localidades = negocio.ObtenerLocalidades();
                            // Se devuelve la lista como id:nombre separados por comas
                            respuesta = "OK|" + string.Join(",", localidades.ConvertAll(l => $"{l.IdLocalidad}:{l.Nombre}"));
                            break;

                        case "OBTENERCANDIDATOS":
                            var candidatos = negocio.ObtenerCandidatos();
                            respuesta = "OK|" + string.Join(",", candidatos.ConvertAll(c => $"{c.IdCandidato}:{c.Nombre}"));
                            break;

                        case "OBTENERMESASPORLOCALIDAD":
                            int idLoc = int.Parse(partes[1]);
                            var mesas = negocio.ObtenerMesasPorLocalidad(idLoc);
                            respuesta = "OK|" + string.Join(",", mesas.ConvertAll(m => $"{m.IdMesa}:{m.NumeroMesa}"));
                            break;

                        case "ESTAMESACERRADA":
                            bool cerrada = negocio.EstaMesaCerrada(int.Parse(partes[1]));
                            respuesta = cerrada ? "SI" : "NO";
                            break;

                        case "OBTENERVOTOSVALIDOS":
                            int mesaId = int.Parse(partes[1]);
                            int candidatoId = int.Parse(partes[2]);
                            int votosValidos = negocio.ObtenerVotosValidos(mesaId, candidatoId);
                            respuesta = $"OK|{votosValidos}";
                            break;

                        case "GUARDARACTUALIZARVOTOS":
                            negocio.GuardarOActualizarVotos(
                                int.Parse(partes[1]), // idMesa
                                int.Parse(partes[2]), // idCandidato
                                int.Parse(partes[3]), // votos
                                int.Parse(partes[4]), // blancos
                                int.Parse(partes[5]), // nulos
                                int.Parse(partes[6])  // ausentes
                            );
                            respuesta = "OK";
                            break;

                        default:
                            respuesta = "ERROR|Comando no reconocido";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Captura de errores en el servidor
                    respuesta = "ERROR|" + ex.Message;
                }

                // Se envía la respuesta al cliente
                byte[] salida = Encoding.UTF8.GetBytes(respuesta + "\n");
                stream.Write(salida, 0, salida.Length);
            }
        }
    }
}
