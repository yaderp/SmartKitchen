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
        public string Calorias { get; set; }
        public string Proteinas { get; set; }
        public string Colesterol { get; set; }
        public string Carbohidratos { get; set; }
        public string Fibra { get; set; }        
        public string Azucares { get; set; }        
        public string Sodio { get; set; }
        public string Calcio { get; set; }
        public string Grasa { get; set; }
        public InfoModel() {
            Id = 1;
            Nombre = "PLATANO";
            Calorias = "89.0 Kcal";
            Proteinas= "1.1 g";
            Colesterol = "0.0 mg";
            Fibra = "2.6 g";
            Azucares = "12.2 g";
            Sodio = "1.0 mg";
        }

        public InfoModel(int id, string Nombre, string calorias, string proteinas, string colesterol, string fibra, string azucares, string sodio)
        {
            Id = id;
            this.Nombre = Nombre;
            Calorias = calorias;
            Proteinas = proteinas;
            Colesterol = colesterol;
            Fibra = fibra;
            Azucares = azucares;
            Sodio = sodio;
            Carbohidratos = "2.7 gr";
        }
    }
}
