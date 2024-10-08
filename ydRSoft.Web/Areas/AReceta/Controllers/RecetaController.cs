using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Areas.AReceta.Controllers
{
    public class RecetaController : Controller
    {
        // GET: AReceta/Receta
        public async Task<ActionResult>  Index()
        {
            string pregunta = "Genera 2 recetas que contengan " +
                "zanahoria y brócoli" +
                ". Proporciónalas en formato JSON que incluya un " +
                "Id, Nombre, NivelDificultad (1 a 5), Tiempo (en minutos), " +
                "una lista de Ingredientes, y una lista de PasosPreparacion. " +
                "Devuelve solo el JSON. que comience y termine con corchetes ([y]) para indicar que es una lista y " +
                "que no haya caracteres no deseados al principio o al final.";

            var resultado = await PreguntaOpenAI(pregunta);
            return View();
        }

        public ActionResult verReceta(int Pagina)
        {
            var resultado = RecetaBL.GetReceta(Pagina);

            return PartialView("_verReceta", resultado);
        }

        public async Task<JsonResult> PreguntarApi(string txtPregunta)
        {
            var resultado = await PreguntaOpenAI(txtPregunta);

            return Json( resultado, JsonRequestBehavior.AllowGet);
        }

        private async Task<string> PreguntaOpenAI(string txtPregunta)
        {
            
            var resultado = await ApiOpenAI.PreguntaApi(txtPregunta);
            string jsonResponse = ApiOpenAI.MensajeContent(resultado);
            try {
                List<RecetaModel> recetas = JsonConvert.DeserializeObject<List<RecetaModel>>(jsonResponse);
            } catch { 
                
            }
            

            return jsonResponse;
        }
    }
}