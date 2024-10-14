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
        public ActionResult  Index()
        {            
            return View();
        }

        public async Task<ActionResult> ObtenerRecetas()
        {
            string txtNombre = TxtProductos();
            //var resultado = new RecetaModel();
            //if (txtNombre.Count() > 0) {
            //    resultado = await RecetaBL.ObtenerRecetas(txtNombre);
            //}

            var resultado = await RecetaBL.GetReceta(187);
            resultado.ListaId = new List<int>() { 211, 214, 218 };

            return PartialView("_verPagina", resultado);
        }

        public async Task<ActionResult> verReceta(int Pagina)
        {            
            var resultado = await RecetaBL.GetReceta(Pagina);

            return PartialView("_verReceta", resultado);
        }

        private string TxtProductos()
        {
            string txtProd = "";

            List<ProductoModel> mLista = (List<ProductoModel>)Session["ListaXY"];

            if (mLista != null)
            {
                foreach (var item in mLista) {
                    txtProd = txtProd+" " + item.Nombre;
                }
            }

            return txtProd;
        }
    }
}