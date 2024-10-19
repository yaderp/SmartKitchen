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
                mLista.Add(new TipoModel(1, "", "COMANDOS", "Mostrara la lista de Comandos"));
                mLista.Add(new TipoModel(2, "", "INICIAR", "Para Iniciar la deteccion de objetos"));                
                mLista.Add(new TipoModel(3, "", "RECETA", "Para Mostrar las Recetas recomendadas"));                
                mLista.Add(new TipoModel(4, "", "MENU", "Mostrara el menu de objetos detectados"));
                mLista.Add(new TipoModel(5, "", "MOSTRAR FAVORITOS", "Mostrara Recetas Favoritas"));
                mLista.Add(new TipoModel(6, "", "MOSTRAR CATEGORIAS", "Mostrara las Categorias"));  
                mLista.Add(new TipoModel(7, "", "BOTONES", "Para ver la lista de botones"));
                mLista.Add(new TipoModel(8, "", "BUSCAR", "Para Buscar Recetas"));
                mLista.Add(new TipoModel(9, "", "CONFIGURACION", "Para ver la Pantalla de Configuracion"));
                mLista.Add(new TipoModel(10, "", "REINICIAR", "Para refrescar la panatalla"));
                mLista.Add(new TipoModel(11, "", "SALIR", "Para cerrar sesion"));
                mLista.Add(new TipoModel(12, "", "SIGUIENTE", "Para Mostrar la siguiente receta"));
                mLista.Add(new TipoModel(13, "", "ATRAS", "Para Mostrar la anterior receta"));

            }
            catch { 
            
            }

            return mLista;
        }
    }


}
