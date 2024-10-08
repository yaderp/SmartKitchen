using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class RecetaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NivelDificultad { get; set; }
        public int Tiempo { get; set; }
        public List<string> Ingredientes { get; set; }
        public List<string> PasosPreparacion { get; set; }
        

        public RecetaModel() {
            Id = 0;
            Ingredientes = new List<string>();
        }
    }
}
