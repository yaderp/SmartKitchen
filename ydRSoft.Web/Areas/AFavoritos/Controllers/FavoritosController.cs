using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;

namespace ydRSoft.Web.Areas.AFavoritos.Controllers
{
    public class FavoritosController : Controller
    {
        // GET: AFavoritos/Favoritos
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LoadFavoritos(int recetaId)
        {
            var resultado = await RecetaBL.GetRecetaId(recetaId);

            return PartialView("_loadFavoritos",resultado);
        }

        public ActionResult VistaFavoritos()
        {
            return PartialView("_vistaFavoritos");
        }

        public async Task<ActionResult> ListaFavoritos(int IdUser,string Nombre)
        {
            var resultado = await FavoritoBL.ListaFavoritos(IdUser, Nombre);

            return PartialView("_listaFavoritos", resultado);
        }

        public async Task<JsonResult> ActFavoritos(int IdFav)
        {
            var resultado = await FavoritoBL.ActEstado(IdFav);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}