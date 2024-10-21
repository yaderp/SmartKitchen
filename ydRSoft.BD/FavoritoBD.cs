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
                                    WHERE iduser = @iduser";

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

        public static async Task<bool> IsDuplicado(int IdUser, int IdRe)
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
