using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class InfoBD
    {
        public static InfoModel GetInfo(string Nombre)
        {
            InfoModel modelo = new InfoModel();

            if (string.IsNullOrEmpty(Nombre))
            {
                return modelo;
            }

            try {
                List<InfoModel> listaInfo = new List<InfoModel>();
                Nombre = Nombre.ToUpper();

                listaInfo.Add(new InfoModel(46, "PLATANO", 89, 1.1M, 0M, 2.6M, 12.2M, 1M));
                listaInfo.Add(new InfoModel(47, "MANZANA", 52, 0.3M, 0M, 2.4M, 10.4M, 1M));
                listaInfo.Add(new InfoModel(51, "ZANAHORIA", 41, 0.9M, 0M, 2.8M, 4.7M, 69M));
                listaInfo.Add(new InfoModel(49, "NARANJA", 43, 1.0M, 0M, 2.2M, 8.3M, 0M));

                
                if (listaInfo != null) {
                    modelo = listaInfo.Where(x => x.Nombre == Nombre).FirstOrDefault();
                    if (modelo == null) { 
                        modelo=new InfoModel();
                    }
                }

            } catch (Exception e) { 
            
            }

            return modelo;
        }
    }
}
