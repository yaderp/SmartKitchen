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
        public static async Task<RpstaModel> Guardar(RecetaModel objModel)
        {
            RpstaModel rpstaModel = new RpstaModel();

            if (objModel == null || objModel.Ingredientes == null || objModel.PasosPreparacion == null)
            {
                rpstaModel.Error = true;
                rpstaModel.Mensaje = "El modelo de receta o las listas de ingredientes/pasos no pueden ser nulos.";
                return rpstaModel;
            }

            string query = @"INSERT INTO receta (nombre, ndif, tiempo,categoria,ingredientes,preparacion, fechareg, estado) 
                                        VALUES (@nombre, @ndif, @tiempo,@categoria,@ingredientes,@preparacion, @fechareg, @estado);
                                        SELECT LAST_INSERT_ID();";

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
                            command.Parameters.AddWithValue("@ndif", objModel.NivelDificultad);
                            command.Parameters.AddWithValue("@tiempo", objModel.Tiempo);
                            command.Parameters.AddWithValue("@categoria", objModel.Categoria);
                            command.Parameters.AddWithValue("@ingredientes", objModel.StrIngre);
                            command.Parameters.AddWithValue("@preparacion", objModel.StrPas);
                            command.Parameters.AddWithValue("@fechareg", DateTime.Now);
                            command.Parameters.AddWithValue("@estado", 1);

                            var resultado = await command.ExecuteScalarAsync();

                            if (resultado != null && int.TryParse(resultado.ToString(), out int recetaId) && recetaId > 0)
                            {
                                rpstaModel.Id = recetaId;
                                rpstaModel.Error = false;
                                rpstaModel.Mensaje = "Receta guardada con éxito.";
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


        public static async Task<List<int>> GetRecetaCat(string Categoria)
        {
            List<int> mLista = new List<int>();

            if (string.IsNullOrEmpty(Categoria)) {
                return mLista;
            }
            
            string query = @"SELECT
                            id
                            FROM receta
                            WHERE categoria = @categoria
                            LIMIT 5;";


            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@categoria", Categoria);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    int Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
                                    mLista.Add(Id);
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
            }
            return mLista;
        }

        public static async Task<RecetaModel> GetRecetaId(int recetaId)
        {
            RecetaModel objModel = null;

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, ingredientes, preparacion, fechareg, estado
                            FROM receta
                            WHERE id = @recetaId;";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@recetaId", recetaId);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        StrIngre = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        StrPas = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                                        FechaRegistro = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now,
                                        Estado = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0
                                    };

                                    objModel = receta;
                                }
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        objModel = null;
                        await Util.LogError.SaveLog("Receta GetId | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return objModel;
        }

        public static async Task<RecetaModel> GetRecetaNom(string Nombre)
        {
            RecetaModel objModel = null;

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, ingredientes, preparacion, fechareg, estado
                            FROM receta
                            WHERE nombre = @recetaNom;";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@recetaNom", Nombre);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        StrIngre = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        StrPas = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                                        FechaRegistro = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now,
                                        Estado = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0
                                    };

                                    objModel = receta;
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        objModel = null;
                        await Util.LogError.SaveLog("Receta GetNombre | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return objModel;
        }

        public static async Task<bool> IsDuplicado(string Nombre)
        {
            string query = "SELECT COUNT(*) FROM receta WHERE nombre = @nombre;";
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
                            cmd.Parameters.AddWithValue("@nombre", Nombre);                            

                            var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                            if (count > 0)
                            {
                                encontrado = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        encontrado=false;
                        await Util.LogError.SaveLog("Duplicado Receta " + ex.Message);
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
