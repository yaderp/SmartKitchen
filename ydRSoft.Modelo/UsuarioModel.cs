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
        public string Region { get; set; }
        public int IdSexo { get; set; }
        public int IdCargo { get; set; }
        public DateTime FechaReg { get; set; }
        public int Estado { get; set; }

        public UsuarioModel() {
            Id = 1;
            Nombres = "INVITADO";
            Dni = "12345678";
            Correo = "correo@corereo";
            Clave = "123";
            IdSexo = 1;
            IdCargo = 0;
            FechaReg = DateTime.Now;
            Estado = 1;
        }
    }
}
