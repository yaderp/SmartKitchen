using System;
using System.Collections.Generic;
using System.IO;
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
                var logi = await UsuarioBL.GetUsuario(Nombre, Clave);
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

                    resultado = await UsuarioBL.Agregar(usuarioModel);
                    if (!resultado.Error)
                    {
                        Session["objUser"] = usuarioModel;
                    }
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


        public async Task<ActionResult> LoadUsuario(int IdUser)
        {
            var objModel = await UsuarioBL.GetUsuarioId(IdUser);

            return PartialView("_loadUsuario", objModel);
        }

        public async Task<ActionResult> ListaUsuario()
        {
            var mLista = await UsuarioBL.getAll(Util.Variables.PUBLICO);

            return PartialView("_listaUsuario", mLista);
        }


        

        public async Task<JsonResult> EditarUsuario(UsuarioModel objModel)
        {
            RpstaModel rpstaModel = new RpstaModel();
            try
            {
                rpstaModel = await UsuarioBL.Editar(objModel);
                if (!rpstaModel.Error)
                {
                    Session["objUser"] = objModel;
                }
            }
            catch (Exception ex)
            {
                rpstaModel.Error = true;
                await Util.LogError.SaveLog("editar usuario " + ex.Message);
            }

            return Json(rpstaModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase imageFile,int IdUser)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                // Ruta donde se guardará la imagen
                string fileName = Path.GetFileName(imageFile.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Imagenes/Avatar"), IdUser + ".jpg");

                // Guardar la imagen en la ruta especificada
                imageFile.SaveAs(path);

                // Retornar la URL de la imagen como respuesta JSON
                return Json(new { success = true, imageUrl = Url.Content("~/Content/Imagenes/Avatar/" + IdUser + ".jpg") });
            }
            else
            {
                return Json(new { success = false });
            }
        }


    }
}