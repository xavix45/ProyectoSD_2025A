using System.Collections.Generic;

namespace Protocolo
{
    public class Mesa
    {
        public int Numero { get; set; }
        public string Fecha { get; set; }
        public string Localidad { get; set; }
        public int Votantes { get; set; } = 100;
        public List<string> Candidatos { get; set; }
        // El error se debe a que la inicialización de objetos con tipo de destino (new()) no está disponible en C# 7.3.
        // Debes especificar el tipo explícitamente al inicializar el diccionario.
        public Dictionary<string, int> Votos { get; set; } = new Dictionary<string, int>();
        public int Blancos { get; set; } = 0;
        public int Nulos { get; set; } = 0;
        public bool Cerrada { get; set; } = false;
    }
}
