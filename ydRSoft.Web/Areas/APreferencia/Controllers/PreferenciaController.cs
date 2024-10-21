using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Areas.APreferencia.Controllers
{
    public class PreferenciaController : Controller
    {
        // GET: APreferencia/Preferencia
        public ActionResult Index()
        {

            return View();
        }

        public async Task<ActionResult> VistaPref(int IdUser)
        {
            var mLista = await PrefBL.ListaPref(IdUser, Util.Variables.PREFRENCIA);

            return PartialView("_vistaPref", mLista);
        }

        public async Task<ActionResult> LoadPref(int IdUser)
        {
            var mLista = await PrefBL.ListaPref(IdUser,Util.Variables.PREFRENCIA);

            return PartialView("_loadPref", mLista);
        }

        public async Task<ActionResult> LoadAlergia(int IdUser)
        {
            var mLista = await PrefBL.ListaPref(IdUser, Util.Variables.ALERGIA);

            return PartialView("_loadAlergia", mLista);
        }

        public async Task<ActionResult> LoadProducto(int IdUser)
        {
            var listaPref = await PrefBL.ListaPref(IdUser, 0);
            var mLista = await ProductoBL.ListaProducto();
            if (listaPref != null && mLista !=null) {

                foreach (var item in mLista) {
                    if (listaPref.Exists(x => x.IdProd == item.Id))
                    {
                        item.Estado = 2;
                    }
                }
            }

            return PartialView("_loadProducto", mLista);
        }


        public async Task<JsonResult> AddProdPref(PrefModel objModel)
        {
            var resultado = await PrefBL.Agregar(objModel);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> EliminarPref(int IdPref)
        {
            var resultado = await PrefBL.Eliminar(IdPref);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        

    }
}