// ************************************************************************
// Proyecto 01 
// Sabina Alomoto Xavier Anatoa
// Fecha de realización: 17/05/2025 
// Fecha de entrega: 03/06/2025 
// Resultados:
// * Clase DAL que se conecta a SQL Server para ejecutar procedimientos relacionados al escrutinio electoral.
// * Permite asignar mesas, registrar votos, cerrar mesas y obtener datos relevantes de la base de datos.
// Recomendaciones:
// * Incluir control de errores más específico y registrar errores en logs.
// * Considerar el uso de patrones de diseño como Repository o Unit of Work para separar la lógica de datos.
// ************************************************************************

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Entidades; // Referencia a las clases de entidades del dominio

namespace CapaConexion
{
    public class EscrutinioDAL
    {
        // Objeto para gestionar la conexión a la base de datos
        private ConexionBD conexion = new ConexionBD();

        // Objeto SqlCommand reutilizable para ejecutar comandos SQL
        private SqlCommand comando;

        // Lector para leer datos retornados por comandos SQL
        private SqlDataReader leer;

        public EscrutinioDAL()
        {
            comando = new SqlCommand(); // Inicialización del comando en el constructor
        }

        // Método para asignar una mesa electoral mediante un procedimiento almacenado
        public string AsignarMesa(DateTime fecha, string localidad, out int idMesa, out int numeroMesa, out int votantes)
        {
            idMesa = 0; numeroMesa = 0; votantes = 0;
            string resultado = "";

            try
            {
                SqlConnection conn = conexion.AbrirConexion();
                comando.Connection = conn;

                comando.CommandText = "AsignarMesa"; // Nombre del SP
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();

                // Parámetros de entrada
                comando.Parameters.AddWithValue("@Fecha", fecha);
                comando.Parameters.AddWithValue("@Localidad", localidad);

                // Parámetros de salida
                var paramIdMesa = new SqlParameter("@IdMesa", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var paramNumeroMesa = new SqlParameter("@NumeroMesa", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var paramVotantes = new SqlParameter("@Votantes", SqlDbType.Int) { Direction = ParameterDirection.Output };

                comando.Parameters.Add(paramIdMesa);
                comando.Parameters.Add(paramNumeroMesa);
                comando.Parameters.Add(paramVotantes);

                comando.ExecuteNonQuery();

                // Recuperar valores de salida
                idMesa = (int)paramIdMesa.Value;
                numeroMesa = (int)paramNumeroMesa.Value;
                votantes = (int)paramVotantes.Value;

                resultado = "OK";
            }
            catch (SqlException ex)
            {
                resultado = "ERROR: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion(); // Liberar recursos
            }

            return resultado;
        }

        // Método para registrar los votos por mesa
        public string RegistrarDatos(int idMesa, string votosCsv, int blancos, int nulos)
        {
            string resultado = "";
            try
            {
                SqlConnection conn = conexion.AbrirConexion();
                comando.Connection = conn;

                comando.CommandText = "RegistrarDatos";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();

                // Agregar parámetros
                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                comando.Parameters.AddWithValue("@Votos", votosCsv);
                comando.Parameters.AddWithValue("@Blancos", blancos);
                comando.Parameters.AddWithValue("@Nulos", nulos);

                // Parámetro de salida
                var paramRespuesta = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 50) { Direction = ParameterDirection.Output };
                comando.Parameters.Add(paramRespuesta);

                comando.ExecuteNonQuery();
                resultado = paramRespuesta.Value.ToString();
            }
            catch (SqlException ex)
            {
                resultado = "ERROR: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return resultado;
        }

        // Método que cierra una mesa electoral, usando SP
        public string CerrarMesa(int idMesa)
        {
            string resultado = "";
            ConexionBD conexion = new ConexionBD();

            try
            {
                using (SqlConnection conn = conexion.AbrirConexion())
                {
                    using (SqlCommand comando = new SqlCommand("CerrarMesa", conn))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.Clear();
                        comando.Parameters.AddWithValue("@IdMesa", idMesa);

                        var paramRespuesta = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 10) { Direction = ParameterDirection.Output };
                        comando.Parameters.Add(paramRespuesta);

                        comando.ExecuteNonQuery();
                        resultado = paramRespuesta.Value.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                resultado = "ERROR: " + ex.Message;
            }

            return resultado;
        }

        // Obtiene la lista de localidades disponibles
        public List<Localidad> ObtenerLocalidades()
        {
            List<Localidad> localidades = new List<Localidad>();
            string query = "SELECT IdLocalidad, Nombre FROM Localidad ORDER BY Nombre";

            SqlCommand cmd = new SqlCommand(query, conexion.AbrirConexion());
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        localidades.Add(new Localidad
                        {
                            IdLocalidad = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return localidades;
        }

        // Retorna todos los candidatos
        public List<Candidato> ObtenerCandidatos()
        {
            List<Candidato> candidatos = new List<Candidato>();
            string query = "SELECT IdCandidato, Nombre FROM Candidato ORDER BY Nombre";

            SqlCommand cmd = new SqlCommand(query, conexion.AbrirConexion());
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        candidatos.Add(new Candidato
                        {
                            IdCandidato = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
            finally
            {
                conexion.CerrarConexion();
            }
            return candidatos;
        }

        

        // Obtiene todos los datos detallados de una mesa
        public DatosMesaCompleta ObtenerDatosMesaCompleta(int idMesa)
        {
            DatosMesaCompleta resultado = new DatosMesaCompleta { Mesa = null, VotosExtras = new VotosExtras() };

            SqlConnection conn = conexion.AbrirConexion();

            try
            {
                // Consulta de datos de mesa + localidad
                string queryMesa = @"
                    SELECT m.IdMesa, m.FechaAsignada, m.NumeroMesa, m.Votantes, m.Cerrada,
                           l.IdLocalidad, l.Nombre
                    FROM Mesa m
                    INNER JOIN Localidad l ON m.IdLocalidad = l.IdLocalidad
                    WHERE m.IdMesa = @IdMesa";

                using (SqlCommand cmdMesa = new SqlCommand(queryMesa, conn))
                {
                    cmdMesa.Parameters.AddWithValue("@IdMesa", idMesa);
                    using (SqlDataReader reader = cmdMesa.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var localidad = new Localidad
                            {
                                IdLocalidad = reader.GetInt32(5),
                                Nombre = reader.GetString(6)
                            };

                            resultado.Mesa = new Mesa
                            {
                                IdMesa = reader.GetInt32(0),
                                FechaAsignada = reader.GetDateTime(1),
                                NumeroMesa = reader.GetInt32(2),
                                Votantes = reader.GetInt32(3),
                                Cerrada = reader.GetBoolean(4),
                                Localidad = localidad,
                                MesaCandidatos = new List<MesaCandidato>()
                            };
                        }
                        else return null;
                    }
                }

                // Consulta candidatos con votos
                string queryCandidatos = @"
                    SELECT mc.IdCandidato, mc.Votos, c.Nombre
                    FROM MesaCandidato mc
                    INNER JOIN Candidato c ON mc.IdCandidato = c.IdCandidato
                    WHERE mc.IdMesa = @IdMesa";

                using (SqlCommand cmdCandidatos = new SqlCommand(queryCandidatos, conn))
                {
                    cmdCandidatos.Parameters.AddWithValue("@IdMesa", idMesa);
                    using (SqlDataReader reader = cmdCandidatos.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var mc = new MesaCandidato
                            {
                                IdMesa = idMesa,
                                IdCandidato = reader.GetInt32(0),
                                Votos = reader.GetInt32(1),
                                Candidato = new Candidato
                                {
                                    IdCandidato = reader.GetInt32(0),
                                    Nombre = reader.GetString(2)
                                }
                            };
                            resultado.Mesa.MesaCandidatos.Add(mc);
                        }
                    }
                }

                // Consulta votos extras
                string queryExtras = "SELECT Blancos, Nulos, Ausentes FROM VotosExtras WHERE IdMesa = @IdMesa";

                using (SqlCommand cmdExtras = new SqlCommand(queryExtras, conn))
                {
                    cmdExtras.Parameters.AddWithValue("@IdMesa", idMesa);
                    using (SqlDataReader reader = cmdExtras.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            resultado.VotosExtras.Blancos = reader.GetInt32(0);
                            resultado.VotosExtras.Nulos = reader.GetInt32(1);
                            resultado.VotosExtras.Ausentes = reader.GetInt32(2);
                        }
                        resultado.VotosExtras.IdMesa = idMesa;
                    }
                }

                return resultado;
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Retorna mesas pertenecientes a una localidad específica
        public List<Mesa> ObtenerMesasPorLocalidad(int idLocalidad)
        {
            List<Mesa> mesas = new List<Mesa>();
            try
            {
                SqlConnection conn = conexion.AbrirConexion();
                comando.Connection = conn;
                comando.Parameters.Clear();

                string consulta = @"
                    SELECT m.IdMesa, m.FechaAsignada, m.NumeroMesa, m.Votantes, m.Cerrada,
                           l.IdLocalidad, l.Nombre
                    FROM Mesa m
                    INNER JOIN Localidad l ON m.IdLocalidad = l.IdLocalidad
                    WHERE l.IdLocalidad = @IdLocalidad";

                comando.CommandText = consulta;
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdLocalidad", idLocalidad);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Mesa mesa = new Mesa
                        {
                            IdMesa = reader.GetInt32(0),
                            FechaAsignada = reader.GetDateTime(1),
                            NumeroMesa = reader.GetInt32(2),
                            Votantes = reader.GetInt32(3),
                            Cerrada = reader.GetBoolean(4),
                            Localidad = new Localidad
                            {
                                IdLocalidad = reader.GetInt32(5),
                                Nombre = reader.GetString(6)
                            }
                        };
                        mesas.Add(mesa);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return mesas;
        }

        // Guarda o actualiza votos válidos y votos extras
        public void GuardarOActualizarVotos(int idMesa, int idCandidato, int votosValidos, int blancos, int nulos, int ausentes)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.Parameters.Clear();

                // Query para insertar o actualizar votos válidos
                string sqlMesaCandidato = @"
                    IF EXISTS (SELECT 1 FROM MesaCandidato WHERE IdMesa = @IdMesa AND IdCandidato = @IdCandidato)
                        UPDATE MesaCandidato SET Votos = @VotosValidos WHERE IdMesa = @IdMesa AND IdCandidato = @IdCandidato
                    ELSE
                        INSERT INTO MesaCandidato (IdMesa, IdCandidato, Votos) VALUES (@IdMesa, @IdCandidato, @VotosValidos)";

                comando.CommandText = sqlMesaCandidato;
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                comando.Parameters.AddWithValue("@IdCandidato", idCandidato);
                comando.Parameters.AddWithValue("@VotosValidos", votosValidos);
                comando.ExecuteNonQuery();

                // Votos extras
                comando.Parameters.Clear();
                string sqlVotosExtras = @"
                    IF EXISTS (SELECT 1 FROM VotosExtras WHERE IdMesa = @IdMesa)
                        UPDATE VotosExtras SET Blancos = @Blancos, Nulos = @Nulos, Ausentes = @Ausentes WHERE IdMesa = @IdMesa
                    ELSE
                        INSERT INTO VotosExtras (IdMesa, Blancos, Nulos, Ausentes) VALUES (@IdMesa, @Blancos, @Nulos, @Ausentes)";

                comando.CommandText = sqlVotosExtras;
                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                comando.Parameters.AddWithValue("@Blancos", blancos);
                comando.Parameters.AddWithValue("@Nulos", nulos);
                comando.Parameters.AddWithValue("@Ausentes", ausentes);
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar votos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        // Verifica si una mesa ya está cerrada
        public bool EstaMesaCerrada(int idMesa)
        {
            bool cerrada = false;

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.Parameters.Clear();

                string consulta = "SELECT Cerrada FROM Mesa WHERE IdMesa = @IdMesa";
                comando.CommandText = consulta;
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                object resultado = comando.ExecuteScalar();

                if (resultado != null && resultado != DBNull.Value)
                    cerrada = Convert.ToBoolean(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar si la mesa está cerrada: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return cerrada;
        }

        // Retorna los votos válidos registrados para un candidato en una mesa
        public int ObtenerVotosValidos(int idMesa, int idCandidato)
        {
            int votos = 0;

            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.Parameters.Clear();

                string consulta = "SELECT Votos FROM MesaCandidato WHERE IdMesa = @IdMesa AND IdCandidato = @IdCandidato";
                comando.CommandText = consulta;
                comando.CommandType = CommandType.Text;

                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                comando.Parameters.AddWithValue("@IdCandidato", idCandidato);

                object resultado = comando.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                    votos = Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los votos válidos: " + ex.Message);
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return votos;
        }

        // Obtiene el número total de votantes registrados para una mesa
        public int ObtenerVotantes(int idMesa)
        {
            int votantes = 0;

            try
            {
                string consulta = "SELECT Votantes FROM Mesa WHERE IdMesa = @IdMesa";
                comando.Connection = conexion.AbrirConexion();
                comando.Parameters.Clear();
                comando.CommandText = consulta;
                comando.Parameters.AddWithValue("@IdMesa", idMesa);

                object resultado = comando.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                    votantes = Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DAL] Error al obtener el número de votantes: {ex.Message}");
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return votantes;
        }
    }
}
