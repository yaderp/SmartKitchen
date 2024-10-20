using MySql.Data.MySqlClient;
using Mysqlx.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;
using System.Data.Common;

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

                    string insertRecetaQuery = @"INSERT INTO receta (nombre, ndif, tiempo,categoria, fechareg, estado) 
                                             VALUES (@nombre, @ndif, @tiempo,@categoria, @fechareg, @estado);
                                             SELECT LAST_INSERT_ID();";


                    using (MySqlCommand command = new MySqlCommand(insertRecetaQuery, connection, transaction))
                    {                       
                        command.Parameters.AddWithValue("@nombre", objModel.Nombre);
                        command.Parameters.AddWithValue("@ndif", objModel.NivelDificultad);
                        command.Parameters.AddWithValue("@tiempo", objModel.Tiempo);
                        command.Parameters.AddWithValue("@categoria", objModel.Categoria);
                        command.Parameters.AddWithValue("@fechareg", DateTime.Now);
                        command.Parameters.AddWithValue("@estado", 1);
                        
                        recetaId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    }

                    string insertIngredienteQuery = @"INSERT INTO ingredientes (idreceta, detalle) VALUES (@idreceta, @detalle);";

                    using (MySqlCommand command = new MySqlCommand(insertIngredienteQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idreceta", recetaId);
                        foreach (string ingrediente in ingredientes)
                        {
                            command.Parameters.AddWithValue("@detalle", ingrediente);
                            await command.ExecuteNonQueryAsync();
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@idreceta", recetaId);
                        }
                    }

                    
                    string insertPreparacionQuery = @"INSERT INTO preparacion (idreceta, detalle) VALUES (@idreceta, @detalle);";

                    using (MySqlCommand command = new MySqlCommand(insertPreparacionQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idreceta", recetaId);
                        foreach (string paso in pasosPreparacion)
                        {
                            command.Parameters.AddWithValue("@detalle", paso);
                            await command.ExecuteNonQueryAsync();
                            command.Parameters.Clear(); 
                            command.Parameters.AddWithValue("@idreceta", recetaId);
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

        public static async Task<List<RecetaModel>> GetRecetaCat(string Categoria)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.fechareg, r.estado
                    FROM receta r
                    WHERE r.categoria = @categoria
                    LIMIT 5;";

                    RecetaModel receta = null;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@categoria", Categoria);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                receta = new RecetaModel
                                {
                                    Id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("nombre"),
                                    NivelDificultad = reader.GetInt32("ndif"),
                                    Tiempo = reader.GetInt32("tiempo"),
                                    Categoria = reader.GetString("categoria"),
                                    FechaRegistro = reader.GetDateTime("fechareg"),
                                    Estado = reader.GetInt32("estado")
                                };

                                mLista.Add(receta);
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
                    await connection.CloseAsync();
                }
            }

            return mLista;
        }

        public static async Task<RecetaModel> GetRecetaId(int recetaId)
        {
            RecetaModel objModel = new RecetaModel();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    // Consulta principal de receta
                    string query = @"
            SELECT r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.fechareg, r.estado
            FROM receta r
            WHERE r.id = @recetaId;";

                    RecetaModel receta = null;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recetaId", recetaId);

                        using (DbDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                receta = new RecetaModel
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    NivelDificultad = reader.GetInt32(2),
                                    Tiempo = reader.GetInt32(3),
                                    Categoria = reader.GetString(4),
                                    FechaRegistro = reader.GetDateTime(5),
                                    Estado = reader.GetInt32(6)
                                };
                            }
                        }
                    }

                    if (receta != null)
                    {
                        objModel = receta;

                        // Consulta de ingredientes
                        string queryIngredientes = @"
                SELECT detalle AS ingrediente
                FROM ingredientes
                WHERE idreceta = @recetaId;";

                        using (MySqlCommand command = new MySqlCommand(queryIngredientes, connection))
                        {
                            command.Parameters.AddWithValue("@recetaId", recetaId);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    receta.Ingredientes.Add(reader.GetString(0));
                                }
                            }
                        }

                        // Consulta de pasos
                        string queryPasos = @"
                SELECT detalle AS paso
                FROM preparacion
                WHERE idreceta = @recetaId;";

                        using (MySqlCommand command = new MySqlCommand(queryPasos, connection))
                        {
                            command.Parameters.AddWithValue("@recetaId", recetaId);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    receta.PasosPreparacion.Add(reader.GetString(0));
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


        public static async Task<RecetaModel> GetRecetaId_otro(int recetaId)
        {
            RecetaModel objModel = new RecetaModel();
            string connectionString = "Server=localhost;Database=dbydrconta;User Id=ydrsoft;Password=Palomita16;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = @"
            SELECT r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.fechareg, r.estado
            FROM receta r
            WHERE r.id = @recetaId;";

                RecetaModel receta = null;

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@recetaId", recetaId);

                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            receta = new RecetaModel
                            {
                                Id = reader.GetInt32(1),
                                Nombre = reader.GetString(2),
                                NivelDificultad = reader.GetInt32(3),
                                Tiempo = reader.GetInt32(4),
                                Categoria = reader.GetString(5),
                                FechaRegistro = reader.GetDateTime(6),
                                Estado = reader.GetInt32(7)
                            };
                        }
                    }
                }

                if (receta != null)
                {
                    objModel = receta;
                    // Aquí puedes agregar la lógica para obtener ingredientes y pasos si es necesario
                }
            }

            return objModel;
        }

        public static async Task<RecetaModel> GetRecetaId_test(int recetaId)
        {
            RecetaModel objModel = new RecetaModel();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.fechareg, r.estado
                    FROM receta r
                    WHERE r.id = @recetaId;";

                    RecetaModel receta = null;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {                        
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
                                    Categoria = reader.GetString("categoria"),
                                    FechaRegistro = reader.GetDateTime("fechareg"),
                                    Estado = reader.GetInt32("estado")
                                };
                            }
                        }
                    }

                    if (receta != null) {
                        objModel = receta;
                        
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


        public static async Task<RecetaModel> GetRecetaNom(string Nombre)
        {
            RecetaModel objModel = null;

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT r.id, r.nombre, r.ndif, r.tiempo, r.categoria, r.fechareg, r.estado
                    FROM receta r
                    WHERE r.nombre = @nombre;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", Nombre);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())
                            {
                                objModel = new RecetaModel
                                {
                                    Id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("nombre"),
                                    NivelDificultad = reader.GetInt32("ndif"),
                                    Tiempo = reader.GetInt32("tiempo"),
                                    Categoria = reader.GetString("categoria"),
                                    FechaRegistro = reader.GetDateTime("fechareg"),
                                    Estado = reader.GetInt32("estado")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("Receta GetNOmbre | " + ex.Message);
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
