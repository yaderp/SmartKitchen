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
            else
            {
                //model = await RecetaBL.GetRecetaId(2);
                //model.ListaId = new List<int> { 2, 1 };
            }

            return PartialView("_verPagina", model);
        }

        public async Task<ActionResult> BuscarReceta(string Nombre,string Region)
        {
            var receta = new RecetaModel();

            if (!string.IsNullOrEmpty(Nombre))
            {
                receta = await RecetaBL.ConsultarNewReceta(Nombre,Region, 0);
            }

            //var receta = await RecetaBL.GetRecetaId(2);

            return PartialView("_newReceta", receta);
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

        public async Task<JsonResult> ActEstado(int recetaId,int Estado)
        {
            var resultado = await RecetaBL.ActEstado(recetaId, Estado);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Calificar(int recetaId, int Calificacion)
        {
            var resultado = await RecetaBL.Calificacion(recetaId, Calificacion);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CargaReceta(int recetaId)
        {
            var resultado = await RecetaBL.GetRecetaId(recetaId);

            return PartialView("_newReceta", resultado);
        }


        public async Task<JsonResult> AddFavoritos(int IdUser, int IdRe)
        {
            var resultado = await FavoritoBL.Agregar(IdUser, IdRe);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddDelFav(int IdUser, int recetaId, int Estado)
        {
            RpstaModel resultado = new RpstaModel();
            if (Estado == 0)
            {
                resultado = await FavoritoBL.ActEstado(IdUser, recetaId);
            }
            else
            {
                resultado = await FavoritoBL.Agregar(IdUser, recetaId);
            }
            
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
                txtProd = string.Join(" ", mLista.Select(x=>x.Nombre));

            return txtProd;
        }

        public ActionResult VistaRecetas()
        {
            return PartialView("_vistaRecetas");
        }

        public async Task<ActionResult> ListaRecetas(int IdUser,string Categoria, int Dificultad,int Tiempo)
        {
            var listafav = (await RecetaBL.ListaFavorito(IdUser)).ListaId;
            var mLista = await RecetaBL.ListaFiltro(Categoria, Dificultad, Tiempo);

            
            if (listafav != null && mLista != null)
            {

                foreach (var item in mLista)
                {
                    if (listafav.Exists(x => x == item.Id))
                    {
                        item.Isfavorito = true;
                    }
                }
            }



            return PartialView("_listaRecetas", mLista);
        }

        public async Task<ActionResult> ListaRecetaNombre(string Nombre)
        {
            var resultado = await RecetaBL.ListaFiltroNombre(Nombre);

            return PartialView("_listaRecetas", resultado);
        }
    }
}