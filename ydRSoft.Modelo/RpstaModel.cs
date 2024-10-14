using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class RpstaModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Detalle { get; set; }
        public bool Error { get; set; }

        public string Mensaje { get; set; }

        public RpstaModel() {
            Error = true;
            Mensaje = "Intente en otro Momento";
        }

        public RpstaModel(bool Error,string Mensaje)
        {
            this.Error = Error;
            this.Mensaje = Mensaje;
        }
    }
}
