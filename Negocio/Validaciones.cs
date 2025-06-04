// ************************************************************************
// Proyecto 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Clase estática que centraliza validaciones relacionadas con el proceso electoral.
// * Mejora la mantenibilidad y reutilización del código de reglas de negocio.
// Recomendaciones:
// * Considerar usar excepciones personalizadas para validaciones fallidas.
// * Añadir validaciones por rango para valores extremos (e.g., máximo de votantes permitidos).
// ************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    // Clase estática para validaciones generales del dominio electoral
    public static class Validaciones
    {
        // Verifica que la fecha no sea futura (válida si es hoy o en el pasado)
        public static bool EsFechaValida(DateTime fecha)
        {
            return fecha <= DateTime.Today;
        }

        // Verifica que la localidad no sea nula, vacía ni solo espacios
        public static bool EsLocalidadValida(string localidad)
        {
            return !string.IsNullOrWhiteSpace(localidad);
        }

        // Valida la suma total de votos: deben ser no negativos y no exceder el número de votantes
        public static bool ValidarVotos(List<int> votos, int votantes, int blancos, int nulos)
        {
            int suma = 0;

            foreach (var v in votos)
            {
                if (v < 0) return false; // No se permiten votos negativos
                suma += v;
            }

            if (blancos < 0 || nulos < 0) return false; // Validar blancos y nulos

            suma += blancos + nulos;

            return suma <= votantes; // Total no puede exceder los votantes registrados
        }
    }
}
