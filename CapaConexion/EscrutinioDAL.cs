using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Entidades;

namespace CapaConexion
{
    public class EscrutinioDAL
    {        
            private ConexionBD conexion = new ConexionBD();
            private SqlCommand comando;
            private SqlDataReader leer;

            public EscrutinioDAL()
            {
                comando = new SqlCommand();
               
            }

        public string AsignarMesa(DateTime fecha, string localidad, out int idMesa, out int numeroMesa, out int votantes)
        {
            idMesa = 0;
            numeroMesa = 0;
            votantes = 0;
            string resultado = "";

            try
            {
                // Asegurarse de que la conexión está abierta
                SqlConnection conn = conexion.AbrirConexion();
                comando.Connection = conn;  // Asignar la conexión al comando

                comando.CommandText = "AsignarMesa";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();

                comando.Parameters.AddWithValue("@Fecha", fecha);
                comando.Parameters.AddWithValue("@Localidad", localidad);

                var paramIdMesa = new SqlParameter("@IdMesa", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var paramNumeroMesa = new SqlParameter("@NumeroMesa", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var paramVotantes = new SqlParameter("@Votantes", SqlDbType.Int) { Direction = ParameterDirection.Output };

                comando.Parameters.Add(paramIdMesa);
                comando.Parameters.Add(paramNumeroMesa);
                comando.Parameters.Add(paramVotantes);

                comando.ExecuteNonQuery();

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
                conexion.CerrarConexion();  // Asegurarse de cerrar la conexión después de la operación
            }

            return resultado;
        }

        // Método para registrar datos - llama al procedimiento RegistrarDatos
        public string RegistrarDatos(int idMesa, string votosCsv, int blancos, int nulos)
        {
            string resultado = "";
            try
            {
                // Asegurarse de que la conexión esté abierta antes de ejecutar el comando
                SqlConnection conn = conexion.AbrirConexion();  // Asegúrate de abrir la conexión aquí
                comando.Connection = conn;  // Asignar la conexión al comando

                comando.CommandText = "RegistrarDatos";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();

                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                comando.Parameters.AddWithValue("@Votos", votosCsv);
                comando.Parameters.AddWithValue("@Blancos", blancos);
                comando.Parameters.AddWithValue("@Nulos", nulos);

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
                // Asegurarse de cerrar la conexión después de la ejecución
                conexion.CerrarConexion();  // Cerrar la conexión
            }
            return resultado;
        }

        // Método para cerrar mesa - llama al procedimiento CerrarMesa
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

                        var paramRespuesta = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 10)
                        {
                            Direction = ParameterDirection.Output
                        };
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

        // Obtener todos los candidatos
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

        // Clase auxiliar para contener datos completos de mesa
        public class DatosMesaCompleta
        {
            public Mesa Mesa { get; set; }
            public VotosExtras VotosExtras { get; set; }
        }

        // Obtener datos completos de una mesa
        public DatosMesaCompleta ObtenerDatosMesaCompleta(int idMesa)
        {
            DatosMesaCompleta resultado = new DatosMesaCompleta();
            resultado.Mesa = null;
            resultado.VotosExtras = new VotosExtras();

            SqlConnection conn = conexion.AbrirConexion();

            try
            {
                // Datos mesa + localidad
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
                        else
                        {
                            return null;
                        }
                    }
                }

                // Candidatos + votos
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

                // Votos extras
                string queryExtras = @"
                    SELECT Blancos, Nulos, Ausentes
                    FROM VotosExtras
                    WHERE IdMesa = @IdMesa";

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
                        else
                        {
                            resultado.VotosExtras.Blancos = 0;
                            resultado.VotosExtras.Nulos = 0;
                            resultado.VotosExtras.Ausentes = 0;
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

        public List<Mesa> ObtenerMesasPorLocalidad(int idLocalidad)
        {
            List<Mesa> mesas = new List<Mesa>();
            try
            {
                SqlConnection conn = conexion.AbrirConexion();
                comando.Connection = conn;
                comando.Parameters.Clear();

                string consulta = @"
                SELECT 
                m.IdMesa, m.FechaAsignada, m.NumeroMesa, m.Votantes, m.Cerrada,
                l.IdLocalidad, l.Nombre
                FROM Mesa m
                INNER JOIN Localidad l ON m.IdLocalidad = l.IdLocalidad
                WHERE l.IdLocalidad = @IdLocalidad";


                comando.CommandText = consulta;
                comando.CommandType = System.Data.CommandType.Text;
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

        public void GuardarOActualizarVotos(int idMesa, int idCandidato, int votosValidos, int blancos, int nulos, int ausentes)
        {
            try
            {
                comando.Connection = conexion.AbrirConexion();
                comando.Parameters.Clear();

                // 1. MesaCandidato
                string sqlMesaCandidato = @"
            IF EXISTS (SELECT 1 FROM MesaCandidato WHERE IdMesa = @IdMesa AND IdCandidato = @IdCandidato)
            BEGIN
                UPDATE MesaCandidato
                SET Votos = @VotosValidos
                WHERE IdMesa = @IdMesa AND IdCandidato = @IdCandidato
            END
            ELSE
            BEGIN
                INSERT INTO MesaCandidato (IdMesa, IdCandidato, Votos)
                VALUES (@IdMesa, @IdCandidato, @VotosValidos)
            END";

                comando.CommandText = sqlMesaCandidato;
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@IdMesa", idMesa);
                comando.Parameters.AddWithValue("@IdCandidato", idCandidato);
                comando.Parameters.AddWithValue("@VotosValidos", votosValidos);
                comando.ExecuteNonQuery();

                // 2. VotosExtras
                comando.Parameters.Clear(); // ← LIMPIA los parámetros antes de la segunda consulta

                string sqlVotosExtras = @"
            IF EXISTS (SELECT 1 FROM VotosExtras WHERE IdMesa = @IdMesa)
            BEGIN
                UPDATE VotosExtras
                SET Blancos = @Blancos, Nulos = @Nulos, Ausentes = @Ausentes
                WHERE IdMesa = @IdMesa
            END
            ELSE
            BEGIN
                INSERT INTO VotosExtras (IdMesa, Blancos, Nulos, Ausentes)
                VALUES (@IdMesa, @Blancos, @Nulos, @Ausentes)
            END";

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
                {
                    cerrada = Convert.ToBoolean(resultado);
                }
                    
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar si la mesa está cerrada: " + ex.Message);
                // Puedes lanzar la excepción si deseas manejarla arriba
                // throw;
            }
            finally 
            {
                conexion.CerrarConexion();
            }

            return cerrada;
        }

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
                {
                    votos = Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los votos válidos: " + ex.Message);
                // throw; // Descomenta si quieres propagar el error
            }
            finally
            {
                conexion.CerrarConexion();
            }

            return votos;
        }

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
                {
                    votantes = Convert.ToInt32(resultado);
                }
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

