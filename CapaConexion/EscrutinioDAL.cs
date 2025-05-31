using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

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
                comando.Connection = conexion.AbrirConexion();
            }

            // Método para asignar mesa - llama al procedimiento almacenado AsignarMesa
            public string AsignarMesa(DateTime fecha, string localidad, out int idMesa, out int numeroMesa, out int votantes)
            {
                idMesa = 0;
                numeroMesa = 0;
                votantes = 0;
                string resultado = "";

                try
                {
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
                    conexion.CerrarConexion();
                }

                return resultado;
            }

            // Método para registrar datos - llama al procedimiento RegistrarDatos
            public string RegistrarDatos(int idMesa, string votosCsv, int blancos, int nulos)
            {
                string resultado = "";
                try
                {
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
                    conexion.CerrarConexion();
                }
                return resultado;
            }

            // Método para cerrar mesa - llama al procedimiento CerrarMesa
            public string CerrarMesa(int idMesa)
            {
                string resultado = "";
                try
                {
                    comando.CommandText = "CerrarMesa";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Clear();

                    comando.Parameters.AddWithValue("@IdMesa", idMesa);

                    var paramRespuesta = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 10) { Direction = ParameterDirection.Output };
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

            // Puedes agregar más métodos para consultas o reportes según necesites
        }
    }
