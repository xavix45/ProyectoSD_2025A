using System;
using System.Collections.Generic;
using System.Linq;

namespace Protocolo
{
    public class AsignacionMesa
    {
        private static Dictionary<string, Queue<int>> mesasDisponibles = new Dictionary<string, Queue<int>>()
           {
               { "Quito", new Queue<int>(new[] { 1, 2, 3 }) }
           };

        private static Dictionary<int, Mesa> mesasAsignadas = new Dictionary<int, Mesa>();

        public static List<string> Candidatos { get; } = new List<string>() { "Alice", "Bob", "Charlie" };

        public static string Asignar(string fecha, string localidad)
        {
            if (!mesasDisponibles.ContainsKey(localidad) || mesasDisponibles[localidad].Count == 0)
                return "ERROR: No hay mesas disponibles";

            int numero = mesasDisponibles[localidad].Dequeue();
            mesasAsignadas[numero] = new Mesa
            {
                Numero = numero,
                Fecha = fecha,
                Localidad = localidad,
                Votantes = 100,
                Candidatos = new List<string>(Candidatos),
                Votos = new Dictionary<string, int>()
            };

            foreach (var candidato in Candidatos)
                mesasAsignadas[numero].Votos[candidato] = 0;

            return $"MESA:{numero}|VOTANTES:100|CANDIDATOS:{string.Join(";", Candidatos)}";
        }

        public static Dictionary<int, Mesa> GetMesasAsignadas() => mesasAsignadas;
    }
}
