// ************************************************************************
// Proyecto 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Clase que modela la relación entre una mesa electoral y un candidato.
// * Permite registrar y mostrar los votos asignados por mesa y candidato.
// Recomendaciones:
// * Validar que la propiedad `Candidato` esté correctamente inicializada antes de usarla.
// * Implementar interfaces como IEquatable si se requiere comparación personalizada.
// ************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MesaCandidato
    {
        // Identificador de la mesa a la que pertenece el registro
        public int IdMesa { get; set; }

        // Identificador del candidato vinculado a la mesa
        public int IdCandidato { get; set; }

        // Cantidad de votos obtenidos por el candidato en la mesa
        public int Votos { get; set; } = 0;

        // Referencia al objeto Candidato, útil para acceder a datos adicionales como el nombre
        public Candidato Candidato { get; set; }

        // Método sobrescrito para representar el objeto como cadena legible
        public override string ToString()
        {
            // Verifica si el candidato está definido para evitar null reference
            string nombre = Candidato != null ? Candidato.Nombre : "Desconocido";

            // Retorna una cadena descriptiva con los datos principales
            return $"MesaCandidato[MesaId={IdMesa}, CandidatoId={IdCandidato} ({nombre}), Votos={Votos}]";
        }
    }
}
