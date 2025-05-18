using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("\nComandos:");
                Console.WriteLine("1. ASIGNACIONMESA|2025-05-17|Quito");
                Console.WriteLine("2. REGISTRODATOS|1|50,30,20|5|2");
                Console.WriteLine("3. CIERREMESA|1");
                Console.WriteLine("4. SALIR");

                string input = Console.ReadLine();
                if (input == "SALIR") break;

                byte[] envio = Encoding.UTF8.GetBytes(input);
                stream.Write(envio, 0, envio.Length);

                bytes = stream.Read(buffer, 0, buffer.Length);
                Console.WriteLine("Respuesta: " + Encoding.UTF8.GetString(buffer, 0, bytes));
            }

            client.Close();
        }
    }
}

