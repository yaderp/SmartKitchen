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
            return PartialView("vistaProductos");
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