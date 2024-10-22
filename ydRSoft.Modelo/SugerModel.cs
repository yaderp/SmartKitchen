using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class SugerModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdReceta { get; set; }
        public int IdUser { get; set; }
        public DateTime Fechareg { get; set; }

        public SugerModel(string Nombre , int IdReceta) {
            this.Nombre = Nombre;
            this.IdReceta = IdReceta;
            IdUser = 1;
            Fechareg = DateTime.Now;
        }

        public SugerModel()
        {            
            Fechareg = DateTime.Now;
        }

    }
}
