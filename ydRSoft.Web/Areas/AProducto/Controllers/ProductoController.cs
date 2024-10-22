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
            List<ProductoModel> mLista = new List<ProductoModel>();
            mLista.Add(new ProductoModel(21,"PLATANO",300,280,250));
            mLista.Add(new ProductoModel(34,"MANZANA",900,220,180));
            mLista.Add(new ProductoModel(12, "ZANAHORIA", 750, 650, 200));

            mLista = await InfoBL.CargarInfo(mLista);

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