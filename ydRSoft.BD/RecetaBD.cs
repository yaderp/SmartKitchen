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
        public static async Task<RecetaModel> Get()
        {
            string query = "SELECT * FROM receta";
            RecetaModel objModel = new RecetaModel();

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Ejecutar la lectura de forma asíncrona
                    {
                        await reader.ReadAsync();
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
            return objModel;
        }
        
        public static async Task<int> Set(RecetaModel objModel) {
            int insertedId = 0;
            string query = @"INSERT INTO receta (nombre, ndif, tiempo, fechareg, estado) 
                                 VALUES (@nombre, @ndif, @tiempo, @fechareg, @estado);
                                 SELECT LAST_INSERT_ID();";

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);

                    cmd.Parameters.AddWithValue("@nombre", objModel.Nombre);
                    cmd.Parameters.AddWithValue("@ndif", objModel.NivelDificultad);
                    cmd.Parameters.AddWithValue("@tiempo", objModel.Tiempo);
                    cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
                    cmd.Parameters.AddWithValue("@estado", 1);

                    insertedId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    
                }
                catch (Exception ex)
                {
                    insertedId = -1;
                    await Util.LogError.SaveLog("Guardar Receta |" +ex.Message);
                }
                finally
                {
                    
                    await conexion.CloseAsync();
                }
            }

            return insertedId;

        }


        public static async Task<string> SetAll(RecetaModel objModel)
        {
            int recetaId=0;
            List<string> ingredientes = objModel.Ingredientes;
            List<string> pasosPreparacion = objModel.PasosPreparacion;

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                MySqlTransaction transaction = null;

                try {
                    await connection.OpenAsync();
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

                } catch (Exception ex){
                    if (transaction != null)
                    {
                        await transaction.RollbackAsync();
                    }
                    recetaId = -1;
                    await Util.LogError.SaveLog("Guardar Receta |" + ex.Message);
                } finally {
                    await connection.CloseAsync();
                }
            }

            return "Receta guardada " + recetaId;
        }
    
        public static async Task<RecetaModel> GetId(int Id)
        {
            RecetaModel objModel = new RecetaModel();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {

                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT r.id, r.nombre, r.ndif, r.tiempo, r.fechareg, r.estado,
                           i.detalle AS ingrediente, p.detalle AS paso
                    FROM receta r
                    LEFT JOIN ingredientes i ON r.id = i.idreceta
                    LEFT JOIN preparacion p ON r.id = p.idreceta
                    WHERE r.id = @recetaId
                    ORDER BY r.id;";

                    RecetaModel receta = null;

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Parámetro para la receta con Id=1
                        command.Parameters.AddWithValue("@recetaId", 1);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            while (await reader.ReadAsync())
                            {
                                if (receta == null)
                                {
                                    // Inicializar la receta cuando se encuentre el primer registro
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

                            // Agregar ingrediente si existe
                            if (!reader.IsDBNull(reader.GetOrdinal("ingrediente")))
                            {
                                receta.Ingredientes.Add(reader.GetString("ingrediente"));
                            }

                            // Agregar paso de preparación si existe
                            if (!reader.IsDBNull(reader.GetOrdinal("paso")))
                            {
                                receta.PasosPreparacion.Add(reader.GetString("paso"));
                            }

                        }
                    }

                    if (receta != null) {
                        objModel = receta;
                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("Receta GetId | " + ex.Message);

                }
            }


            return objModel;
        }
    
    }
}
