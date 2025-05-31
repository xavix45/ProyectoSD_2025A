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
        public ReporteMesaDTO ObtenerEstadisticasMesa(int idMesa)
        {
            // Llamar al DAL para obtener la mesa completa
            var datosMesa = dal.ObtenerDatosMesaCompleta(idMesa);
            if (datosMesa == null)
                return null;

            var reporte = new ReporteMesaDTO
            {
                IdMesa = datosMesa.Mesa.IdMesa,
                NumeroMesa = datosMesa.Mesa.NumeroMesa,
                Fecha = datosMesa.Mesa.FechaAsignada,
                Localidad = datosMesa.Mesa.Localidad.Nombre,
                Votantes = datosMesa.Mesa.Votantes,
                Blancos = datosMesa.VotosExtras.Blancos,
                Nulos = datosMesa.VotosExtras.Nulos,
                Ausentes = datosMesa.VotosExtras.Ausentes,
                TotalVotos = 0,
                VotosPorCandidato = new Dictionary<string, int>()
            };

            int sumaVotos = 0;
            foreach (var mc in datosMesa.Mesa.MesaCandidatos)
            {
                reporte.VotosPorCandidato.Add(mc.Candidato.Nombre, mc.Votos);
                sumaVotos += mc.Votos;
            }
            reporte.TotalVotos = sumaVotos;

            return reporte;
        }

        // Obtener todas las localidades
        public List<Localidad> ObtenerLocalidades()
        {
            return dal.ObtenerLocalidades();
        }

        // Obtener todos los candidatos
        public List<Candidato> ObtenerCandidatos()
        {
            return dal.ObtenerCandidatos();
        }
    }
}
