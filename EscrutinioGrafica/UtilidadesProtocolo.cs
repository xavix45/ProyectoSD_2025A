// ************************************************************************
// Prueba 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Clase utilitaria para deserializar respuestas del servidor TCP a objetos de dominio.
// * Convierte cadenas del protocolo de texto en listas de entidades (Localidad, Candidato, Mesa).
// Recomendaciones:
// * Validar datos malformateados más profundamente (uso de expresiones regulares o sanitización).
// * Considerar soportar otros formatos como JSON para mayor robustez y escalabilidad.
// ************************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace EscrutinioGrafica
{
    public static class UtilidadesProtocolo
    {
        // Convierte la respuesta del servidor con localidades en una lista de objetos Localidad
        public static List<Localidad> DeserializarLocalidades(string respuesta)
        {
            var localidades = new List<Localidad>();

            // Validar que la respuesta empiece con "OK|"
            if (!respuesta.StartsWith("OK|"))
                return localidades; // Si no, devolver lista vacía

            // Remover el prefijo "OK|" para procesar sólo los datos
            string datos = respuesta.Substring(3);

            // Dividir la cadena por comas, cada ítem tiene formato "id:nombre"
            var items = datos.Split(',');

            foreach (var item in items)
            {
                // Separar el id y el nombre por el carácter ':'
                var partes = item.Split(':');
                if (partes.Length == 2 &&
                    int.TryParse(partes[0], out int id)) // Validar que el id sea un entero
                {
                    // Crear objeto Localidad y agregarlo a la lista
                    localidades.Add(new Localidad
                    {
                        IdLocalidad = id,
                        Nombre = partes[1]
                    });
                }
            }
            return localidades; // Devolver lista con localidades convertidas
        }

        // Convierte la respuesta del servidor con candidatos en lista de objetos Candidato
        public static List<Candidato> DeserializarCandidatos(string respuesta)
        {
            var candidatos = new List<Candidato>();

            if (!respuesta.StartsWith("OK|"))
                return candidatos;

            string datos = respuesta.Substring(3);
            var items = datos.Split(',');

            foreach (var item in items)
            {
                var partes = item.Split(':');
                if (partes.Length == 2 &&
                    int.TryParse(partes[0], out int id))
                {
                    candidatos.Add(new Candidato
                    {
                        IdCandidato = id,
                        Nombre = partes[1]
                    });
                }
            }
            return candidatos;
        }

        // Convierte la respuesta del servidor con mesas en lista de objetos Mesa
        public static List<Mesa> DeserializarMesas(string respuesta)
        {
            var mesas = new List<Mesa>();

            if (!respuesta.StartsWith("OK|"))
                return mesas;

            string datos = respuesta.Substring(3);
            var items = datos.Split(',');

            foreach (var item in items)
            {
                var partes = item.Split(':');
                if (partes.Length == 2 &&
                    int.TryParse(partes[0], out int id) &&
                    int.TryParse(partes[1], out int numero))
                {
                    mesas.Add(new Mesa
                    {
                        IdMesa = id,
                        NumeroMesa = numero
                    });
                }
            }
            return mesas;
        }
    }

}
