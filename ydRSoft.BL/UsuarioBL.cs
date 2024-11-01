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

        public static async Task<RpstaModel> Agregar(UsuarioModel objModel)
        {
            if (objModel != null)
            {
                var muser = await UsuarioBD.GetUsuarioNom(objModel.Nombres);
                if (muser != null)
                {
                    return new RpstaModel(true, "Nombre ya Registrado");
                }
            }

            var resultado = await UsuarioBD.Guardar(objModel);

            return resultado;
        }

        public static async Task<UsuarioModel> getUsuarioNom(string Nombre)
        {
            var resultado = await UsuarioBD.GetUsuarioNom(Nombre);
            return resultado;
        }

        public static async Task<UsuarioModel> GetUsuarioId(int Id)
        {
            var resultado = await UsuarioBD.GetUsuarioId(Id);

            return resultado;
        }

        public static async Task<UsuarioModel> GetUsuario(string Nombre, string Clave)
        {
            var resultado = await UsuarioBD.GetUsuario(Nombre, Clave);

            return resultado;
        }

        public static async Task<List<UsuarioModel>> getAll(int Estado)
        {
            var resultado = await UsuarioBD.GetAll(Estado);

            return resultado;
        }

        public static async Task<RpstaModel> Editar(UsuarioModel model)
        {
            var resultado = await UsuarioBD.EditarUsuario(model);
            return resultado;
        }
    }
}
