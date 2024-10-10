using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Controllers
{
    public class HomeController : Controller
    {

        private static readonly HttpClient client = new HttpClient();


        public async Task<ActionResult> Index()
        {

            //var resultado = await UsuarioBL.SetUsuario(new UsuarioModel());
            //var lista = await UsuarioBL.getALl();

            var receta = await RecetaBL.SetReceta(RecetaBL.GetReceta(2));

            return View();
        }

        private async Task ConsultarAPI()
        {
            string apiUrl = "http://127.0.0.1:5000/items";
            List<Item> items = new List<Item>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<Item>>(jsonResponse);
                }
                else
                {
                    ViewBag.Error = $"Error al obtener los items: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ocurrió un error al intentar conectarse a la API: " + ex.Message;
            }

            //return View(items);
        }

        [HttpPost]
        public async Task<JsonResult> UploadImage(string image)
        {
            ProductoModel mProd = new ProductoModel();

            try
            {
                if (!string.IsNullOrEmpty(image))
                {
                    // Crear la carpeta UploadedImages si no existe
                    string path = Server.MapPath("~/UploadedImages/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // Eliminar el prefijo de la cadena base64
                    string base64Data = image.Replace("data:image/png;base64,", "");
                    byte[] imageBytes = Convert.FromBase64String(base64Data);

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(ms))
                        {
                            // Guardar la imagen en el servidor en formato JPEG
                            string fileName = "capturedImage_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                            string filePath = Path.Combine(path, fileName);

                            // Guardar como JPEG con calidad estándar
                            await Task.Run(() =>
                            {
                                img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                            });
                        }
                    }
                }

               
            }
            catch (Exception ex)
            {
                
            }

            return Json(mProd, JsonRequestBehavior.AllowGet);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

// Clase para representar un Item
public class Item
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
}