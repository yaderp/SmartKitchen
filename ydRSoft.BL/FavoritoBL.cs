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
            var duplicado = await FavoritoBD.IsDuplicado(IdUser, IdRe);
            if (!duplicado)
            {
                rpstaModel=await FavoritoBD.Guardar(IdUser, IdRe);
            }

            return rpstaModel;
        }

        public static async Task<RpstaModel> ActEstado(int IdUser, int recetaId)
        {
            var resultado = await FavoritoBD.ActEstado(IdUser, recetaId, 0);

            return resultado;
        }

        public static async Task<RpstaModel> ActEstado(int IdFav)
        {
            var resultado = await FavoritoBD.ActEstado(IdFav, 0);

            return resultado;
        }

        public static async Task<RecetaModel> GetAll(int IdUser)
        {
            var mLista = await FavoritoBD.GetAll(IdUser);
            var objModel = new RecetaModel(mLista);

            return objModel;
        }

        public static async Task<List<RecetaModel>> ListaFavoritos(int IdUser, string Nombre)
        {
            List<RecetaModel> mLista;

            if (string.IsNullOrEmpty(Nombre))
            {
                mLista = await FavoritoBD.ListaFavoritos(IdUser);
            }
            else
            {
                mLista = await FavoritoBD.ListaFavoritos(IdUser,Nombre);
            }            

            return mLista;
        }

    }
}
