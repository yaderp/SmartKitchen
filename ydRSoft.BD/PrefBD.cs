using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
    public class PrefBD
    {
        public static async Task<RpstaModel> Guardar(PrefModel objModel)
        {

            RpstaModel model = new RpstaModel();

            if (objModel == null || objModel.IdUser <= 0 || objModel.IdProd <= 0)
            {
                model.Mensaje = "Parámetros inválidos";
                return model;
            }

            string query = "INSERT INTO preferencias (iduser, idprod, fechareg, estado) " +
                           "VALUES (@iduser, @idprod, @fechareg, @estado)";

            using (MySqlConnection conexion = MySqlConexion.MyConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        await conexion.OpenAsync();

                        using (MySqlCommand cmd = new MySqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@iduser", objModel.IdUser);
                            cmd.Parameters.AddWithValue("@idprod", objModel.IdProd);
                            cmd.Parameters.AddWithValue("@fechareg", DateTime.Now);
                            cmd.Parameters.AddWithValue("@estado", objModel.Estado);

                            var respuesta = await cmd.ExecuteNonQueryAsync();

                            if (respuesta > 0)
                            {
                                model.Error = false;
                                model.Mensaje = "Correcto";
                            }
                        }
                    }
                    catch (MySqlException mysqlEx)
                    {
                        model.Mensaje = mysqlEx.Message;
                        await Util.LogError.SaveLog("Guardar Prefer |" + mysqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        model.Mensaje = ex.Message;
                        await Util.LogError.SaveLog("Guardar Pref |" + ex.Message);
                    }
                    finally
                    {
                        await conexion.CloseAsync();
                    }
                }
            }

            return model;
        }

        public static async Task<List<PrefModel>> GetPref(int IdUser,int Estado)
        {
            List<PrefModel> mLista = new List<PrefModel>();

            string query = @"SELECT r.id, r.iduser, r.idprod, p.nombre, r.estado
                             FROM preferencias r
                             inner join producto p on r.idprod = p.id
                             WHERE r.iduser = @iduser and r.estado = @estado";

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
                            command.Parameters.AddWithValue("@estado", Estado);

                            using (DbDataReader reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var pref = new PrefModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        IdUser = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                                        IdProd = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Nombre = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                        Estado = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0
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
            }

            return mLista;
        }

        public static async Task<List<PrefModel>> GetPref(int IdUser)
        {
            List<PrefModel> mLista = new List<PrefModel>();
            string query = @"SELECT r.id, r.iduser, r.idprod, p.nombre, r.estado
                             FROM preferencias r
                             inner join producto p on r.idprod = p.id
                             WHERE r.iduser = @iduser and r.estado > 0";

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
                                    var pref = new PrefModel
                                    {
                                        Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                                        IdUser = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0,
                                        IdProd = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0,
                                        Nombre = !reader.IsDBNull(3) ? reader.GetString(3) : "",
                                        Estado = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0
                                    };

                                    mLista.Add(pref);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        await Util.LogError.SaveLog("Pref Get IdUser | " + ex.Message);
                    }
                    finally
                    {
                        await connection.CloseAsync();
                    }
                }
            }


            return mLista;
        }

        public static async Task<RpstaModel> EditarPref(int Id, int Estado)
        {
            RpstaModel model = new RpstaModel();

            string query = @"UPDATE preferencias 
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
