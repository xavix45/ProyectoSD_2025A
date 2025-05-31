using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Localidad
    {
        public int IdLocalidad { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return $"Localidad[Id={IdLocalidad}, Nombre={Nombre}]";
        }
    }
}
