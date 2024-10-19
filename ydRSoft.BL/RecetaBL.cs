using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class RecetaBL
    {

        public static async Task<RecetaModel> ObtenerRecetas(string TxtProd)
        {
            var objModel = new RecetaModel();

            var mLista = await ConsultarOpenAI(TxtProd);
            var listaId = await Guardar(mLista);

            if (listaId != null)
            {
                if (listaId.Count > 0)
                {
                    objModel = await GetRecetaId(listaId[0]);
                    objModel.ListaId = listaId;
                }
            }

            return objModel;
         }

        public static async Task<RecetaModel> GetRecetaId(int IdR)
        {
            var receta = await RecetaBD.GetRecetaId(IdR);
            return receta;
        }

        public static async Task<List<RecetaModel>> GetRecetaCat(string Categoria)
        {
            var mlista = await RecetaBD.GetRecetaCat(Categoria);
            return mlista;
        }

        public static async Task<RecetaModel> GetRecetaFav(int IdUser)
        {
            var receta = await RecetaBD.GetRecetaId(IdUser);
            return receta;
        }

        public static async Task<List<RecetaModel>> ConsultarOpenAI(string txtProducto)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            string txtPregunta = "Genera " + Util.Variables.CantRecetas +
                " recetas que contengan " + txtProducto +
                ". Proporciónalas en formato JSON que incluya un " +
                "Id, Nombre, NivelDificultad (1 a 5), Tiempo (en minutos), Categoria (entrada o sopa o principal o postre ) " +
                "una lista de Ingredientes, y una lista de PasosPreparacion. " +
                "Devuelve solo el JSON. que comience y termine con corchetes ([y]) para indicar que es una lista y " +
                "que no haya caracteres no deseados al principio o al final.";

            var resultado = await ApiOpenAI.PreguntaApi(txtPregunta);
            string jsonResponse = ApiOpenAI.MensajeContent(resultado);
            try
            {
                mLista = JsonConvert.DeserializeObject<List<RecetaModel>>(jsonResponse);
            }
            catch
            {

            }

            return mLista;
        }

        public static async Task<List<int>> Guardar(List<RecetaModel> mLista)
        {
            List<int> listaId = new List<int>();
            try {
                if (mLista != null)
                {                    
                    foreach (var item in mLista)
                    {
                        var recetaId = await ExisteReceta(item.Nombre);
                        if (recetaId==0)
                        {
                            recetaId = await RecetaBD.Guardar(item);
                        }                        

                        if (recetaId > 0) listaId.Add(recetaId);
                    }
                }
            }
            catch(Exception ex) {
                await Util.LogError.SaveLog("Guardar Receta " + ex.Message);
            }

            return listaId;
        }

        private static async Task<int> ExisteReceta(string Nombre)
        {
            var receta = await RecetaBD.GetRecetaNom(Nombre);
            if(receta != null) return receta.Id;

            return 0;
        }
    }
}
