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
        public static async Task<RpstaModel> GuardarUsuario(UsuarioModel objModel)
        {
            RpstaModel model = new RpstaModel();

            string query = "INSERT INTO usuario (nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado) " +
                           "VALUES (@nombres, @dni, @correo, @clave, @idsexo, @idcargo, @fechareg, @estado)";

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
                    cmd.Parameters.AddWithValue("@idcargo", objModel.IdCargo);
                    cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
                    cmd.Parameters.AddWithValue("@estado", 1);

                    var respuesta = await cmd.ExecuteNonQueryAsync();
                    if (respuesta > 0)
                    {
                        model.Error=false;
                        model.Mensaje = "Correcto";
                    }
                }
                catch (Exception ex)
                {
                    model.Mensaje = ex.Message;
                    await Util.LogError.SaveLog("Guardar USuario |");
                }
                finally
                {
                    await conexion.CloseAsync();
                }                
            }

            return model;
        }

        public static async Task<UsuarioModel> GetUsuario(string Nombre, string Clave)
        {
            string query = "SELECT * FROM usuario WHERE nombres = @nombres and clave = @clave";
            UsuarioModel usuario = null;

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombres", Nombre);
                    cmd.Parameters.AddWithValue("@clave", Clave);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Ejecutar la lectura de forma asíncrona
                    {
                        if (await reader.ReadAsync()) // Leer los datos de forma asíncrona
                        {
                            usuario = new UsuarioModel
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
            return usuario;
        }

        public static async Task<UsuarioModel> GetUsuarioId(int id)
        {
            string query = "SELECT * FROM usuario WHERE id = @id";
            UsuarioModel usuario = null;

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@id", id); // Asignar el valor de id al parámetro

                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Ejecutar la lectura de forma asíncrona
                    {
                        if (await reader.ReadAsync()) // Leer los datos de forma asíncrona
                        {
                            usuario = new UsuarioModel
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
            return usuario;
        }

        public static async Task<UsuarioModel> GetUsuarioNom(string Nombre)
        {
            string query = "SELECT * FROM usuario WHERE nombres = @nombres";
            UsuarioModel usuario = null;

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombres", Nombre);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Ejecutar la lectura de forma asíncrona
                    {
                        if (await reader.ReadAsync()) // Leer los datos de forma asíncrona
                        {
                            usuario = new UsuarioModel
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
            return usuario;
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

    }
}
