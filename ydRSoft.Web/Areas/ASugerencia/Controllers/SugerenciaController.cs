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

        public async Task<ActionResult> LoadSugerencia()
        {
            var resultado = await SugerenciaBL.ConsultarSuger();
            return PartialView("_loadSugerencia", resultado);
        }
    }
}