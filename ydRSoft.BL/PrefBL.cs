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

        public static async Task<RpstaModel> Eliminar(int IdPref)
        {
            var resultado = await PrefBD.EditarPref(IdPref, 0);

            return resultado;
        }

        public static async Task<List<PrefModel>> ListaPref(int Id, int Estado)
        {
            List<PrefModel> mLista;

            if (Estado == 0)
            {
                mLista = await PrefBD.GetPref(Id);
            }
            else
            {
                mLista = await PrefBD.GetPref(Id, Estado);
            }            

            return mLista;

        }
    }
}
