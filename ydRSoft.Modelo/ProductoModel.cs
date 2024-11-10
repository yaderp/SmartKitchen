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
            Id = -1;
            Nombre = "";
            PosX = 0;
            PosY = 0;
            Radio = 0;
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

        public ProductoModel Clone()
        {
            return new ProductoModel
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Calorias = this.Calorias,
                Proteinas = this.Proteinas,
                Colesterol = this.Colesterol,
                Carbohidratos = this.Carbohidratos,
                Fibra = this.Fibra,
                Azucares = this.Azucares,
                Sodio = this.Sodio,
                Calcio = this.Calcio,
                Grasa = this.Grasa,
                PosX = this.PosX,
                PosY = this.PosY,
                Radio = this.Radio,
                Estado = this.Estado
            };
        }
    }
}
