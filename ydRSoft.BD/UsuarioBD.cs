using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class UsuarioBD
    {
        public static async Task<RpstaModel> Guardar(UsuarioModel objModel)
        {
            RpstaModel model = new RpstaModel();

            string query = "INSERT INTO usuario (nombres, dni, correo, clave, idsexo, idcargo, fechareg, estado, region) " +
                           "VALUES (@nombres, @dni, @correo, @clave, @idsexo, @idcargo, @fechareg, @estado, @region)";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombres", objModel.Nombres);
                        cmd.Parameters.AddWithValue("@dni", objModel.Dni);
                        cmd.Parameters.AddWithValue("@correo", objModel.Correo);
                        cmd.Parameters.AddWithValue("@clave", objModel.Clave);
                        cmd.Parameters.AddWithValue("@idsexo", objModel.IdSexo);
                        cmd.Parameters.AddWithValue("@idcargo", objModel.IdCargo);
                        cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@estado", 1);
                        cmd.Parameters.AddWithValue("@region", objModel.Region);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
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
            }
            return model;
        }

        public static async Task<UsuarioModel> GetUsuario(string Nombre, string Clave)
        {
            string query = @"SELECT
                            id, nombres, dni, correo, clave, idsexo, idcargo, estado, region
                            FROM usuario 
                            WHERE nombres = @nombres and clave = @clave;";

            UsuarioModel usuario = null;

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync();

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombres", Nombre);
                    cmd.Parameters.AddWithValue("@clave", Clave);

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync()) 
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new UsuarioModel
                            {
                                Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                Nombres = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                Dni = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                Correo = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                Clave = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                IdSexo = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                                Estado = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0,
                                Region = !reader.IsDBNull(8) ? reader.GetString(8) : ""
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                   await Util.LogError.SaveLog("Login Usuario | " + ex.Message);
                }
                finally
                {
                    await conexion.CloseAsync();
                }
            }
            return usuario;
        }

        public static async Task<UsuarioModel> GetUsuarioId(int id)
        {
            string query = @"SELECT
                            id, nombres, dni, correo, clave, idsexo, idcargo, estado, region
                            FROM usuario 
                            WHERE id = @id;";

            UsuarioModel usuario = null;

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id", id);

                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                usuario = new UsuarioModel
                                {
                                    Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                    Nombres = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                    Dni = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                    Correo = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                    Clave = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                    IdSexo = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                                    IdCargo = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0,
                                    Estado = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                                    Region = !reader.IsDBNull(8) ? reader.GetString(8) : ""
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Id Usuario | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

                
            return usuario;
        }

        public static async Task<UsuarioModel> GetUsuarioNom(string Nombre)
        {
            string query = @"SELECT
                            id, nombres, dni, correo, clave, idsexo, idcargo, estado, region
                            FROM usuario 
                            WHERE nombres = @nombres;";

            UsuarioModel usuario = null;

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombres", Nombre);

                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                usuario = new UsuarioModel
                                {
                                    Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                    Nombres = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                    Dni = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                    Correo = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                    Clave = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                    IdSexo = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                                    IdCargo = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0,
                                    Estado = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                                    Region = !reader.IsDBNull(8) ? reader.GetString(8) : ""
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Nombre Usuario | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return usuario;
        }

        public static async Task<RpstaModel> EditarUsuario(UsuarioModel objModel)
        {
            RpstaModel model = new RpstaModel();

            string query = "UPDATE usuario SET dni = @dni, correo = @correo, " +
                           "clave = @clave, idsexo = @idsexo, idcargo = @idcargo, estado = @estado, region= @region " +
                           "WHERE id = @id";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@dni", objModel.Dni);
                        cmd.Parameters.AddWithValue("@correo", objModel.Correo);
                        cmd.Parameters.AddWithValue("@clave", objModel.Clave);
                        cmd.Parameters.AddWithValue("@idsexo", objModel.IdSexo);
                        cmd.Parameters.AddWithValue("@idcargo", objModel.IdCargo);
                        cmd.Parameters.AddWithValue("@estado", objModel.Estado);
                        cmd.Parameters.AddWithValue("@id", objModel.Id);
                        cmd.Parameters.AddWithValue("@region", objModel.Region);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Usuario actualizado correctamente.";
                        }
                        else
                        {
                            model.Error = true;
                            model.Mensaje = "No se encontró el usuario.";
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Editar Usuario | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }
                

            return model;
        }

        public static async Task<List<UsuarioModel>> GetAll(int Estado)
        {
            List<UsuarioModel> mLista = new List<UsuarioModel>();

            string query = @"SELECT
                            id, nombres, dni, correo, clave, idsexo, idcargo, estado, region
                            FROM usuario 
                            WHERE estado = @estado;";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@estado", Estado);

                            using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var mUser = new UsuarioModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombres = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        Dni = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                        Correo = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                        Clave = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        IdSexo = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                                        IdCargo = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0,
                                        Estado = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0,
                                        Region = !reader.IsDBNull(8) ? reader.GetString(8) : ""
                                    };

                                    mLista.Add(mUser);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Usuario Getall | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
        }
    }
}
