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
            listaInfo.Add(new InfoModel(46, "PLATANO", "89 Kcal", "1.1 g", "0.0 mg", "2.6 g", "12.2 g", "1 mg"));
            listaInfo.Add(new InfoModel(47, "MANZANA", "52 Kcal", "0.3 g", "0.0 mg", "2.4 g", "10.4 g", "1 mg"));
            listaInfo.Add(new InfoModel(49, "NARANJA", "47 Kcal", "0.9 g", "0.0 mg", "2.4 g", "9.0 g", "0 mg"));
            listaInfo.Add(new InfoModel(51, "ZANAHORIA", "41 Kcal", "0.9 g", "0.0 mg", "2.8 g", "4.7 g", "69 mg"));
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
