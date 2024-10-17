using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BD
{
    public class ProductoBD
    {
        public static async Task<RpstaModel> Guardar(ProductoModel objModel) {
           RpstaModel respuesta = new RpstaModel();
            try { 
            

            }catch (Exception ex) {
                await Util.LogError.SaveLog("Guardar Producto "+ex.Message);
            }

            return respuesta;
        }
    }
}
