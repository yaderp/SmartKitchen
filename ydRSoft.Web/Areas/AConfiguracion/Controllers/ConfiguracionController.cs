using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Areas.AConfiguracion.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: AConfiguracion/Configuracion
        public ActionResult Index()
        {
            UsuarioModel model = (UsuarioModel)Session["objUser"];
            if (model == null)
            {
                return RedirectToAction("Login", "Home", new { Area = string.Empty });
            }

            return View(model);
        }

        public  ActionResult VerConfig()
        {  
            return PartialView("_verConfig");
        }
    }
}