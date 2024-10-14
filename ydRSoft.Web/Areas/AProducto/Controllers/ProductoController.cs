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
        public JsonResult Prueba(string image64)
        {
            List<ProductoModel> mLista = new List<ProductoModel>();
            mLista.Add(new ProductoModel());
            mLista.Add(new ProductoModel(2,"MANZANA",600,400,200));
            
            return Json(mLista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VistaProductos()
        {

            return PartialView("vistaProductos");
        }


        [HttpPost]
        public async Task<JsonResult> ConsultarApi(string image64)
        {
            List<ProductoModel> mLista = new List<ProductoModel>();

            if (!string.IsNullOrEmpty(image64))
            {
                string url = "http://127.0.0.1:5000/items";

                try
                {
                    string base64Data = image64.Replace("data:image/png;base64,", "");
                    // Crear el JSON con la imagen en base64
                    var jsonContent = new
                    {
                        imagen = base64Data,
                        nombre = "yader"

                    };
                    string json = JsonConvert.SerializeObject(jsonContent);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PostAsync(url, data);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = response.Content.ReadAsStringAsync().Result;

                            mLista= JsonConvert.DeserializeObject<List<ProductoModel>>(responseBody);
                            mLista = ValidaPosXY(mLista);
                            mLista = InfoBL.CargarInfo(mLista);
                        }
                        else
                        {
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("->" + ex.Message);
                }
            }
            return Json(mLista, JsonRequestBehavior.AllowGet);
        }


        private List<ProductoModel> ValidaPosXY(List<ProductoModel> listaModel)
        {
            List<ProductoModel> mLista = (List<ProductoModel>)Session["ListaXY"];
            if (mLista != null) {
                try
                {
                    for (int i = 0; i < listaModel.Count; i++)
                    {
                        listaModel[i].PosX = GetPosX(listaModel[i].PosX);
                        listaModel[i].PosY = GetPosY(listaModel[i].PosY);
                        listaModel[i].Radio = listaModel[i].Radio + 90;

                        var model= mLista.Where(x => x.Nombre == listaModel[i].Nombre).FirstOrDefault();
                        if (model != null) {

                            int dy = Math.Abs(model.PosY - listaModel[i].PosY);
                            int dx = Math.Abs(model.PosX - listaModel[i].PosX);
                            var dif = model.Radio - listaModel[i].Radio;
                            int dr = Math.Abs((int)dif);

                            if (dx < 5 && dy < 5 && dr < 5) 
                            {
                                listaModel[i].PosX = mLista[i].PosX;
                                listaModel[i].PosY = mLista[i].PosY;
                                listaModel[i].Radio = mLista[i].Radio;
                            }
                        }
                    }
                }
                catch
                {
                    
                }
            }
            else
            {
                for (int i = 0; i < listaModel.Count; i++)
                {
                    listaModel[i].PosX = GetPosX(listaModel[i].PosX);
                    listaModel[i].PosY = GetPosY(listaModel[i].PosY);
                    listaModel[i].Radio = listaModel[i].Radio + 90;
                }
            }

            Session["ListaXY"] = listaModel;
            return listaModel;
        }


        private int GetPosX(int PosX)
        {
            var difx = PosX + (PosX - 555) * 70 / 148;
            return difx;
        }

        private int GetPosY(int PosY)
        {
            var dify = PosY + (PosY + 230) * 60/ 152;
            return dify;
        }
    }
}