using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Modelo
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int IdSexo { get; set; }
        public DateTime FechaReg { get; set; }
        public int Estado { get; set; }

        public UsuarioModel() {
            Nombres = "NOmbre Usuario";
            Dni = "12345678";
            Correo = "correo@corereo";
            Clave = "1234";
            IdSexo = 2;
            FechaReg = DateTime.Now;
            Estado = 1;
        }
    }
}
