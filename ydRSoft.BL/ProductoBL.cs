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
                    }
                }
                catch (Exception ex)
                {
                    await Util.LogError.SaveLog("->" + ex.Message);
                }
            }


            return mLista;
        }

        public static async Task<List<ProductoModel>> ProdDetectados(string image64, List<ProductoModel> listaSession)
        {
            var listaModel = new List<ProductoModel>();
            try {
                var mLista = await ConsultarApi(image64);

                mLista = ValidaPosXY(mLista, listaSession);

                foreach (var item in mLista)
                {
                    var temp = InfoSing.Instance.GetProdNombre(item.Nombre);
                    if(temp != null)
                    {
                        temp.PosX = item.PosX;
                        temp.PosY = item.PosY;
                        temp.Radio = item.Radio;

                        listaModel.Add(temp);
                    }
                   
                }
            } catch(Exception ex) {
                await Util.LogError.SaveLog("Prod Detectados "+ex.Message);
            }
            return listaModel;
        }

        //guarda un producto con su informacion nutricional
        public static async Task<RpstaModel> Guardar(ProductoModel objModel)
        {
            RpstaModel rpstaModel = new RpstaModel(true,"Ya registrado");

            if (string.IsNullOrEmpty(objModel.Nombre)) return new RpstaModel(true,"Nombre Producto No valido");

            objModel.Nombre = objModel.Nombre.ToUpper();
            var duplicado = await ProductoBD.IsDuplicado(objModel.Nombre);

            if (!duplicado)
            {

                rpstaModel = await ProductoBD.Guardar(objModel);

                if (rpstaModel != null && !rpstaModel.Error)
                {
                    await InfoSing.Instance.LoadProductos();
                }
            }

            return rpstaModel;
        }

        public static async Task<ProductoModel> GetInformacionN(string Nombre)
        {
            

            var modelo = await ProductoBD.GetProducto(Nombre);
            if(modelo == null)
            {
                modelo = await ConsultaProdAPI(Nombre);
            }

            return modelo;

        }


        //consulta la informacion nutricional de un producto
        public static async Task<ProductoModel> ConsultaProdAPI(string Nombre)
        {
            ProductoModel objModel = null;

            //string txtPregunta = "Genera  la informacion nutricional de " + Nombre +
            //   ". Proporciónalas en formato JSON que incluya un " +
            //   "Id, Nombre, Calorias, Proteinas, Colesterol, Carbohidratos, Fibra,  " +
            //   "Azucares, Sodio, Calcio, Grasa. "+
            //   "Devuelve solo el JSON. que no haya caracteres no deseados al principio o al final."+
            //   "que la informacion tenga sus respectivas unidades ejemplo 2.4 Kcal";

            var txtPregunta = "Proporciona la información nutricional de una " + Nombre +
                " con las unidades adecuadas. por ejemplo 2.5 mg 2.4 Kcal 2.4 g " +
                "Las propiedades deben incluir: Id, Nombre, " +
                "Calorias, Proteinas , Colesterol , Carbohidratos , Fibra ," +
                " Azucares , Sodio , Calcio , Grasa ."+
                "Solo responde con el JSON, sin texto adicional. que inicie y termine en {} segurando el json";

            var resultado = await ApiOpenAI.PreguntaApi(txtPregunta);
            string jsonResponse = ApiOpenAI.MensajeContent(resultado);

            try
            {
                objModel  = JsonConvert.DeserializeObject<ProductoModel>(jsonResponse);

            }
            catch
            {

            }

            return objModel;
        }

        public static async Task<List<ProductoModel>> ListaProducto()
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
        //580
        //<- 780 | 580 | 280 ->
        private static int GetPosX(int PosX)
        {
            var difx = PosX + (PosX - 580) * 70 / 148;
            return difx;
        }
        // baja 400 | 160 | 0 sube
        private static int GetPosY(int PosY)
        {
            var dify = PosY + (PosY + 160) * 60 / 152;
            return dify;
        }
    }
}
