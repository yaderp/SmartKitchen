using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Areas.AUsuario.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: AUsuario/Usuario
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> InicioUsuario(string Nombre,string Clave, int Opcion)
        {
            Session["objUser"] = null;
            RpstaModel resultado = new RpstaModel();
            if (Opcion == 1)
            {
                var logi = await UsuarioBL.GetUsuario(Nombre,Clave);
                if (logi != null) {
                    resultado.Error = false;
                    Session["objUser"] = logi;
                }
                else
                {
                    resultado.Mensaje = "Nombre. i. o. clave. Incorrectos.";
                }
            }
            else
            {
                if (Opcion == 2)
                {
                    UsuarioModel usuarioModel = new UsuarioModel();
                    usuarioModel.Nombres = Nombre;
                    usuarioModel.Clave = Clave;

                    resultado = await UsuarioBL.SetUsuario(usuarioModel);

                    //registars
                }
                else
                {
                    Session["objUser"] = new UsuarioModel();
                    resultado.Error = false;
                }
            }

            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> CerrarSesion()
        {
            RpstaModel rpstaModel = new RpstaModel();
            try {
                Session["objUser"] = null;
                rpstaModel.Error = false;
            }
            catch (Exception ex) {
                rpstaModel.Error = true;
                await Util.LogError.SaveLog("Cerrar Sesion " + ex.Message);
            }

            return Json(rpstaModel, JsonRequestBehavior.AllowGet);
        }

    }
}