using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Areas.AProducto.Controllers
{
    public class ProductoController : Controller
    {
        // GET: AProducto/Producto
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async  Task<JsonResult> Prueba(string image64)
        {
            var model = await ProductoBL.GetInformacionN("PLATANO");
            model.PosX = 300;
            model.PosY = 300;
            model.Radio = 280;

            var model2 = await ProductoBL.GetInformacionN("ZANAHORIA");
            model2.PosX = 950;
            model2.PosY = 350;
            model2.Radio = 150;

            List<ProductoModel> mLista = new List<ProductoModel> { model, model2 };
            return Json(mLista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VistaProductos()
        {
            return PartialView("_vistaProductos");
        }

        public ActionResult LoadProductos()
        {
            return PartialView("_loadProductos");
        }

        public async Task<ActionResult> ListaProductos()
        {
            var mLista = await ProductoBL.ListaProducto();
            mLista = mLista.OrderBy(x => x.Nombre).ToList();
            return PartialView("_listaProductos", mLista);
        }

        public async Task<ActionResult> BuscarProducto(string Nombre)
        {
            var modelo = await ProductoBL.GetInformacionN(Nombre);
            
            return PartialView("_newProducto", modelo);
        }

        [HttpPost]
        public async Task<JsonResult> GuardarInfoN(ProductoModel objModel)
        {
            var resultado = await ProductoBL.Guardar(objModel);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> ActProducto(int IdProd)
        {
            var resultado = await ProductoBL.ActProducto(IdProd, 0);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> ConsultarApi(string image64)
        {
            List<ProductoModel> listaSession = (List<ProductoModel>)Session["ListaXY"];
            var listaProd = await ProductoBL.ProdDetectados(image64, listaSession);

            Session["ListaXY"] = listaProd;
            return Json(listaProd, JsonRequestBehavior.AllowGet);
        }
    }
}