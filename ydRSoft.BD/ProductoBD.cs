using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class ProductoBD
    {
        public static async Task<RpstaModel> Guardar(InfoModel objModel) {

            RpstaModel model = new RpstaModel();

            string query = "INSERT INTO producto (nombre, calorias, proteinas, colesterol, fibra, carbohidratos,azucares,sodio,calcio,grasa, fechareg, estado) " +
                           "VALUES (@nombre, @calorias, @proteinas, @colesterol, @fibra, @carbohidratos,@azucares,@sodio,@calcio,@grasa, @fechareg, @estado)";

            MySqlConnection conexion = MySqlConexion.MyConexion();
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
                catch (Exception ex)
                {
                    model.Mensaje = ex.Message;
                    await Util.LogError.SaveLog("Guardar USuario |");
                }
                finally
                {
                    await conexion.CloseAsync();
                }
            }

            return model;
        }


        public static async Task<InfoModel> GetProducto(string Nombre)
        {
            string query = "SELECT * FROM producto WHERE nombre = @nombre";
            InfoModel mInfo = null;

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync();

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombre", Nombre);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        if (await reader.ReadAsync())
                        {
                            mInfo = new InfoModel
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Calorias = reader.GetString("calorias"),
                                Proteinas = reader.GetString("proteinas"),
                                Colesterol = reader.GetString("colesterol"),
                                Fibra = reader.GetString("fibra"),
                                Carbohidratos = reader.GetString("fibra"),
                                Azucares = reader.GetString("fibra"),
                                Sodio = reader.GetString("fibra"),
                                Calcio = reader.GetString("fibra"),
                                Grasa = reader.GetString("fibra")
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await conexion.CloseAsync();
                }
            }
            return mInfo;
        }


        public static async Task<List<InfoModel>> ListaProd()
        {
            List<InfoModel> mLista = new List<InfoModel>();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM producto WHERE estado > 0";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@estado", 1);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var prod  = new InfoModel
                                {
                                    Id = reader.GetInt32("id"),
                                    Nombre = reader.GetString("nombre"),
                                    Calorias = reader.GetString("calorias"),
                                    Proteinas = reader.GetString("proteinas"),
                                    Colesterol = reader.GetString("colesterol"),
                                    Fibra = reader.GetString("fibra"),
                                    Carbohidratos = reader.GetString("fibra"),
                                    Azucares = reader.GetString("fibra"),
                                    Sodio = reader.GetString("fibra"),
                                    Calcio = reader.GetString("fibra"),
                                    Grasa = reader.GetString("fibra")
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

            return mLista;
        }

    }
}
