using System.Collections.Generic;

namespace Protocolo
{
    public class CierreMesa
    {
        public static string Cerrar(int numeroMesa)
        {
            var mesas = AsignacionMesa.GetMesasAsignadas();

            if (!mesas.ContainsKey(numeroMesa))
                return "ERROR: Mesa no encontrada";

            var mesa = mesas[numeroMesa];

            if (mesa.Cerrada)
                return "CERRADA";

            mesa.Cerrada = true;
            return "OK";
        }
    }
}
