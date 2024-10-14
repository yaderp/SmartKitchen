using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class InfoBL
    {

        public static List<ProductoModel> CargarInfo(List<ProductoModel> mLista)
        {
            try {

                for(int i = 0; i < mLista.Count; i++)
                {
                    mLista[i].Info = InfoBD.GetInfo(mLista[i].Nombre);
                }
            
            } catch{ 
            
            }

            return mLista;
        }
    }
}
