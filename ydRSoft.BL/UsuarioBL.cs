using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class UsuarioBL
    {

        public static async Task<string> SetUsuario(UsuarioModel objModel)
        {
            var resultado = await UsuarioBD.GuardarUsuario(objModel);

            return resultado;
        }

        public static async Task<List<UsuarioModel>> getALl()
        {
            var resultado = await UsuarioBD.LeerUsuariosAsync();

            var aux = await UsuarioBD.LeerUsuarios();

            return resultado;
        }
    }
}
