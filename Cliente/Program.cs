// ClienteTCP.cs
using System;
using System.Net.Sockets;
using System.Text;

public static class ClienteTCP
{
    private static string ipServidor = "127.0.0.1";
    private static int puerto = 5000;

    public static string EnviarComando(string comando)
    {
        TcpClient cliente = new TcpClient(ipServidor, puerto);
        NetworkStream stream = cliente.GetStream();
        {
            byte[] datos = Encoding.UTF8.GetBytes(comando);
            stream.Write(datos, 0, datos.Length);

            byte[] buffer = new byte[4096];
            int bytesLeidos = stream.Read(buffer, 0, buffer.Length);

            return Encoding.UTF8.GetString(buffer, 0, bytesLeidos).Trim();
        }
    }
}
