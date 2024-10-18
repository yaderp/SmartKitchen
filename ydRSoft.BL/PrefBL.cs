using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class PrefBL
    {
        public static async Task<RpstaModel> Agregar(PrefModel objModel)
        {

            var resultado = await PrefBD.Guardar(objModel);

            return resultado;
        }

        public static async Task<List<PrefModel>> ListaPref(int Id, int Estado)
        {
            var resultado = await PrefBD.GetPref(Id,Estado);

            return resultado;

        }
    }
}
