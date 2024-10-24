using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;

namespace ydRSoft.Web.Areas.ASugerencia.Controllers
{
    public class SugerenciaController : Controller
    {
        // GET: ASugerencia/Sugerencia
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LoadSugerencia(int recetaId)
        {
            var resultado = await RecetaBL.GetRecetaId(recetaId);
            return PartialView("_loadSugerencia", resultado);
        }

        public async Task<ActionResult> AlertaSug()
        {
            var resultado = await SugerenciaBL.ConsultarSuger();
            return PartialView("_alertaSug", resultado);
        }

        public ActionResult VistaSugerencia()
        {            
            return PartialView("_vistaSugerencia");
        }

        public async Task<ActionResult> ListaSugerencia(string Nombre)
        {
            var resultado = await SugerenciaBL.ListaSugerencia(Nombre);
            return PartialView("_listaSugerencia", resultado);
        }

        public async Task<JsonResult> ActSugerencia(int IdSug)
        {
            var resultado = await SugerenciaBL.ActEstado(IdSug);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}