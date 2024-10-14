using MySql.Data.MySqlClient;
using Mysqlx.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class RecetaBD
    {
        public static async Task<int> Guardar(RecetaModel objModel)
        {
            int recetaId = 0;
            List<string> ingredientes = objModel.Ingredientes;
            List<string> pasosPreparacion = objModel.PasosPreparacion;

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                MySqlTransaction transaction = null;
                await connection.OpenAsync();               
                try
                {
                    transaction = await connection.BeginTransactionAsync();

                    string insertRecetaQuery = @"INSERT INTO receta (nombre, ndif, tiempo, fechareg, estado) 
                                             VALUES (@nombre, @ndif, @tiempo, @fechareg, @estado);
                                             SELECT LAST_INSERT_ID();";


                    using (MySqlCommand command = new MySqlCommand(insertRecetaQuery, connection, transaction))
                    {
                        // Parámetros para la receta
                        command.Parameters.AddWithValue("@nombre", objModel.Nombre);
                        command.Parameters.AddWithValue("@ndif", objModel.NivelDificultad);
                        command.Parameters.AddWithValue("@tiempo", objModel.Tiempo);
                        command.Parameters.AddWithValue("@fechareg", DateTime.Now);
                        command.Parameters.AddWithValue("@estado", 1);

                        // Ejecutar el comando y obtener el ID de la receta insertada
                        recetaId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    }

                    string insertIngredienteQuery = @"INSERT INTO ingredientes (idreceta, detalle) VALUES (@idreceta, @detalle);";

                    using (MySqlCommand command = new MySqlCommand(insertIngredienteQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idreceta", recetaId);
                        foreach (string ingrediente in ingredientes)
                        {
                            command.Parameters.AddWithValue("@detalle", ingrediente);
                            await command.ExecuteNonQueryAsync(); // Inserta cada ingrediente
                            command.Parameters.Clear(); // Limpiar parámetros para la siguiente iteración
                            command.Parameters.AddWithValue("@idreceta", recetaId); // Volver a agregar el idreceta
                        }
                    }

                    // 3. Insertar los pasos de preparación asociados
                    string insertPreparacionQuery = @"INSERT INTO preparacion (idreceta, detalle) VALUES (@idreceta, @detalle);";

                    using (MySqlCommand command = new MySqlCommand(insertPreparacionQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idreceta", recetaId);
                        foreach (string paso in pasosPreparacion)
                        {
                            command.Parameters.AddWithValue("@detalle", paso);
                            await command.ExecuteNonQueryAsync(); // Inserta cada paso
                            command.Parameters.Clear(); // Limpiar parámetros para la siguiente iteración
                            command.Parameters.AddWithValue("@idreceta", recetaId); // Volver a agregar el idreceta
                        }
                    }

                    await transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        await transaction.RollbackAsync();
                    }
                    recetaId = -1;
                    await Util.LogError.SaveLog("Guardar Receta |" + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return recetaId;
        }
                
        public static async Task<RecetaModel> GetReceta(int recetaId)
        {
            RecetaModel objModel = new RecetaModel();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    // Consulta para obtener solo la receta
                    string query = @"
                    SELECT r.id, r.nombre, r.ndif, r.tiempo, r.fechareg, r.estado
                    FROM receta r
                    WHERE r.id = @recetaId;";

                    RecetaModel receta = null;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Parámetro para la receta con Id=1
                        command.Parameters.AddWithValue("@recetaId", recetaId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                receta = new RecetaModel
                                {
                                    Id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("nombre"),
                                    NivelDificultad = reader.GetInt32("ndif"),
                                    Tiempo = reader.GetInt32("tiempo"),
                                    FechaRegistro = reader.GetDateTime("fechareg"),
                                    Estado = reader.GetInt32("estado")
                                };
                            }
                        }
                    }

                    if (receta != null) {
                        objModel = receta;

                        // 2. Consulta para obtener los ingredientes de la receta
                        string queryIngredientes = @"
                        SELECT detalle AS ingrediente
                        FROM ingredientes
                        WHERE idreceta = @recetaId;";

                        using (MySqlCommand command = new MySqlCommand(queryIngredientes, connection))
                        {
                            command.Parameters.AddWithValue("@recetaId", recetaId);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (await reader.ReadAsync())
                                {
                                    receta.Ingredientes.Add(reader.GetString("ingrediente"));
                                }
                            }
                        }


                        // 3. Consulta para obtener los pasos de preparación de la receta
                        string queryPasos = @"
                        SELECT detalle AS paso
                        FROM preparacion
                        WHERE idreceta = @recetaId;";

                        using (MySqlCommand command = new MySqlCommand(queryPasos, connection))
                        {
                            command.Parameters.AddWithValue("@recetaId", recetaId);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (await reader.ReadAsync())
                                {
                                    receta.PasosPreparacion.Add(reader.GetString("paso"));
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("Receta GetId | " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return objModel;
        }
    }
}
