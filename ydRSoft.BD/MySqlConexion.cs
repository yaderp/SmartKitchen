using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.BD
{
    public class MySqlConexion
    {
        public static MySqlConnection MyConexion()
        {
            string context = "Database=dbydrconta; Data Source = localhost; User Id=ydrsoft; Password=Palomita16";
            try
            {
                MySqlConnection conexionBD = new MySqlConnection(context);
                return conexionBD;
            }
            catch
            {
                return null;
            }
        }
    }
}
