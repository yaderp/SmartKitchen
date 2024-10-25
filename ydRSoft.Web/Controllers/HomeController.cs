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
            await InfoSing.Instance.LoadProductos();
            await SugerenciaBL.LoadSugerencia();

            var mUser = await UsuarioBL.GetUsuarioId(2);

            Session["objUser"] = mUser;
            //return RedirectToAction("Index", "Configuracion", new { Area = "AConfiguracion" });

            UsuarioModel model = (UsuarioModel)Session["objUser"];
            if (model == null)
            {
                return RedirectToAction("Login");
            }

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