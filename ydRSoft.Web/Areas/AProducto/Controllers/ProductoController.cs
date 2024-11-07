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
            var model = await ProductoBL.GetInformacionN("PLÁTANO");
            model.PosX = 350;
            model.PosY = 150;
            model.Radio = 130;

            var model2 = await ProductoBL.GetInformacionN("MANZANA");
            model2.PosX = 800;
            model2.PosY = 200;
            model2.Radio = 90;

            List<ProductoModel> mLista = new List<ProductoModel> { model, model2 };

            //Session["ListaXY"] = mLista;
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

        public async Task<JsonResult> EditarNombre(int IdProd, string Nombre)
        {
            var resultado = await ProductoBL.EditarNombre(IdProd,Nombre);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EditarId(int prodId, int newId)
        {
            var resultado = await ProductoBL.EditarId(prodId, newId);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> EditarProducto(int prodId)
        {
            var producto = await ProductoBL.GetProductoId(prodId);

            return PartialView("_editarProducto", producto);
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