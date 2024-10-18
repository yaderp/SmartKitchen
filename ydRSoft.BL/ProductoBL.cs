using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class ProductoBL
    {
        public static async Task<List<ProductoModel>> ConsultarApi(string image64)
        {
            List<ProductoModel> mLista = new List<ProductoModel>();
            try
            {
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

                                mLista = JsonConvert.DeserializeObject<List<ProductoModel>>(responseBody);                                
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
            catch (Exception ex)
            {
                await Util.LogError.SaveLog("->" + ex.Message);
            }


            return mLista;
        }

        //guarda un producto con su informacion nutricional
        public static async Task<RpstaModel> Guardar(InfoModel objModel)
        {
            var resultado = await ProductoBD.Guardar(objModel);

            return resultado;
        }

        //consulta la informacion nutricional de un producto
        public static async Task<InfoModel> ConsultaProdAPI(string Nombre)
        {
            InfoModel objModel = new InfoModel();

            string txtPregunta = "Genera  la informacion nutricional de " + Nombre +
               ". Proporciónalas en formato JSON que incluya un " +
               "Id, Nombre, Calorias, Proteinas, Colesterol, Fibra, Carbohidratos,  " +
               "Azucares, Sodio, Calcio, Grasa. "+
               "Devuelve solo el JSON. que no haya caracteres no deseados al principio o al final."+
               "que la informacion tenga sus respectivas unidades ejemplo 2.4 Kcal";

            var resultado = await ApiOpenAI.PreguntaApi(txtPregunta);
            string jsonResponse = ApiOpenAI.MensajeContent(resultado);

            try
            {
                objModel = JsonConvert.DeserializeObject<InfoModel>(jsonResponse);

            }
            catch
            {

            }

            return objModel;
        }

        public static async Task<List<InfoModel>> ListaProducto()
        {
            var resultado = await ProductoBD.ListaProd();

            return resultado;
        }

        private static List<ProductoModel> ValidaPosXY(List<ProductoModel> listaModel, List<ProductoModel> mLista)
        {
            if (mLista != null)
            {
                try
                {
                    for (int i = 0; i < listaModel.Count; i++)
                    {
                        listaModel[i].PosX = GetPosX(listaModel[i].PosX);
                        listaModel[i].PosY = GetPosY(listaModel[i].PosY);
                        listaModel[i].Radio = listaModel[i].Radio + 90;

                        var model = mLista.Where(x => x.Nombre == listaModel[i].Nombre).FirstOrDefault();
                        if (model != null)
                        {

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

            return listaModel;
        }

        private static int GetPosX(int PosX)
        {
            var difx = PosX + (PosX - 555) * 70 / 148;
            return difx;
        }

        private static int GetPosY(int PosY)
        {
            var dify = PosY + (PosY + 230) * 60 / 152;
            return dify;
        }
    }
}
