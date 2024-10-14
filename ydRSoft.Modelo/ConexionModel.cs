using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class ConexionModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdSexo { get; set; }
        public List<TipoModel> ListaCom { get; set; }
        public ConexionModel()
        {
            Id = 0;
            Nombre = "YADER";
            ListaCom = new List<TipoModel>();
        }
        public ConexionModel(int Id, string Nombre) {
            this.Id = Id;
            this.Nombre = Nombre;
            ListaCom = new List<TipoModel>();
        }
    }
}
