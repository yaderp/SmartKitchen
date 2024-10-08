using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class RecetaBL
    {

        public static RecetaModel GetReceta(int Pagina)
        {
            var receta = new RecetaModel();

            try {

                //List<RecetaModel> recetas = JsonConvert.DeserializeObject<List<RecetaModel>>(json);
                var lista = JsonConvert.DeserializeObject<List<RecetaModel>>(Util.Funciones.json);

                receta = lista.Where(x => x.Id == Pagina).FirstOrDefault();

                var ingred = receta.Ingredientes;
                for(int i=0;i<ingred.Count;i++) {
                    ingred[i] = Util.Funciones.LetraMayuscula(ingred[i]);
                }

                receta.Ingredientes = ingred;
            }
            catch(Exception) { 
            
            } 

            return receta;
        }

    }
}
