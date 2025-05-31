using System;
using System.Collections.Generic;
using CapaConexion;
using Entidades;

namespace Negocio
{
    public class EscrutinioCN
    {
        private EscrutinioDAL dal = new EscrutinioDAL();

        // Asignar una mesa para una localidad y fecha
        public (string Resultado, Mesa MesaAsignada) AsignarMesa(DateTime fecha, string localidad)
        {
            int idMesa, numeroMesa, votantes;
            string resultado = dal.AsignarMesa(fecha, localidad, out idMesa, out numeroMesa, out votantes);

            if (resultado == "OK")
            {
                // Crear objeto Mesa con datos asignados
                var mesa = new Mesa
                {
                    IdMesa = idMesa,
                    FechaAsignada = fecha,
                    NumeroMesa = numeroMesa,
                    Votantes = votantes,
                    Localidad = new Localidad { Nombre = localidad }
                    // Opcional: cargar candidatos desde BD o dejar vacío
                };
                return (resultado, mesa);
            }
            return (resultado, null);
        }

        // Registrar votos en la mesa
        public string RegistrarDatos(int idMesa, List<int> votosPorCandidato, int blancos, int nulos)
        {
            if (votosPorCandidato == null || votosPorCandidato.Count == 0)
                return "ERROR: Lista de votos vacía";

            // Validar valores negativos
            foreach (var v in votosPorCandidato)
            {
                if (v < 0)
                    return "ERROR: Votos negativos no permitidos";
            }
            if (blancos < 0 || nulos < 0)
                return "ERROR: Votos blancos o nulos negativos no permitidos";

            // Convertir lista a CSV
            string votosCsv = string.Join(",", votosPorCandidato);

            // Llamar DAL
            return dal.RegistrarDatos(idMesa, votosCsv, blancos, nulos);
        }

        // Cerrar mesa para evitar más registros
        public string CerrarMesa(int idMesa)
        {
            return dal.CerrarMesa(idMesa);
        }

        // Obtener estadísticas generales por mesa
        public Mesa ObtenerEstadisticasMesa(int idMesa)
        {
            // Aquí se podría llamar a un método DAL para obtener datos complejos
            // Por simplicidad, supondremos que tienes un método que devuelve datos mapeados
            // Ejemplo:
            // return dal.ObtenerMesaCompleta(idMesa);

            throw new NotImplementedException("Implementar método para obtener estadísticas desde DAL");
        }

        
    }
}
