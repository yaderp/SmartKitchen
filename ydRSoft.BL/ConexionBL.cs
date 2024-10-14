using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class ConexionBL
    {

        public static ConexionModel GetConexion()
        {
            ConexionModel model = new ConexionModel();
            model.ListaCom = ListaCom();

            return model;
        }


        public static List<TipoModel> ListaCom()
        {
            var mLista = new List<TipoModel>();
            
            try {
                mLista.Add(new TipoModel(1, "<i class='btn-circle btn-success fa fa-camera'></i>", "INICIAR", "Para Iniciar la deteccion de objetos"));
                mLista.Add(new TipoModel(2, "<i class='btn-circle btn-primary fa fa-camera'></i>", "INICIAR", "Para detener la deteccion de objetos"));
                mLista.Add(new TipoModel(3, "<i class='btn-circle btn-primary fa fa-eye'></i>", "RECETA", "Para Mostrar las Recetas recomendadas"));
                mLista.Add(new TipoModel(4, "<i class='btn btn-circle btn-primary btn-outline'>2</i>", "SIGUIENTE", "Para Mostrar la siguiente receta"));
                mLista.Add(new TipoModel(5, "<i class='btn btn-circle btn-primary btn-outline'>1</i>", "ATRAS", "Para Mostrar la anterior receta"));
                mLista.Add(new TipoModel(6, "<i class='btn-circle btn-primary fa fa-bars'></i>", "MENU", "Mostrara el menu de objetos detectados"));
                mLista.Add(new TipoModel(7, "<i class='btn-circle btn-primary fa fa-star'></i>", "FAVORITOS", "Mostrara Recetas Favoritas"));
                mLista.Add(new TipoModel(8, "<i class='btn-circle btn-primary fa fa-microphone'></i>", "COMANDOS", "Mostrara la lista de Comandos"));
                mLista.Add(new TipoModel(9, "<i class='btn-circle btn-primary fa fa-gears'></i>", "CONFIGURACION", "Para ver la Pantalla de Configuracion"));
                mLista.Add(new TipoModel(9, "<i class='btn-circle btn-primary fa fa-window-close-o'></i>", "CERRAR", "Para ver la Pantalla de Configuracion"));

            }
            catch { 
            
            }

            return mLista;
        }
    }


}
