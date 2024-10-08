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

        public List<InfoModel> listaInfo = new List<InfoModel>();

        public InfoSing() {

            listaInfo.Add(new InfoModel(1, "PLATANO", 89, 22.8M, 2.6M, 12.2M, 8.7M, 1));
            listaInfo.Add(new InfoModel(2, "MANZANA", 52, 13.8M, 2.4M, 10.4M, 4.6M, 1));
            listaInfo.Add(new InfoModel(3, "MANDARINA", 89, 22.8M, 2.6M, 12.2M, 8.7M, 1));
            listaInfo.Add(new InfoModel(4, "ZANAHORIA", 89, 22.8M, 2.6M, 12.2M, 8.7M, 1));
        }

        public static InfoSing Instance
        {
            get
            {
                if (instance == null)
                    instance = new InfoSing();

                return instance;
            }
        }

    }
}
