using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            UsuarioModel model = (UsuarioModel)Session["objUser"];

            //var mUser = await UsuarioBL.GetUsuarioId(2);

            //Session["objUser"] = mUser;

            //return RedirectToAction("Index", "Configuracion", new { Area = "AConfiguracion" });
            //return RedirectToAction("About", "Home", new { Area = string.Empty });

            if (model == null)
            {
                return RedirectToAction("Login");
            }

            await InfoSing.Instance.LoadProductos();
            await SugerenciaBL.LoadSugerencia(model.Id);

            return View(model);
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}