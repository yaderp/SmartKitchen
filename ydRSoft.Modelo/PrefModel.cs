using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class PrefModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdProd { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public PrefModel() {
        
        }
    }
}
