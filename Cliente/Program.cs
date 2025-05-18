using System;
using System.Net.Sockets;
using System.Text;


namespace Cliente
{
    public class Program
    {
        static void Main()
        {
            TcpClient client = new TcpClient("127.0.0.1", 5000);
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[4096];
            int bytes = stream.Read(buffer, 0, buffer.Length);
            Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytes));

            while (true)
            {
                Console.WriteLine("\nSeleccione una opción:");
                Console.WriteLine("1. Asignar Mesa");
                Console.WriteLine("2. Registrar Datos");
                Console.WriteLine("3. Cerrar Mesa");
                Console.WriteLine("4. Salir");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine();
                string input = "";

                if (opcion == "4") break;

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese la fecha (YYYY-MM-DD): ");
                        string fecha = Console.ReadLine();
                        Console.Write("Ingrese la localidad: ");
                        string localidad = Console.ReadLine();
                        input = $"ASIGNACIONMESA|{fecha}|{localidad}";
                        break;

                    case "2":
                        Console.Write("Ingrese número de mesa: ");
                        string numMesa = Console.ReadLine();
                        Console.Write("Ingrese votos por candidatos separados por coma (ej. 50,30,20): ");
                        string votos = Console.ReadLine();
                        Console.Write("Ingrese votos en blanco: ");
                        string blancos = Console.ReadLine();
                        Console.Write("Ingrese votos nulos: ");
                        string nulos = Console.ReadLine();
                        input = $"REGISTRODATOS|{numMesa}|{votos}|{blancos}|{nulos}";
                        break;

                    case "3":
                        Console.Write("Ingrese número de mesa a cerrar: ");
                        string mesaCerrar = Console.ReadLine();
                        input = $"CIERREMESA|{mesaCerrar}";
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        continue;
                }

                byte[] envio = Encoding.UTF8.GetBytes(input);
                stream.Write(envio, 0, envio.Length);

                bytes = stream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("Respuesta: " + Encoding.UTF8.GetString(buffer, 0, bytes));
            }

            client.Close();
        }
    }
}

