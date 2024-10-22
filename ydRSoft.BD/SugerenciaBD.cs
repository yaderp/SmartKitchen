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

            string query = @"INSERT INTO sugerencias (nombre,idreceta, iduser, fechareg) 
                                        VALUES (@nombre,@idreceta,@iduser, @fechareg);";

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
    
    }
}
