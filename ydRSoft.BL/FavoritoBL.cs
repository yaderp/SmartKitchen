using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class FavoritoBL
    {
        public static async Task<RpstaModel> Agregar(int IdUser, int IdRe)
        {
            RpstaModel rpstaModel = new RpstaModel(true,"Ya es tu Favorito");
            var duplicado = await FavoritoBD.GetDuplicado(IdUser, IdRe);
            if (!duplicado)
            {
                rpstaModel=await FavoritoBD.Guardar(IdUser, IdRe);
            }

            return rpstaModel;
        }

        public static async Task<RecetaModel> Mostrar(int IdUser)
        {
            var mLista = await FavoritoBD.GetAll(IdUser);

            RecetaModel recetaModel = new RecetaModel();
            if(mLista!= null)
            {
                if (mLista.Count > 0)
                {                    
                    List<int> lista = new List<int>();
                    foreach (var item in mLista)
                    {
                        lista.Add(item.IdRe);
                    }

                    recetaModel = await RecetaBD.GetRecetaId(mLista[0].IdRe);
                    recetaModel.ListaId = lista;
                }
            }

            return recetaModel;
        }
    }
}
