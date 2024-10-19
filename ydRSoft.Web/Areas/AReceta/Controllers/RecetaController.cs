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

            //RecetaModel model = await RecetaBL.GetRecetaId(2);
            //model.ListaId = new List<int>() { 2, 3, 1, 4 };

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
            string txtcat = "postres";
            switch (opc)
            {
                case 1: txtcat = "entradas"; break;
                case 2: txtcat = "sopas"; break;
                case 3: txtcat = "principal"; break;
                default: txtcat = "postre"; break;

            }

            var mlista = await RecetaBL.GetRecetaCat(txtcat);

            RecetaModel model = new RecetaModel();

            if (mlista != null)
            {
                if (mlista.Count > 0)
                {
                    List<int> lista = new List<int>();
                    model = mlista[0];
                    foreach (var item in mlista)
                    {
                        lista.Add(item.Id);
                    }
                    model.ListaId = lista;
                }

            }

            return PartialView("_verPagina", model);
        }

        public async Task<JsonResult> AddFavoritos(int IdUser, int IdRe)
        {
            var resultado = await FavoritoBL.Agregar(IdUser, IdRe);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> VerFavoritos(int IdUser)
        {
            var resultado = await FavoritoBL.Mostrar(IdUser);

            return PartialView("_verPagina", resultado);
        }

        public ActionResult VerCategorias()
        {
            return PartialView("_verCategorias");
        }

        private string TxtProductos()
        {
            string txtProd = "";

            List<ProductoModel> mLista = (List<ProductoModel>)Session["ListaXY"];

            if (mLista != null)
            {
                foreach (var item in mLista)
                {
                    txtProd = txtProd + " " + item.Nombre;
                }
            }

            return txtProd;
        }
    }
}