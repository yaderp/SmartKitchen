using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class ProductoModel
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
        public int PosX { get; set; }
        public int PosY { get; set; }
        public decimal Radio {  get; set; }

        public int Estado { get; set; }
        public InfoModel Info { get; set; }

        public ProductoModel() {
            Id = 1;
            Nombre = "PLATANO";
            PosX = 300;
            PosY = 300;
            Radio = 180;
            Info = new InfoModel();
        }

        public ProductoModel(int Id,string Nombre, int PosX,int PosY, decimal Radio) {
        
            this.Id = Id;
            this.Nombre = Nombre;
            this.PosX = PosX;
            this.PosY = PosY;
            this.Radio = Radio;
            Info = new InfoModel();
        }
    }
}
