using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Validaciones
    {
        public static bool EsFechaValida(DateTime fecha)
        {
            return fecha <= DateTime.Today; // No permitir fechas futuras, por ejemplo
        }

        public static bool EsLocalidadValida(string localidad)
        {
            return !string.IsNullOrWhiteSpace(localidad);
        }

        public static bool ValidarVotos(List<int> votos, int votantes, int blancos, int nulos)
        {
            int suma = 0;
            foreach (var v in votos)
            {
                if (v < 0) return false;
                suma += v;
            }
            if (blancos < 0 || nulos < 0) return false;
            suma += blancos + nulos;
            return suma <= votantes;
        }
    }
}
