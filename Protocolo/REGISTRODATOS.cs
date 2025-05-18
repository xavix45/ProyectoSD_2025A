using System;
using System.Collections.Generic;
using System.Linq;

namespace Protocolo
{
    public class RegistroDatos
    {
        public static string Registrar(int numeroMesa, int[] votos, int blancos, int nulos)
        {
            var mesas = AsignacionMesa.GetMesasAsignadas();

            if (!mesas.ContainsKey(numeroMesa))
                return "ERROR: Mesa no encontrada";

            var mesa = mesas[numeroMesa];

            if (mesa.Cerrada)
                return "CERRADA";

            int totalVotos = votos.Sum() + blancos + nulos;
            if (totalVotos > mesa.Votantes)
                return "ERROR: Supera el numero de votantes asignados";

            for (int i = 0; i < mesa.Candidatos.Count; i++)
                mesa.Votos[mesa.Candidatos[i]] += votos[i];

            mesa.Blancos += blancos;
            mesa.Nulos += nulos;

            return "OK";
        }
    }
}
