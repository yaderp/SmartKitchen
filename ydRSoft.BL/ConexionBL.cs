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
                mLista.Add(new TipoModel(1, "", "COMANDOS", "Mostrará la lista de Comandos"));
                mLista.Add(new TipoModel(2, "", "INICIAR", "Para Iniciar la detección de objetos"));                
                mLista.Add(new TipoModel(3, "", "RECETA", "Para Mostrar las Recetas recomendadas"));                
                mLista.Add(new TipoModel(4, "", "MENÚ", "Mostrará el menú de objetos detectados"));
                mLista.Add(new TipoModel(5, "", "MOSTRAR FAVORITOS", "Mostrará Recetas Favoritas"));
                mLista.Add(new TipoModel(6, "", "MOSTRAR CATEGORÍAS", "Mostrará las Categorias"));  
                mLista.Add(new TipoModel(7, "", "BOTONES", "Para ver o acultar la lista de botones"));
                //mLista.Add(new TipoModel(8, "", "BUSCAR", "Para Buscar Recetas"));
                mLista.Add(new TipoModel(8, "", "AGREGAR FAVORITO", "Para Agregar la receta a Favoritos"));
                //mLista.Add(new TipoModel(9, "", "REINICIAR", "Para refrescar la panatalla"));
                mLista.Add(new TipoModel(9, "", "SALIR", "Para cerrar sesión"));
                //mLista.Add(new TipoModel(12, "", "SIGUIENTE", "Para Mostrar la siguiente receta"));
                //mLista.Add(new TipoModel(13, "", "ATRAS", "Para Mostrar la anterior receta"));

            }
            catch { 
            
            }

            return mLista;
        }
    }


}
