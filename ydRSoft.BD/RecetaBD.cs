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

            string query = @"INSERT INTO receta (nombre, ndif, tiempo,categoria,ingredientes,preparacion, fechareg, estado,calificacion, iduser) 
                                        VALUES (@nombre, @ndif, @tiempo,@categoria,@ingredientes,@preparacion, @fechareg, @estado,@calificacion, @iduser);
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
                            command.Parameters.AddWithValue("@estado", objModel.Estado);
                            command.Parameters.AddWithValue("@calificacion", 0);
                            command.Parameters.AddWithValue("@iduser", objModel.IdUser);
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
                            WHERE categoria = @categoria and estado > 1
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
                            id, nombre, ndif, tiempo, categoria, ingredientes, preparacion, fechareg, estado, calificacion, iduser
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
                                        Estado = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
                                        Calificacion = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0,
                                        IdUser = !reader.IsDBNull(10) ? reader.GetInt32(10) : 1
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
                            id, nombre, ndif, tiempo, categoria, ingredientes, preparacion, fechareg, estado, calificacion, iduser
                            FROM receta
                            WHERE nombre = @recetaNom";

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
                                        Estado = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0,
                                        Calificacion = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0,
                                        IdUser = !reader.IsDBNull(10) ? reader.GetInt32(10) : 1
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


        public static async Task<List<RecetaModel>> ListaFiltros(string query)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel()
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Calificacion = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Dif | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<List<RecetaModel>> ListaDificultad(int Dificultad)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            if (Dificultad <= 0)
            {
                return mLista;
            }

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, calificacion
                            FROM receta
                            WHERE ndif = @ndif and estado > 0;";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ndif", Dificultad);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel()
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Calificacion = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0,
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Dif | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<List<RecetaModel>> ListaCategoria(string Categoria)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            if (string.IsNullOrEmpty(Categoria))
            {
                return mLista;
            }

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, calificacion
                            FROM receta
                            WHERE categoria = @categoria and estado > 0;";

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
                                    var receta = new RecetaModel()
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Calificacion = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Dif | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<List<RecetaModel>> ListaTiempo(int Tiempo)
        {
            int MinTiempo = 0;
            switch (Tiempo)
            {
                case 5: MinTiempo = 0; break;
                case 15: MinTiempo = 5; break;
                case 30: MinTiempo = 15; break;
                case 999: MinTiempo = 30; break;
            }

            List<RecetaModel> mLista = new List<RecetaModel>();

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, calificacion
                            FROM receta
                            WHERE tiempo <= @tiempo  and tiempo > @mintiempo and estado > 0;";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@tiempo", Tiempo);
                            command.Parameters.AddWithValue("@mintiempo", MinTiempo);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel()
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Calificacion = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Dif | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<List<RecetaModel>> ListaRecetas()
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, calificacion
                            FROM receta
                            WHERE estado > 0";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel()
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Calificacion = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Dif | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
        }

        public static async Task<string> ListaSugerida(int IdUser)
        {
            string recetas = "";

            string query = @"SELECT
                            nombre
                            FROM receta
                            WHERE estado = 1 and iduser = @iduser
                            ORDER BY id DESC
                            LIMIT 4";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@iduser", IdUser);
                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var Nombre = !reader.IsDBNull(0) ? reader.GetString(0) : "";

                                    recetas = recetas + " " + Nombre;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Dif | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return recetas;
        }

        public static async Task<List<RecetaModel>> ListaRecetaNombre(string Nombre)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            string query = @"SELECT
                            id, nombre, ndif, tiempo, categoria, calificacion
                            FROM receta
                            WHERE nombre LIKE @nombre and estado > 0";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", "%" + Nombre + "%");

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var receta = new RecetaModel()
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        NivelDificultad = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Tiempo = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0,
                                        Categoria = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Calificacion = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0
                                    };

                                    mLista.Add(receta);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Receta lista Nombre| " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }

            return mLista;
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

        public static async Task<RpstaModel> ActEstado(int recetaId,int Estado)
        {
            RpstaModel model = new RpstaModel();

            string query = "UPDATE receta SET estado = @estado " +
                           "WHERE id = @id";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@estado", Estado);
                        cmd.Parameters.AddWithValue("@id", recetaId);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Receta actualizada correctamente.";
                        }
                        else
                        {
                            model.Error = true;
                            model.Mensaje = "No se encontró la receta.";
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Editar receta | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }


            return model;
        }


        public static async Task<RpstaModel> Calificacion(int recetaId, int Puntos)
        {
            RpstaModel model = new RpstaModel();

            string query = "UPDATE receta SET calificacion = @calificacion " +
                           "WHERE id = @id";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@calificacion", Puntos);
                        cmd.Parameters.AddWithValue("@id", recetaId);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Receta actualizada correctamente.";
                        }
                        else
                        {
                            model.Error = true;
                            model.Mensaje = "No se encontró la receta.";
                        }
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Editar receta | " + ex.Message);
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
