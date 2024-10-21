using Newtonsoft.Json;
using System;
using System.Collections;
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
            var mLista = await ConsultarOpenAI(TxtProd);
            var objModel = await GuardarConsulta(mLista);

            return objModel;
         }

        public static async Task<RpstaModel> Guardar(RecetaModel objModel)
        {
            RpstaModel rpstaModel = new RpstaModel();

            try
            {
                if (objModel == null || objModel.Ingredientes == null || objModel.PasosPreparacion == null)
                    return new RpstaModel();

                objModel.StrIngre = ListaToStr(objModel.Ingredientes);
                objModel.StrPas = ListaToStr(objModel.PasosPreparacion);

                rpstaModel = await RecetaBD.Guardar(objModel);

            }
            catch (Exception ex)
            {
                await Util.LogError.SaveLog("guardara Receta BL " + ex.Message);
            }

            return rpstaModel;
        }

        public static async Task<RecetaModel> GetRecetaId(int IdR)
        {
            var receta = await RecetaBD.GetRecetaId(IdR);
            if (receta != null && receta.StrIngre != null && receta.StrPas != null)
            {
                receta.Ingredientes = StrToLista(receta.StrIngre);
                receta.PasosPreparacion = StrToLista(receta.StrPas);
            }

            return receta;
        }

        public static async Task<RecetaModel> GetRecetaCat(int opc)
        {
            string Categoria = "postres";
            switch (opc)
            {
                case 1: Categoria = "entradas"; break;
                case 2: Categoria = "sopas"; break;
                case 3: Categoria = "principal"; break;
                default: Categoria = "postres"; break;
            }

            var mlista = await RecetaBD.GetRecetaCat(Categoria);
            var objModel = new RecetaModel(mlista);
            

            return objModel;
        }

        public static async Task<RecetaModel> ListaFavorito(int IdUser)
        {
            var mLista = await FavoritoBD.GetAll(IdUser);
            var objModel = new RecetaModel(mLista);
            return objModel;
        }
               

        public static async Task<List<RecetaModel>> ListaFiltro(string Categoria,int Dificultad)
        {
            List<RecetaModel> lista = new List<RecetaModel>();

            if (string.IsNullOrEmpty(Categoria) && Dificultad == 0) {

                lista = await RecetaBD.ListaRecetas();
            }
            else
            {
                if (Dificultad == 0)
                {
                    lista = await RecetaBD.ListaCategoria(Categoria);
                }
                else
                {
                    lista = await RecetaBD.ListaDificultad(Dificultad);
                }
            }

            return lista;
        }

        public static async Task<List<RecetaModel>> ListaFiltroNombre(string Nombre)
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                return new List<RecetaModel>();
            }

            return await RecetaBD.ListaRecetaNombre(Nombre);
        }

        #region OpenAi
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

        public static async Task<RecetaModel> GuardarConsulta(List<RecetaModel> mLista)
        {
            RecetaModel recetaModel = new RecetaModel();
            List<int> listaId = new List<int>();

            if (mLista == null || mLista.Count == 0)
            {
                return recetaModel;
            }

            try
            {
                foreach (var item in mLista)
                {
                    var receta = await RecetaBD.GetRecetaNom(item.Nombre);
                    if (receta != null)
                    {           
                        listaId.Add(receta.Id);

                        if (listaId.Count == 0)
                        {
                            recetaModel = receta;
                        }
                    }
                    else
                    {
                        var resultado = await Guardar(item);
                        if (!resultado.Error)
                        {
                            if (listaId.Count == 0)
                            {
                                recetaModel = item;
                                recetaModel.Id = resultado.Id;
                            }

                            listaId.Add(resultado.Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Util.LogError.SaveLog("Guardar Receta " + ex.Message);
            }

            recetaModel.ListaId = listaId;
            return recetaModel;
        }
        
        #endregion

        private static string ListaToStr(List<string> mLista)
        {
            string txtlista = string.Empty;

            if (mLista == null) return txtlista;

            txtlista = string.Join("|", mLista);

            return txtlista;
        }

        private static List<string> StrToLista(string txtlista)
        {
            if (string.IsNullOrEmpty(txtlista)) return new List<string>();

            var mLista = txtlista.Split('|').ToList();

            return mLista;
        }

    }
}
