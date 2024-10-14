using MySql.Data.MySqlClient;
using System;
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

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                await connection.OpenAsync();
                try
                {
                    string insertRecetaQuery = @"INSERT INTO favoritos (iduser, idreceta, fechareg, estado) 
                                             VALUES (@iduser, @idreceta, @fechareg, @estado);
                                             SELECT LAST_INSERT_ID();";


                    using (MySqlCommand command = new MySqlCommand(insertRecetaQuery, connection))
                    {
                        // Parámetros para la receta
                        command.Parameters.AddWithValue("@iduser", IdUser);
                        command.Parameters.AddWithValue("@idreceta", IdRe);
                        command.Parameters.AddWithValue("@fechareg", DateTime.Now);
                        command.Parameters.AddWithValue("@estado", 1);

                        // Ejecutar el comando y obtener el ID de la receta insertada
                        var IdFav = Convert.ToInt32(await command.ExecuteScalarAsync());
                        if (IdFav > 0) {
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
                    await connection.CloseAsync();
                }
            }

            return rpstaModel;
        }

        public static async Task<List<FavoritoModel>> GetAll(int IdUser)
        {
            List<FavoritoModel> mLista = new List<FavoritoModel>();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    // Consulta para obtener solo la receta
                    string query = @"
                    SELECT r.id, r.iduser, r.idreceta
                    FROM favoritos r
                    WHERE r.iduser = @iduser";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iduser", IdUser);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var fav = new FavoritoModel
                                {
                                    Id = reader.GetInt32("id"),
                                    IdUser = reader.GetInt32("iduser"),
                                    IdRe = reader.GetInt32("idreceta")

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
                    await connection.CloseAsync();
                }
            }

            return mLista;
        }

        public static async Task<bool> GetDuplicado(int IdUser, int IdRe)
        {
            string query = "SELECT * FROM favoritos WHERE iduser = @iduser and idreceta = @idreceta";
           bool encontrado = false;

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync(); // Abrir la conexión de forma asíncrona

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@iduser", IdUser);
                    cmd.Parameters.AddWithValue("@idreceta", IdRe);

                    using (MySqlDataReader reader = cmd.ExecuteReader()) // Ejecutar la lectura de forma asíncrona
                    {
                        if (await reader.ReadAsync()) // Leer los datos de forma asíncrona
                        {
                            var favorito = new FavoritoModel
                            {
                                Id = reader.GetInt32("id"),
                                IdUser = reader.GetInt32("iduser"),
                                IdRe = reader.GetInt32("idreceta")
                            };

                            encontrado = true;
                        }
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
            return encontrado;
        }
    }
}
