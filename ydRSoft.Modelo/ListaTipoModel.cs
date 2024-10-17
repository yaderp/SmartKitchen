using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class ListaTipoModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<string> ListaPref { get; set; }
        public List<string> ListaAlergia { get; set; }

        public ListaTipoModel(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            ListaPref = new List<string>();
            ListaAlergia = new List<string>();
        }

        public ListaTipoModel() {         
            Id = 0;
            Nombre = string.Empty;
            ListaPref = new List<string>();
            ListaAlergia = new List<string>();
        }
    }
}
