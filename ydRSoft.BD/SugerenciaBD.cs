using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class SugerenciaBD
    {
        public static async Task<RpstaModel> Guardar(SugerModel objModel)
        {
            RpstaModel rpstaModel = new RpstaModel();

            if (objModel == null)
            {
                rpstaModel.Error = true;
                rpstaModel.Mensaje = "El modelo no pueden ser nulos.";
                return rpstaModel;
            }

            string query = @"INSERT INTO sugerencias (nombre,idreceta, iduser, fechareg,estado) 
                                        VALUES (@nombre,@idreceta,@iduser, @fechareg,@estado);";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    await connection.OpenAsync();

                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", objModel.Nombre);
                            command.Parameters.AddWithValue("@idreceta", objModel.IdReceta);
                            command.Parameters.AddWithValue("@iduser", objModel.IdUser);
                            command.Parameters.AddWithValue("@fechareg", DateTime.Now);
                            command.Parameters.AddWithValue("@estado", 0);

                            var resultado = await command.ExecuteNonQueryAsync();

                            if (resultado > 0)
                            {
                                rpstaModel.Error = false;
                                rpstaModel.Mensaje = "Guardado con éxito.";
                            }
                            else
                            {
                                rpstaModel.Mensaje = "Error al guardar";
                                return rpstaModel;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Guardar Receta |" + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return rpstaModel;
        }
        
        public static async Task<int> GetRecetaId()
        {
            int recetaId = 0;
            string query = @"SELECT idreceta 
                            FROM sugerencias
                            WHERE DATE(fechareg) = CURDATE();";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    await connection.OpenAsync();

                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    recetaId = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Guardar Receta |" + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return recetaId;
        }

        public static async Task<List<RecetaModel>> ListaSug(string Nombre)
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
                                        f.id, f.idreceta, r.nombre, r.ndif, r.tiempo, r.categoria, r.calificacion 
                                        FROM sugerencias f 
                                        inner join receta r on r.id = f.idreceta 
                                        WHERE r.nombre LIKE @nombre and f.estado > 0;";

                        using (MySqlCommand command = new MySqlCommand(query, conexion))
                        {
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

        public static async Task<List<RecetaModel>> ListaSug()
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
                                        f.id, f.idreceta, r.nombre, r.ndif, r.tiempo, r.categoria, r.calificacion 
                                        FROM sugerencias f 
                                        inner join receta r on r.id = f.idreceta 
                                        WHERE f.estado > 0;";

                        using (MySqlCommand command = new MySqlCommand(query, conexion))
                        {

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

        public static async Task<RpstaModel> ActEstado(int IdSug, int Estado)
        {
            RpstaModel model = new RpstaModel();

            string query = @"UPDATE sugerencias 
                            SET estado = @estado 
                            WHERE id = @idsug;";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@estado", Estado);
                        cmd.Parameters.AddWithValue("@idsug", IdSug);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Sugerencia actualizada correctamente.";
                        }
                        else
                        {
                            model.Error = true;
                            model.Mensaje = "No se encontró la Sugerencia.";
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

    }
}
