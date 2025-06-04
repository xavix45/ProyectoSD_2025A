// ************************************************************************
// Proyecto 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Clase funcional para manejar la conexión a la base de datos SQL Server.
// * Permite abrir y cerrar la conexión de forma controlada desde otras capas.
// Recomendaciones:
// * Manejar excepciones al abrir/cerrar conexión para mejorar la robustez.
// * Considerar el uso de `using` o patrones de diseño como Singleton o Factory.
// ************************************************************************

using System;                      // Espacio de nombres para funcionalidades básicas del sistema.
using System.Collections.Generic;  // Colecciones genéricas, aunque no se utilizan aquí.
using System.Data.SqlClient;       // Necesario para trabajar con SQL Server.
using System.Data;                 // Contiene definiciones para acceder a datos.
using System.Linq;                 // LINQ para consultas a colecciones (no se usa en este código).
using System.Text;                 // Para manipulación de cadenas de texto (no usado).
using System.Threading.Tasks;      // Para programación asíncrona y paralela (no usado).

namespace CapaConexion              // Define el espacio de nombres para organización lógica del código.
{
    public class ConexionBD         // Clase que encapsula la conexión a la base de datos.
    {
        // Objeto SqlConnection inicializado con una cadena de conexión a una instancia local de SQL Server.
        public SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-U1OLQL1\\SQLEXPRESS;Initial Catalog=EleccionesSD;Integrated Security=True;");

        // Método que abre la conexión si está cerrada.
        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed) // Verifica si la conexión está cerrada.
                Conexion.Open();                           // Abre la conexión si está cerrada.
            return Conexion;                               // Retorna la conexión abierta.
        }

        // Método que cierra la conexión si está abierta.
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)    // Verifica si la conexión está abierta.
                Conexion.Close();                          // Cierra la conexión si está abierta.
            return Conexion;                               // Retorna la conexión cerrada.
        }
    }
}
