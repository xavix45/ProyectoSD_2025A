
// ************************************************************************
// Proyecto 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Servidor TCP funcional que gestiona múltiples comandos del cliente en el contexto de un sistema de elecciones.
// * Soporta asignación de mesas, registro de votos, cierre y consulta de estadísticas vía red.
// Recomendaciones:
// * Implementar autenticación básica para los clientes que se conecten.
// * Considerar usar un protocolo binario o JSON para escalabilidad futura.
// * Añadir logs persistentes para monitorear el uso del servidor y depurar errores.
// ************************************************************************

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Negocio;
using Entidades;
using System.Collections.Generic;
using System.IO;


namespace ServidorTCP
{
    internal class Servidor
    {
        // Instancia de la capa de negocio para manejar la lógica electoral
        static EscrutinioCN negocio = new EscrutinioCN();

        static void Main()
        {
            // Crear el listener TCP que escuchará en todas las interfaces de red en el puerto 5000
            TcpListener listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            Console.WriteLine("[Servidor] Escuchando en el puerto 5000...");

            // Bucle infinito para aceptar conexiones entrantes continuamente
            while (true)
            {
                // Esperar (bloqueante) a que un cliente se conecte
                TcpClient cliente = listener.AcceptTcpClient();

                // Mostrar en consola la IP y puerto del cliente que se ha conectado
                Console.WriteLine("[Servidor] Cliente conectado desde " + cliente.Client.RemoteEndPoint);

                // Crear un nuevo hilo para manejar al cliente sin bloquear el listener principal
                new Thread(() => ManejarCliente(cliente)).Start();
            }
        }

        // Método que maneja la comunicación con un cliente específico
        static void ManejarCliente(TcpClient cliente)
        {
            NetworkStream stream = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                // Obtener el stream de red para enviar y recibir datos
                stream = cliente.GetStream();

                // Crear objetos para lectura y escritura de texto con codificación UTF8
                reader = new StreamReader(stream, Encoding.UTF8);
                writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                // Enviar mensaje de bienvenida al cliente cuando se conecta
                writer.WriteLine("Conectado al servidor del CNE Ficticio.");

                string linea;

                // Leer línea por línea mientras el cliente mantenga la conexión abierta
                while ((linea = reader.ReadLine()) != null)
                {
                    // Mostrar en consola el comando que se ha recibido del cliente
                    Console.WriteLine($"[Servidor] Comando recibido: {linea}");

                    // Separar el comando y sus parámetros usando '|' como delimitador
                    string[] partes = linea.Split('|');
                    string comando = partes[0];
                    string respuesta;

                    try
                    {
                        // Procesar el comando recibido según su tipo
                        switch (comando)
                        {
                            case "ASIGNACIONMESA":
                                // Extraer parámetros de fecha y localidad
                                DateTime fecha = DateTime.Parse(partes[1]);
                                string localidad = partes[2];

                                // Llamar a la capa de negocio para asignar mesa
                                var (resultado, mesa) = negocio.AsignarMesa(fecha, localidad);

                                // Preparar la respuesta según el resultado
                                respuesta = resultado == "OK"
                                    ? $"OK|{mesa.IdMesa}|{mesa.NumeroMesa}|{mesa.Votantes}"
                                    : $"ERROR|{resultado}";

                                Console.WriteLine($"[Servidor] Respuesta ASIGNACIONMESA: {respuesta}");
                                break;

                            case "REGISTRODATOS":
                                // Extraer parámetros para registrar votos
                                int idMesa = int.Parse(partes[1]);
                                List<int> votos = new List<int>(Array.ConvertAll(partes[2].Split(','), int.Parse));
                                int blancos = int.Parse(partes[3]);
                                int nulos = int.Parse(partes[4]);

                                // Registrar los datos y obtener la respuesta
                                respuesta = negocio.RegistrarDatos(idMesa, votos, blancos, nulos);

                                Console.WriteLine($"[Servidor] Respuesta REGISTRODATOS: {respuesta}");
                                break;
                            case "OBTENERVOTOSEXTRASMESA":
                                int idMesa1 = int.Parse(partes[1]);
                                var votosExtras = negocio.ObtenerVotosMesa(idMesa1); // Método que devuelve VotosExtras de la mesa
                                respuesta = $"OK|{votosExtras.Blancos}|{votosExtras.Nulos}|{votosExtras.Ausentes}";
                                break;

                            case "CIERREMESA":
                                // Cerrar mesa especificada
                                int idCerrar = int.Parse(partes[1]);
                                respuesta = negocio.CerrarMesa(idCerrar);

                                Console.WriteLine($"[Servidor] Respuesta CIERREMESA para IdMesa={idCerrar}: {respuesta}");
                                break;

                            case "ESTADISTICASMESA":
                                // Obtener estadísticas de una mesa
                                int idEst = int.Parse(partes[1]);
                                var reporte = negocio.ObtenerEstadisticasMesa(idEst);

                                if (reporte != null)
                                {
                                    // Formatear respuesta con datos y votos por candidato
                                    respuesta = $"OK|{reporte.NumeroMesa}|{reporte.Localidad}|{reporte.Votantes}|{reporte.Blancos}|{reporte.Nulos}|{reporte.Ausentes}|{reporte.TotalVotos}";
                                    foreach (var par in reporte.VotosPorCandidato)
                                    {
                                        respuesta += $"|{par.Key}:{par.Value}";
                                    }
                                }
                                else
                                {
                                    // Si no se encontró la mesa, enviar error
                                    respuesta = "ERROR|No encontrado";
                                }

                                Console.WriteLine($"[Servidor] Respuesta ESTADISTICASMESA: {respuesta}");
                                break;

                            case "OBTENERLOCALIDADES":
                                // Obtener lista de localidades
                                var localidades = negocio.ObtenerLocalidades();

                                // Construir respuesta concatenando id y nombre separados por comas
                                respuesta = "OK|" + string.Join(",", localidades.ConvertAll(l => $"{l.IdLocalidad}:{l.Nombre}"));

                                Console.WriteLine("[Servidor] Enviando lista localidades");
                                break;

                            case "OBTENERCANDIDATOS":
                                // Obtener lista de candidatos
                                var candidatos = negocio.ObtenerCandidatos();

                                // Formatear respuesta con id y nombre de candidatos
                                respuesta = "OK|" + string.Join(",", candidatos.ConvertAll(c => $"{c.IdCandidato}:{c.Nombre}"));

                                Console.WriteLine("[Servidor] Enviando lista candidatos");
                                break;

                            case "OBTENERMESASPORLOCALIDAD":
                                // Obtener mesas según localidad indicada
                                int idLoc = int.Parse(partes[1]);
                                var mesas = negocio.ObtenerMesasPorLocalidad(idLoc);

                                // Formatear respuesta con id y número de mesa
                                respuesta = "OK|" + string.Join(",", mesas.ConvertAll(m => $"{m.IdMesa}:{m.NumeroMesa}"));

                                Console.WriteLine($"[Servidor] Enviando mesas para localidad {idLoc}");
                                break;

                            case "ESTAMESACERRADA":
                                // Verificar si mesa está cerrada
                                bool cerrada = negocio.EstaMesaCerrada(int.Parse(partes[1]));
                                respuesta = cerrada ? "SI" : "NO";

                                Console.WriteLine($"[Servidor] Mesa {partes[1]} cerrada: {respuesta}");
                                break;

                            case "OBTENERVOTOSVALIDOS":
                                // Obtener votos válidos para candidato en una mesa
                                int mesaId = int.Parse(partes[1]);
                                int candidatoId = int.Parse(partes[2]);
                                int votosValidos = negocio.ObtenerVotosValidos(mesaId, candidatoId);

                                respuesta = $"OK|{votosValidos}";

                                Console.WriteLine($"[Servidor] Votos validos mesa {mesaId}, candidato {candidatoId}: {votosValidos}");
                                break;



                            case "GUARDARACTUALIZARVOTOS":
                                // Verificar si la mesa está cerrada antes de procesar el comando
                                int idMesa2 = int.Parse(partes[1]);
                                bool estaCerrada = negocio.EstaMesaCerrada(idMesa2);

                                if (estaCerrada)
                                {
                                    respuesta = "ERROR|La mesa ya está cerrada. No se pueden registrar votos.";
                                    Console.WriteLine($"[Servidor] Error: La mesa {idMesa2} está cerrada.");
                                    break;
                                }

                                // Obtener los parámetros del comando
                                int idCandidato = int.Parse(partes[2]);
                                int votosValidos1 = int.Parse(partes[3]);
                                int blancos1 = int.Parse(partes[4]);
                                int nulos1 = int.Parse(partes[5]);
                                int ausentes = int.Parse(partes[6]);

                                // Obtener el número de votantes en la mesa
                                int votantes = negocio.ObtenerVotantes(idMesa2);

                                // Validar que los votos no excedan el número de votantes
                                int totalVotos = votosValidos1 + blancos1 + nulos1 + ausentes;
                                if (totalVotos > votantes)
                                {
                                    respuesta = "ERROR|El número total de votos no puede exceder el número de votantes en la mesa.";
                                    Console.WriteLine("[Servidor] Error: Los votos superan el número de votantes.");
                                    break;
                                }

                                // Llamar al método de negocio para guardar o actualizar los votos
                                negocio.GuardarOActualizarVotos(idMesa2, idCandidato, votosValidos1, blancos1, nulos1, ausentes);

                                respuesta = "OK";
                                Console.WriteLine($"[Servidor] Votos actualizados para mesa {idMesa2}, candidato {idCandidato}");
                                break;


                            default:
                                // Si el comando no es reconocido, enviar error
                                respuesta = "ERROR|Comando no reconocido";

                                Console.WriteLine($"[Servidor] Comando no reconocido: {comando}");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Capturar cualquier excepción al procesar el comando y enviar error al cliente
                        respuesta = "ERROR|" + ex.Message;
                        Console.WriteLine($"[Servidor] Error al procesar comando {comando}: {ex.Message}");
                    }

                    try
                    {
                        // Enviar la respuesta formateada al cliente con salto de línea
                        writer.WriteLine(respuesta);
                    }
                    catch (IOException ioEx)
                    {
                        // Si hay error al enviar (cliente desconectado), se muestra y se sale del loop
                        Console.WriteLine($"[Servidor] Error al enviar respuesta: {ioEx.Message}");
                        break; // Salir del while
                    }
                }
            }
            catch (Exception ex)
            {
                // Capturar errores de conexión o lectura inesperados
                Console.WriteLine($"[Servidor] Error de conexión o lectura: {ex.Message}");
            }
            finally
            {
                // Cerrar todos los recursos para liberar memoria y conexión
                if (reader != null) reader.Close();
                if (writer != null) writer.Close();
                if (stream != null) stream.Close();
                if (cliente != null) cliente.Close();

                Console.WriteLine("[Servidor] Conexión con cliente finalizada.");
            }
        }
    }
}
