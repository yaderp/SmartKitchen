using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;

namespace ydRSoft.Web.Areas.APreferencia.Controllers
{
    public class PreferenciaController : Controller
    {
        // GET: APreferencia/Preferencia
        public ActionResult Index()
        {

            return View();
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

        public async Task<ActionResult> LoadProducto()
        {
            var mLista = await ProductoBL.ListaProducto();

            return PartialView("_loadAlergia", mLista);
        }

    }
}