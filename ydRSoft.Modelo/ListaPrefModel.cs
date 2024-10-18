using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class ListaPrefModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<TipoModel> ListaPref { get; set; }
        public List<TipoModel> ListaAlergia { get; set; }

        public ListaPrefModel(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            ListaPref = new List<TipoModel>();
            ListaAlergia = new List<TipoModel>();
        }

        public ListaPrefModel() {         
            Id = 0;
            Nombre = string.Empty;
            ListaPref = new List<TipoModel>();
            ListaAlergia = new List<TipoModel>();
        }
    }
}
