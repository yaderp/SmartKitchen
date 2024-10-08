using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class UsuarioBD
    {


        public static async Task<string> GuardarUsuario(UsuarioModel objModel)
        {
            string resultado = "Error de Conexion- verifique log";

            string query = "INSERT INTO usuario (nombres, dni, correo, clave, idsexo, fechareg, estado) " +
                           "VALUES (@nombres, @dni, @correo, @clave, @idsexo, @fechareg, @estado)";

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombres", objModel.Nombres);
                    cmd.Parameters.AddWithValue("@dni", objModel.Dni);
                    cmd.Parameters.AddWithValue("@correo", objModel.Correo);
                    cmd.Parameters.AddWithValue("@clave", objModel.Clave);
                    cmd.Parameters.AddWithValue("@idsexo", objModel.IdSexo);
                    cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
                    cmd.Parameters.AddWithValue("@estado", 1);

                    var respuesta = await cmd.ExecuteNonQueryAsync();
                    if (respuesta > 0)
                    {
                        resultado = "Correcto";
                    }
                }
                catch (Exception ex)
                {
                    resultado = ex.Message;
                    await Util.LogError.SaveLog("Guardar USuario |");
                }
                finally
                {
                    resultado = resultado + " Finalizado";
                    await conexion.CloseAsync();
                }                
            }

            return resultado;
        }

        public static async Task<List<UsuarioModel>> LeerUsuariosAsync()
        {
            string query = "SELECT * FROM usuario";
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

            MySqlConnection conexion = MySqlConexion.MyConexion();

            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    System.Data.Common.DbDataReader dbDataReader = await cmd.ExecuteReaderAsync();

                    using (MySqlDataReader reader = (MySqlDataReader)dbDataReader) // Ejecutar la lectura de forma asíncrona
                    {
                        while (await reader.ReadAsync()) // Leer los datos de forma asíncrona
                        {
                            UsuarioModel usuario = new UsuarioModel
                            {
                                Id = reader.GetInt32("id"),
                                Nombres = reader.GetString("nombres"),
                                Dni = reader.GetString("dni"),
                                Correo = reader.GetString("correo"),
                                Clave = reader.GetString("clave"),
                                IdSexo = reader.GetInt32("idsexo"),
                                FechaReg = reader.GetDateTime("fechareg"),
                                Estado = reader.GetInt32("estado")
                            };

                            listaUsuarios.Add(usuario); // Añadir usuario a la lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await conexion.CloseAsync(); // Cerrar la conexión de forma asíncrona
                }
            }
            return listaUsuarios;
        }

        public static async Task<List<UsuarioModel>> LeerUsuarios()
        {
            string query = "SELECT * FROM usuario";
            List<UsuarioModel> listaUsuarios = new List<UsuarioModel>();

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Ejecutar la lectura de forma asíncrona
                    {
                        while (await reader.ReadAsync()) // Leer los datos de forma asíncrona
                        {
                            UsuarioModel usuario = new UsuarioModel
                            {
                                Id = reader.GetInt32("id"),
                                Nombres = reader.GetString("nombres"),
                                Dni = reader.GetString("dni"),
                                Correo = reader.GetString("correo"),
                                Clave = reader.GetString("clave"),
                                IdSexo = reader.GetInt32("idsexo"),
                                FechaReg = reader.GetDateTime("fechareg"),
                                Estado = reader.GetInt32("estado")
                            };
                            listaUsuarios.Add(usuario); // Añadir usuario a la lista
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await conexion.CloseAsync(); // Cerrar la conexión de forma asíncrona
                }
            }
            return listaUsuarios;
        }



        public static UsuarioModel GetUsuario()
        {
            UsuarioModel objModel = new UsuarioModel();
            MySqlConnection conexion = MySqlConexion.MyConexion();
            conexion.Open();
            try
            {


                MySqlDataReader leer = null;
                string query = "select *from usuario limit 1";

                MySqlCommand cmdsql = new MySqlCommand(query, conexion);

                leer = cmdsql.ExecuteReader();
                if (leer.HasRows)
                {
                    leer.Read();
                    objModel.Id = leer.GetInt32(0);
                    objModel.Nombres = leer.GetString(1);
                    objModel.Correo = leer.GetString(2);
                    objModel.Clave = leer.GetString(3);
                    objModel.Correo = leer.GetString(4);
                    objModel.IdSexo = leer.GetInt32(5);
                }
            }
            catch
            {

            }
            conexion.Close();
            return objModel;
        }
    }
}
