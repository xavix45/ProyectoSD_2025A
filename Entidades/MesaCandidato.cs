using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MesaCandidato
    {
        public int IdMesa { get; set; }
        public int IdCandidato { get; set; }
        public int Votos { get; set; } = 0;

        public Candidato Candidato { get; set; }

        public override string ToString()
        {
            string nombre = Candidato != null ? Candidato.Nombre : "Desconocido";
            return $"MesaCandidato[MesaId={IdMesa}, CandidatoId={IdCandidato} ({nombre}), Votos={Votos}]";
        }
    }
}
