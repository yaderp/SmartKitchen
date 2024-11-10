using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class InfoSing
    {
        private static InfoSing instance = null;
        private static readonly object padlock = new object();
        private List<ProductoModel> listaInfo;

        private InfoSing()
        {
            listaInfo = new List<ProductoModel>();
        }

        public static InfoSing Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InfoSing();
                    }
                    return instance;
                }
            }
        }

        public List<ProductoModel> ListaProductos()
        {
            return listaInfo;
        }

        public ProductoModel GetProdNombre(string Nombre)
        {
            return listaInfo.FirstOrDefault(x => x.Nombre == Nombre);
        }

        public ProductoModel GetProdId(int IdProd)
        {
            var producto = listaInfo.FirstOrDefault(x => x.Id == IdProd);
            if (producto != null)
                return producto;
            return new ProductoModel();
        }

        public async Task LoadProductos()
        {
            listaInfo = await ProductoBL.ListaProducto();
        }
    }

}
