using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Mesa
    {
        public int IdMesa { get; set; }
        public DateTime FechaAsignada { get; set; }
        public Localidad Localidad { get; set; }
        public int NumeroMesa { get; set; }
        public int Votantes { get; set; } = 100;
        public bool Cerrada { get; set; } = false;

        public List<MesaCandidato> MesaCandidatos { get; set; } = new List<MesaCandidato>();
        public VotosExtras VotosExtras { get; set; } = new VotosExtras();

        public override string ToString()
        {
            //var sb = new StringBuilder();
            //sb.AppendLine($"{NumeroMesa}\n");
            //sb.AppendLine("Candidatos y votos:");
            //foreach (var mc in MesaCandidatos)
            //{
            //    sb.AppendLine($" - {mc}");
            //}
            //sb.AppendLine($"Votos Extras: {VotosExtras}");
            //return sb.ToString();
            return $"{NumeroMesa}";
        }
    }
}
