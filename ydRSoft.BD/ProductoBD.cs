using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class ProductoBD
    {
        public static async Task<RpstaModel> Guardar(ProductoModel objModel) {

            RpstaModel model = new RpstaModel();

            string query = "INSERT INTO producto (nombre, calorias, proteinas, colesterol, fibra, carbohidratos,azucares,sodio,calcio,grasa, fechareg, estado) " +
                           "VALUES (@nombre, @calorias, @proteinas, @colesterol, @fibra, @carbohidratos,@azucares,@sodio,@calcio,@grasa, @fechareg, @estado)";

            using(MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre", objModel.Nombre);
                        cmd.Parameters.AddWithValue("@calorias", objModel.Calorias);
                        cmd.Parameters.AddWithValue("@proteinas", objModel.Proteinas);
                        cmd.Parameters.AddWithValue("@colesterol", objModel.Colesterol);
                        cmd.Parameters.AddWithValue("@fibra", objModel.Carbohidratos);
                        cmd.Parameters.AddWithValue("@carbohidratos", objModel.Fibra);
                        cmd.Parameters.AddWithValue("@azucares", objModel.Azucares);
                        cmd.Parameters.AddWithValue("@sodio", objModel.Sodio);
                        cmd.Parameters.AddWithValue("@calcio", objModel.Calcio);
                        cmd.Parameters.AddWithValue("@grasa", objModel.Grasa);
                        cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@estado", 1);

                        var respuesta = await cmd.ExecuteNonQueryAsync();
                        if (respuesta > 0)
                        {
                            model.Error = false;
                            model.Mensaje = "Correcto";
                        }
                    }
                    catch (MySqlException mysqlEx)
                    {
                        model.Mensaje = mysqlEx.Message;
                        await Util.LogError.SaveLog("Guardar Producto |" + mysqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Guardar Producto |" + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }
          

            return model;
        }

        public static async Task<ProductoModel> GetProducto(int IdProd)
        {
            ProductoModel producto = null;

            string query = @"SELECT 
                            id, nombre, calorias, proteinas, colesterol, fibra,
                            carbohidratos, azucares, sodio, calcio, grasa, estado
                            FROM producto WHERE id = @id";


            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", IdProd);

                            using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    producto = new ProductoModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        Calorias = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                        Proteinas = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                        Colesterol = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Fibra = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        Carbohidratos = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                                        Azucares = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                        Sodio = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                        Calcio = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                        Grasa = !reader.IsDBNull(10) ? reader.GetString(10) : "",
                                        Estado = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0
                                    };
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Nombre | " + ex.Message);
                        producto = null;
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return producto;
        }
        public static async Task<ProductoModel> GetProducto(string Nombre)
        {
            ProductoModel producto = null;

            if (string.IsNullOrWhiteSpace(Nombre))
            {
                return producto;
            }

            string query = @"SELECT 
                            Id, nombre, calorias, proteinas, colesterol, fibra,
                            carbohidratos, azucares, sodio, calcio, grasa, estado
                            FROM producto WHERE nombre = @nombre";


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

                            using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    producto = new ProductoModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0):0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1):"",
                                        Calorias = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                        Proteinas = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                        Colesterol = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Fibra = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        Carbohidratos = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                                        Azucares = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                        Sodio = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                        Calcio = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                        Grasa = !reader.IsDBNull(10) ? reader.GetString(10) : "",
                                        Estado = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0
                                    };
                                }
                            }
                        }
                            
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Nombre | " + ex.Message);
                        producto = null;
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }
               
            return producto;
        }

        public static async Task<RpstaModel> ActProducto(int IdProd,int Estado)
        {
            RpstaModel  model = new RpstaModel();              

            string query = @"UPDATE producto
                            SET estado = @estado 
                            WHERE id = @id";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@id", IdProd);
                            cmd.Parameters.AddWithValue("@estado", Estado);

                            var respuesta = await cmd.ExecuteNonQueryAsync();
                            if (respuesta > 0)
                            {
                                model.Error = false;
                                model.Mensaje = "producto actualizada correctamente.";
                            }
                            else
                            {
                                model.Error = true;
                                model.Mensaje = "No se encontró el producto.";
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Act | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return model;
        }

        public static async Task<RpstaModel> EditarId(int IdProd, int newId)
        {
            RpstaModel model = new RpstaModel();

            string query = @"UPDATE producto
                            SET id = @newid 
                            WHERE id = @id";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@newid", newId);
                            cmd.Parameters.AddWithValue("@id", IdProd);

                            var respuesta = await cmd.ExecuteNonQueryAsync();
                            if (respuesta > 0)
                            {
                                model.Error = false;
                                model.Id = newId;
                                model.Mensaje = "producto actualizada correctamente.";
                            }
                            else
                            {
                                model.Error = true;
                                model.Mensaje = "No se encontró el producto.";
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Act | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return model;
        }

        public static async Task<RpstaModel> EditarNombre(int IdProd, string Nombre)
        {
            RpstaModel model = new RpstaModel();

            string query = @"UPDATE producto
                            SET  nombre = @nombre
                            WHERE id = @id";

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
                            cmd.Parameters.AddWithValue("@id", IdProd);

                            var respuesta = await cmd.ExecuteNonQueryAsync();
                            if (respuesta > 0)
                            {
                                model.Error = false;
                                model.Mensaje = "producto actualizada correctamente.";
                            }
                            else
                            {
                                model.Error = true;
                                model.Mensaje = "No se encontró el producto.";
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Act | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return model;
        }

        public static async Task<RpstaModel> EditarProducto(ProductoModel objModel)
        {
            RpstaModel model = new RpstaModel();

            string query = @"
                UPDATE producto
                SET 
                    nombre = @nombre,
                    calorias = @calorias,
                    proteinas = @proteinas,
                    colesterol = @colesterol,
                    fibra = @fibra,
                    carbohidratos = @carbohidratos,
                    azucares = @azucares,
                    sodio = @sodio,
                    calcio = @calcio,
                    grasa = @grasa,
                WHERE id = @id";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {  
                            cmd.Parameters.AddWithValue("@nombre", objModel.Nombre);
                            cmd.Parameters.AddWithValue("@calorias", objModel.Calorias);
                            cmd.Parameters.AddWithValue("@proteinas", objModel.Proteinas);
                            cmd.Parameters.AddWithValue("@colesterol", objModel.Colesterol);
                            cmd.Parameters.AddWithValue("@fibra", objModel.Fibra);
                            cmd.Parameters.AddWithValue("@carbohidratos", objModel.Carbohidratos);
                            cmd.Parameters.AddWithValue("@azucares", objModel.Azucares);
                            cmd.Parameters.AddWithValue("@sodio", objModel.Sodio);
                            cmd.Parameters.AddWithValue("@calcio", objModel.Calcio);
                            cmd.Parameters.AddWithValue("@grasa", objModel.Grasa);
                            cmd.Parameters.AddWithValue("@id", objModel.Id);

                            var respuesta = await cmd.ExecuteNonQueryAsync();
                            if (respuesta > 0)
                            {
                                model.Error = false;
                                model.Mensaje = "producto actualizada correctamente.";
                            }
                            else
                            {
                                model.Error = true;
                                model.Mensaje = "No se encontró el producto.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Act | " + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return model;
        }

        public static async Task<List<ProductoModel>> ListaProd()
        {
            List<ProductoModel> mLista = new List<ProductoModel>();

            string query = @"SELECT 
                            Id, nombre, calorias, proteinas, colesterol, fibra,
                            carbohidratos, azucares, sodio, calcio, grasa, estado
                            FROM producto WHERE estado > 0";

            using (MySqlConnection connection = MySqlConexion.MyConexion())
            {
                if (connection != null)
                {
                    try
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            //command.Parameters.AddWithValue("@estado", 1);

                            using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var prod = new ProductoModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                                        Calorias = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                                        Proteinas = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                        Colesterol = !reader.IsDBNull(4) ? reader.GetString(4) : "",
                                        Fibra = !reader.IsDBNull(5) ? reader.GetString(5) : "",
                                        Carbohidratos = !reader.IsDBNull(6) ? reader.GetString(6) : "",
                                        Azucares = !reader.IsDBNull(7) ? reader.GetString(7) : "",
                                        Sodio = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                        Calcio = !reader.IsDBNull(9) ? reader.GetString(9) : "",
                                        Grasa = !reader.IsDBNull(10) ? reader.GetString(10) : "",
                                        Estado = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0
                                    };

                                    mLista.Add(prod);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Prod Getall | " + ex.Message);
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
            string query = "SELECT COUNT(*) FROM producto WHERE nombre = @nombre;";
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
                        encontrado = false;
                        await Util.LogError.SaveLog("Duplicado Producto " + ex.Message);
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
