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
        private static async Task<List<ProductoModel>> ConsultarApi(string image64)
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
            var mLista = new List<ProductoModel>();
            try {
                var resultado  = await ConsultarApi(image64);

                resultado = CargarProducto(resultado, listaSession);
                mLista = resultado;
            } catch(Exception ex) {
                await Util.LogError.SaveLog("Prod Detectados "+ex.Message);
            }
            return mLista;
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
            else
            {
                var prod = await ProductoBD.GetProducto(objModel.Nombre);
                if (prod != null && prod.Id > 0 && prod.Estado == 0)
                {
                    rpstaModel = await ProductoBD.ActProducto(prod.Id, 1);

                }
            }

            return rpstaModel;
        }


        public static async Task<RpstaModel> ActProducto(int IdProd,int Estado)
        {
            var resultado = await ProductoBD.ActProducto(IdProd, Estado);

            return resultado;
        }
        public static async Task<RpstaModel> EditarNombre(int IdProd, string Nombre)
        {
            var resultado = await ProductoBD.EditarNombre(IdProd, Nombre);

            return resultado;
        }

        public static async Task<RpstaModel> EditarId(int IdProd, int newId)
        {
            var resultado = await ProductoBD.EditarId(IdProd, newId);

            return resultado;
        }

        public static async Task<RpstaModel> EditarProducto(ProductoModel objModel)
        {
            var resultado = await ProductoBD.EditarProducto(objModel);

            return resultado;
        }

        public static async Task<ProductoModel> GetProductoId(int IdProd)
        {
            var resultado = await ProductoBD.GetProducto(IdProd);
            
            return resultado;

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

        private static List<ProductoModel> FiltrarLista(List<ProductoModel> listaModel)
        {
            var mLista = new List<ProductoModel>();

            listaModel.RemoveAll(x => x.Radio > 160);

            foreach(var item in listaModel)
            {
                var duplicados = listaModel.Where(p => p.Id != item.Id &&
                                            Math.Abs(p.PosX - item.PosX) < 20 &&
                                            Math.Abs(p.PosY - item.PosY) < 20).ToList();


                if (duplicados.Any())
                {
                    var productoConMenorRadio = duplicados
                        .Concat(new[] { item }) // Incluye el producto actual en la comparación
                        .OrderBy(p => p.Radio)
                        .First();

                    // Agrega solo si no ha sido agregado ya (para evitar duplicados en la lista final)
                    if (!mLista.Any(p => p.Id == productoConMenorRadio.Id))
                    {
                        mLista.Add(productoConMenorRadio);
                    }
                }
                else
                {
                    // Si no hay duplicados, agregar el producto actual
                    mLista.Add(item);
                }
            }

            return mLista;

        }

        public static  List<ProductoModel> CargarProducto(List<ProductoModel> listaModel, List<ProductoModel> listaSession)
        {
            List<ProductoModel> mLista = new List<ProductoModel>();
            if (listaModel != null)
            {
                listaModel = FiltrarLista(listaModel);

                try
                {
                    foreach (var item in listaModel)
                    {
                        ProductoModel mprod7 = InfoSing.Instance.GetProdId(item.Id).Clone();
                        mprod7.PosX = GetPosX(item.PosX);
                        mprod7.PosY = GetPosY(item.PosY);
                        mprod7.Radio = item.Radio + 70;

                        if (listaSession != null && listaSession.Count > 0)
                        {
                            var temp = listaSession.Where(x => x.Id == item.Id).FirstOrDefault();

                            if (temp != null)
                            {
                                int dy = Math.Abs(mprod7.PosY - temp.PosY);
                                int dx = Math.Abs(mprod7.PosX - temp.PosX);

                                if (dx < 25 && dy < 25)
                                {
                                    mprod7 = temp;                                    
                                }
                            }
                        }

                        mLista.Add(mprod7);
                    }
                }
                catch{}
            }           

            return mLista;
        }
        //580
        //<- 780 | 580 | 280 ->
        private static int GetPosX(int PosX)
        {
            var difx = PosX + (PosX - 750) * 50 / 152;
            return difx;
        }
        // baja 400 | 160 | 0 sube
        private static int GetPosY(int PosY)
        {
            var dify = PosY + (PosY + 200) * 45 / 152;
            return dify;
        }


    }
}

/*
 
 posX = posX + (posX - 750) * 50 / 152;
        posY = posY + (posY + 200) * 45 / 152;
        radio = radio + 70;
 * 
 * */
