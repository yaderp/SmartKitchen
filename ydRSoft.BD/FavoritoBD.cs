using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class FavoritoBD
    {
        public static async Task<RpstaModel> Guardar(int IdUser, int IdRe)
        {
            RpstaModel rpstaModel = new RpstaModel();

            string Query = @"INSERT INTO favoritos (iduser, idreceta, fechareg, estado) 
                             VALUES (@iduser, @idreceta, @fechareg, @estado)";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(Query, conexion))
                        {
                            command.Parameters.AddWithValue("@iduser", IdUser);
                            command.Parameters.AddWithValue("@idreceta", IdRe);
                            command.Parameters.AddWithValue("@fechareg", DateTime.Now);
                            command.Parameters.AddWithValue("@estado", 1);

                            var filasAfectadas = await command.ExecuteNonQueryAsync();

                            if (filasAfectadas > 0)
                            {
                                rpstaModel.Error = false;
                                rpstaModel.Mensaje = "Agregado a favoritos";
                            }
                            else
                            {     
                                rpstaModel.Mensaje = "Error al guardar";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Guardar Favorito |" + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
                else
                {
                    rpstaModel.Mensaje = "No se pudo establecer la conexión";
                }
            }

            return rpstaModel;
        }

        public static async Task<List<int>> GetAll(int IdUser)
        {
            List<int> mLista = new List<int>();


            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        string query = @"
                                    SELECT idreceta
                                    FROM favoritos
                                    WHERE iduser = @iduser and estado > 0;";

                        using (MySqlCommand command = new MySqlCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@iduser", IdUser);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    int IdRe = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                                    mLista.Add(IdRe);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("fav getall | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }               
            }

            return mLista;
        }

        public static async Task<List<RecetaModel>> ListaFavoritos(int IdUser, string Nombre)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();
            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        string query = @"SELECT 
                                        f.id, r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.calificacion 
                                        FROM favoritos f 
                                        inner join receta r on r.id = f.idreceta 
                                        WHERE r.nombre LIKE @nombre and f.iduser = @iduser and f.estado > 0;";

                        using (MySqlCommand command = new MySqlCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@iduser", IdUser);
                            command.Parameters.AddWithValue("@nombre", "%" + Nombre + "%");

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel
                                    {
                                        IdFav = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Id = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                                        Nombre = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                        NivelDificultad = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Tiempo = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                                        Categoria = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        Calificacion = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("lista fav | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<List<RecetaModel>> ListaFavoritos(int IdUser)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        string query = @"SELECT 
                                        f.id, r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.calificacion 
                                        FROM favoritos f 
                                        inner join receta r on r.id = f.idreceta 
                                        WHERE f.iduser = @iduser and f.estado > 0;";

                        using (MySqlCommand command = new MySqlCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@iduser", IdUser);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel
                                    {
                                        IdFav = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Id = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                                        Nombre = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                        NivelDificultad = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Tiempo = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0,
                                        Categoria = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        Calificacion = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0
                                    };
                                   
                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("lista fav | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<RpstaModel> ActEstado(int IdUser, int recetaId, int Estado)
        {
            RpstaModel model = new RpstaModel();

            string query = "UPDATE favoritos SET estado = @estado " +
                           "WHERE idreceta = @idreceta and iduser = @iduser;";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@estado", Estado);
                        cmd.Parameters.AddWithValue("@iduser", IdUser);
                        cmd.Parameters.AddWithValue("@idreceta", recetaId);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Favorito actualizada correctamente.";
                        }
                        else
                        {
                            model.Error = true;
                            model.Mensaje = "No se encontró la Favorito.";
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Favorito Edit | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }


            return model;
        }

        public static async Task<RpstaModel> ActEstado(int IdFav, int Estado)
        {
            RpstaModel model = new RpstaModel();

            string query = "UPDATE favoritos SET estado = @estado " +
                           "WHERE id = @idfav;";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@estado", Estado);
                        cmd.Parameters.AddWithValue("@idfav", IdFav);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Favorito actualizada correctamente.";
                        }
                        else
                        {
                            model.Error = true;
                            model.Mensaje = "No se encontró la Favorito.";
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Favorito Edit 2 | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }


            return model;
        }

        public static async Task<bool> IsDuplicado(int IdUser, int IdRe)
        {
            string query = "SELECT COUNT(*) FROM favoritos WHERE iduser = @iduser AND idreceta = @idreceta AND estado >0;";
            bool encontrado = false;

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@iduser", IdUser);
                            cmd.Parameters.AddWithValue("@idreceta", IdRe);

                            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                            if (count > 0)
                            {
                                encontrado = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Duplicado favorito "+ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }
            
            return encontrado;
        }
    }
}
