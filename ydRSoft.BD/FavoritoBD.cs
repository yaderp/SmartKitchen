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
                             VALUES (@iduser, @idreceta, @fechareg, @estado);
                             SELECT LAST_INSERT_ID();";

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

                            var resultado = await command.ExecuteScalarAsync();


                            // Verificamos si el resultado es nulo antes de convertirlo a Int32
                            if (resultado != null && int.TryParse(resultado.ToString(), out int IdFav) && IdFav > 0)
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
                        await Util.LogError.SaveLog("Guardar Receta |" + ex.Message);
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

        public static async Task<List<FavoritoModel>> GetAll(int IdUser)
        {
            List<FavoritoModel> mLista = new List<FavoritoModel>();


            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        string query = @"
                                    SELECT r.id, r.iduser, r.idreceta
                                    FROM favoritos r
                                    WHERE r.iduser = @iduser";

                        using (MySqlCommand command = new MySqlCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@iduser", IdUser);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var fav = new FavoritoModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        IdUser = !reader.IsDBNull(1) ? reader.GetInt32(0) : 0,
                                        IdRe = !reader.IsDBNull(2) ? reader.GetInt32(0) : 0

                                    };

                                    mLista.Add(fav);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta Getcat | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }               
            }

            return mLista;
        }

        public static async Task<bool> GetDuplicado(int IdUser, int IdRe)
        {
            string query = "SELECT COUNT(*) FROM favoritos WHERE iduser = @iduser AND idreceta = @idreceta";
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
