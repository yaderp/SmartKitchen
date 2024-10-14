using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;

namespace ydRSoft.Web.Areas.AConfiguracion.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: AConfiguracion/Configuracion
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult VerConfig()
        {            

            return PartialView("_verConfig");
        }
    }
}