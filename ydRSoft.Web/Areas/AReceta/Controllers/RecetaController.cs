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
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ObtenerRecetas()
        {
            string txtNombre = TxtProductos();
            var model = new RecetaModel();
            if (txtNombre.Count() > 0)
            {
                model = await RecetaBL.ObtenerRecetas(txtNombre);
            }

            return PartialView("_verPagina", model);
        }

        public async Task<ActionResult> verReceta(int Pagina)
        {
            var resultado = await RecetaBL.GetRecetaId(Pagina);

            return PartialView("_verReceta", resultado);
        }

        [HttpPost]
        public async Task<ActionResult> GetCategorias(int opc)
        {
            var resultado = await RecetaBL.GetRecetaCat(opc);

            return PartialView("_verPagina", resultado);
        }

        public async Task<JsonResult> AddFavoritos(int IdUser, int IdRe)
        {
            var resultado = await FavoritoBL.Agregar(IdUser, IdRe);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> VerFavoritos(int IdUser)
        {
            var resultado = await FavoritoBL.GetAll(IdUser);

            return PartialView("_verPagina", resultado);
        }
         
        private string TxtProductos()
        {
            string txtProd = "";

            List<ProductoModel> mLista = (List<ProductoModel>)Session["ListaXY"];
            if (mLista != null)
                txtProd = string.Join(" ", mLista);

            return txtProd;
        }

        public ActionResult VistaRecetas()
        {
            return PartialView("_vistaRecetas");
        }

        public async Task<ActionResult> ListaRecetas(string Categoria, int Dificultad)
        {
            var resultado = await RecetaBL.ListaFiltro(Categoria,Dificultad);

            return PartialView("_listaRecetas", resultado);
        }

        public async Task<ActionResult> ListaRecetaNombre(string Nombre)
        {
            var resultado = await RecetaBL.ListaFiltroNombre(Nombre);

            return PartialView("_listaRecetas", resultado);
        }
    }
}