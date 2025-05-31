using System;
using System.Collections.Generic;
using Negocio;
using Entidades;

namespace COMPROBACION
{
    class Program
    {
        static EscrutinioCN negocio = new EscrutinioCN();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Escrutinio Electoral - Pruebas ===\n");

            // Obtener localidades
            Console.WriteLine("Localidades disponibles:");
            var localidades = negocio.ObtenerLocalidades();
            foreach (var loc in localidades)
            {
                Console.WriteLine($"- {loc.IdLocalidad}: {loc.Nombre}");
            }
            Console.WriteLine();

            // Obtener candidatos
            Console.WriteLine("Candidatos disponibles:");
            var candidatos = negocio.ObtenerCandidatos();
            foreach (var cand in candidatos)
            {
                Console.WriteLine($"- {cand.IdCandidato}: {cand.Nombre}");
            }
            Console.WriteLine();

            // Asignar mesa a localidad y fecha
            Console.Write("Ingrese localidad para asignar mesa: ");
            string localidadInput = Console.ReadLine();

            Console.Write("Ingrese fecha (yyyy-MM-dd): ");
            DateTime fecha;
            while (!DateTime.TryParse(Console.ReadLine(), out fecha))
            {
                Console.Write("Fecha inválida, intente nuevamente (yyyy-MM-dd): ");
            }

            var (resultadoAsignacion, mesaAsignada) = negocio.AsignarMesa(fecha, localidadInput);

            if (resultadoAsignacion == "OK")
            {
                Console.WriteLine($"Mesa asignada correctamente:");
                Console.WriteLine($"IdMesa: {mesaAsignada.IdMesa}, NumeroMesa: {mesaAsignada.NumeroMesa}, Votantes: {mesaAsignada.Votantes}");
            }
            else
            {
                Console.WriteLine($"Error al asignar mesa: {resultadoAsignacion}");
                return;
            }
            Console.WriteLine();

            // Ingresar votos
            Console.WriteLine("Ingrese votos por candidato separados por coma, en orden:");
            for (int i = 0; i < candidatos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {candidatos[i].Nombre}");
            }
            Console.Write("Votos (ejemplo: 10,20,15,5,0): ");
            string votosStr = Console.ReadLine();

            // Convertir a lista de enteros
            List<int> votos = new List<int>();
            try
            {
                string[] partes = votosStr.Split(',');
                foreach (var v in partes)
                    votos.Add(int.Parse(v.Trim()));
            }
            catch
            {
                Console.WriteLine("Formato de votos inválido.");
                return;
            }

            Console.Write("Ingrese votos blancos: ");
            int blancos = int.Parse(Console.ReadLine());

            Console.Write("Ingrese votos nulos: ");
            int nulos = int.Parse(Console.ReadLine());

            string resultadoRegistro = negocio.RegistrarDatos(mesaAsignada.IdMesa, votos, blancos, nulos);
            Console.WriteLine($"Resultado registro votos: {resultadoRegistro}");
            Console.WriteLine();

            // Obtener estadísticas de la mesa
            var reporte = negocio.ObtenerEstadisticasMesa(mesaAsignada.IdMesa);
            if (reporte != null)
            {
                Console.WriteLine("=== Estadísticas de la Mesa ===");
                Console.WriteLine($"Mesa Nº: {reporte.NumeroMesa}");
                Console.WriteLine($"Localidad: {reporte.Localidad}");
                Console.WriteLine($"Fecha: {reporte.Fecha:yyyy-MM-dd}");
                Console.WriteLine($"Votantes: {reporte.Votantes}");
            }
            else
            {
                Console.WriteLine("No se encontraron estadísticas para la mesa.");
            }
            Console.WriteLine();

            // Cerrar la mesa
            Console.Write("¿Desea cerrar la mesa? (S/N): ");
            string cerrar = Console.ReadLine().Trim().ToUpper();
            if (cerrar == "S")
            {
                string resultadoCierre = negocio.CerrarMesa(mesaAsignada.IdMesa);
                Console.WriteLine($"Resultado cierre mesa: {resultadoCierre}");
            }

            Console.WriteLine("\nPrueba finalizada. Presione cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}
