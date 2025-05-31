using System;
using System.Collections.Generic;

namespace Negocio
{
    public class ReporteMesaDTO
    {
        public int IdMesa { get; set; }
        public int NumeroMesa { get; set; }
        public string Localidad { get; set; }
        public DateTime Fecha { get; set; }
        public int Votantes { get; set; }
        public int Blancos { get; set; }
        public int Nulos { get; set; }
        public int Ausentes { get; set; }
        public int TotalVotos { get; set; }
        public Dictionary<string, int> VotosPorCandidato { get; set; } = new Dictionary<string, int>();
    }
}
