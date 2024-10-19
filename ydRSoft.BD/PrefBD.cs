using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class PrefBD
    {
        public static async Task<RpstaModel> Guardar(PrefModel objModel)
        {

            RpstaModel model = new RpstaModel();

            string query = "INSERT INTO preferencias (iduser, idprod, fechareg, estado) " +
                           "VALUES (@iduser, @idprod, @fechareg, @estado)";

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync();

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@iduser", objModel.IdUser);
                    cmd.Parameters.AddWithValue("@idprod", objModel.IdProd);          
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

        public static async Task<List<PrefModel>> GetPref(int IdUser,int Estado)
        {
            List<PrefModel> mLista = new List<PrefModel>();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT r.id, r.iduser, r.idprod, p.nombre, r.estado
                    FROM preferencias r
                    inner join producto p on r.idprod = p.id
                    WHERE r.iduser = @iduser and r.estado = @estado";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iduser", IdUser);
                        command.Parameters.AddWithValue("@estado", Estado);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var pref = new PrefModel
                                {
                                    Id = reader.GetInt32("id"),
                                    IdUser = reader.GetInt32("iduser"),
                                    IdProd = reader.GetInt32("idprod"),
                                    Nombre = reader.GetString("nombre"),
                                    Estado = reader.GetInt32("estado")
                                };

                                mLista.Add(pref);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("Pref Get | " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return mLista;
        }



        public static async Task<List<PrefModel>> GetPref(int IdUser)
        {
            List<PrefModel> mLista = new List<PrefModel>();

            MySqlConnection connection = MySqlConexion.MyConexion();
            if (connection != null)
            {
                try
                {
                    await connection.OpenAsync();

                    string query = @"
                    SELECT r.id, r.iduser, r.idprod, p.nombre, r.estado
                    FROM preferencias r
                    inner join producto p on r.idprod = p.id
                    WHERE r.iduser = @iduser";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@iduser", IdUser);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (await reader.ReadAsync())
                            {
                                var pref = new PrefModel
                                {
                                    Id = reader.GetInt32("id"),
                                    IdUser = reader.GetInt32("iduser"),
                                    IdProd = reader.GetInt32("idprod"),
                                    Nombre = reader.GetString("nombre"),
                                    Estado = reader.GetInt32("estado")
                                };

                                mLista.Add(pref);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("Pref Get | " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }

            return mLista;
        }



        public static async Task<RpstaModel> EditarPref(int Estado, int Id)
        {
            RpstaModel model = new RpstaModel();

            string query = @"UPDATE usuario 
                            SET estado = @estado
                            WHERE id = @id";

            MySqlConnection conexion = MySqlConexion.MyConexion();
            if (conexion != null)
            {
                try
                {
                    await conexion.OpenAsync();

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@estado", Estado);
                    cmd.Parameters.AddWithValue("@id", Id);

                    var respuesta = await cmd.ExecuteNonQueryAsync();
                    if (respuesta > 0)
                    {
                        model.Error = false;
                        model.Mensaje = "Pref actualizado correctamente.";
                    }
                    else
                    {
                        model.Error = true;
                        model.Mensaje = "No se encontró la Pref.";
                    }
                }
                catch (Exception ex)
                {
                    model.Mensaje = ex.Message;
                    await Util.LogError.SaveLog("Pref editar | " + ex.Message);
                }
                finally
                {
                    await conexion.CloseAsync();
                }
            }

            return model;
        }

    }
}
