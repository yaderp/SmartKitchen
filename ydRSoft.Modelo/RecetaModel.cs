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
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public int NivelDificultad { get; set; }
        public int Tiempo { get; set; }
        public string Categoria { get; set; }
        public int Calificacion { get; set; }
        public string StrIngre { get; set; }
        public string StrPas { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Estado { get; set; }
        public List<string> Ingredientes { get; set; }
        public List<string> PasosPreparacion { get; set; }        
        public List<int> ListaId { get; set; }
        public bool Isfavorito { get; set; }
        public int IdFav { get; set; }
        public RecetaModel() {
            Id = 0;
            Nombre = "nombre01";
            Categoria = "categoria";
            StrIngre = "ingredientes";
            StrPas = "pasos";
            Ingredientes = new List<string>();
            PasosPreparacion = new List<string>();
            ListaId = new List<int>();
            Isfavorito = false;
        }

        public RecetaModel(List<int> ListaId)
        {            
            this.ListaId = ListaId;

            if (ListaId != null && ListaId.Count > 0)
            {
                Id = ListaId[0];
            }
            else
            {
                Id = 0;
            }
        }
    }
}
