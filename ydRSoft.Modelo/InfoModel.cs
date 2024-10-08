using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class InfoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Calorias { get; set; }
        public decimal Proteinas { get; set; }
        public decimal Colesterol { get; set; }
        public decimal Fibra { get; set; }
        public decimal Azucares { get; set; }
        public decimal Sodio { get; set; }

        public InfoModel() {
            Id = 0;
            Nombre = "";
        }

        public InfoModel(int id, string Nombre, decimal calorias, decimal proteinas, decimal colesterol, decimal fibra, decimal azucares, decimal sodio)
        {
            Id = id;
            this.Nombre = Nombre;
            Calorias = calorias;
            Proteinas = proteinas;
            Colesterol = colesterol;
            Fibra = fibra;
            Azucares = azucares;
            Sodio = sodio;
        }
    }
}
