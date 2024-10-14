using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class TipoModel
    {

        public int Id { get; set; }
        public string Icono { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }

        public TipoModel(int Id,string Icono, string Nombre, string Detalle) {
            this.Id = Id;
            this.Icono = Icono;
            this.Nombre = Nombre;
            this.Detalle = Detalle;
        }
    }
}
