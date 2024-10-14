using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.BL
{
    public class ProductoBL
    {

        public static async Task ConsultarApi(string base64Image)
        {
            string url = "http://127.0.0.1:5000/items";

            try
            {               
                // Crear el contenido JSON para la solicitud
                var jsonContent = new
                {
                    imagen = base64Image
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(jsonContent);
                StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                // Realizar la solicitud POST
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsync(url, data);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        
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
    }
}
