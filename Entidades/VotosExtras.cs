using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class VotosExtras
    {
        public int IdMesa { get; set; }
        public int Blancos { get; set; } = 0;
        public int Nulos { get; set; } = 0;
        public int Ausentes { get; set; } = 0;

        public override string ToString()
        {
            return $"VotosExtras[MesaId={IdMesa}, Blancos={Blancos}, Nulos={Nulos}, Ausentes={Ausentes}]";
        }
    }
}
