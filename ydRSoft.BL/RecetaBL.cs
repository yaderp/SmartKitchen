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

        public static RecetaModel GetReceta(int Pagina)
        {
            var receta = new RecetaModel();

            try {
                var lista = JsonConvert.DeserializeObject<List<RecetaModel>>(Util.Funciones.json);

                receta = lista.Where(x => x.Id == Pagina).FirstOrDefault();

                //var ingred = receta.Ingredientes;
                //for(int i=0;i<ingred.Count;i++) {
                //    ingred[i] = Util.Funciones.LetraMayuscula(ingred[i]);
                //}

                //receta.Ingredientes = ingred;
            }
            catch(Exception) { 
            
            } 

            return receta;
        }


        public static async Task<List<RecetaModel>> Lista(string txtProducto)
        {
            List<RecetaModel> mLista = new List<RecetaModel>();

            string txtPregunta = "Genera 1 recetas que contengan " + txtProducto +
                ". Proporciónalas en formato JSON que incluya un " +
                "Id, Nombre, NivelDificultad (1 a 5), Tiempo (en minutos), " +
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


        public static async Task<string> SetReceta(RecetaModel objModel)
        {

            //var resultado = await RecetaBD.Set(objModel);
            var resultado = await RecetaBD.SetAll(objModel);

            return resultado;
        }

    }
}
