﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ydRSoft.BD;
using ydRSoft.Modelo;

namespace ydRSoft.BL
{
    public class SugerenciaBL
    {

        public static async Task LoadSugerencia(int Iduser)
        {
            var recetaId = (await SugerenciaBD.GetRecetaId()).Id;

            if (recetaId == 0)
            {
                var recetas = await RecetaBD.ListaSugerida(Iduser);
                if (!string.IsNullOrEmpty(recetas))
                {
                    string txtPregunta = "basado en estas recetas" + recetas +
                                    " deseo una nueva receta que combine elementos de las anteriores " +
                                    "o proponga una alternativa interesante de acuerdo a la region de las recetas proporcionadas" +
                                    " y envie la informacion separados por | con los datos Nombre | Region | categoria " +
                                    "Enviar unicamente esos 3 datos en el formato solicitado.";

                    var resultado = await ApiOpenAI.PreguntaApi(txtPregunta);
                    string respuesta = ApiOpenAI.MensajeContent(resultado);

                    if (!string.IsNullOrEmpty(respuesta))
                    {
                        var listar = respuesta.Split('|');
                        string nombre = listar[0];

                        var receta = await RecetaBL.ConsultarNewReceta(nombre, "", 0);

                        if (receta.Id > 0)
                        {
                            var resultSug = await SugerenciaBD.Guardar(new SugerModel(receta.Nombre, receta.Id));
                        }
                    }
                }
            }
        }

        public static async Task<RecetaModel> ConsultarSuger(int IdUser)
        {
            RecetaModel receta = new RecetaModel();

            var sugr7a = await SugerenciaBD.GetRecetaId();
            var recetaId = sugr7a.IdReceta;

            if (recetaId > 0)
            {
                receta = await RecetaBL.GetRecetaId(recetaId);
                receta.IdFav = sugr7a.Id;
            }
            else
            {
                var recetas = await RecetaBD.ListaSugerida(IdUser);
                if (!string.IsNullOrEmpty(recetas))
                {
                    string txtPregunta = "basado en estas recetas" + recetas +
                                    " deseo una nueva receta que combine elementos de las anteriores " +
                                    "o proponga una alternativa interesante de acuerdo a la region de las recetas proporcionadas" +
                                    " y envie la informacion separados por | con los datos Nombre | Region | categoria " +
                                    "Enviar unicamente esos 3 datos en el formato solicitado.";

                    var resultado = await ApiOpenAI.PreguntaApi(txtPregunta);
                    string respuesta = ApiOpenAI.MensajeContent(resultado);

                    if (!string.IsNullOrEmpty(respuesta))
                    {
                        var listar = respuesta.Split('|');
                        string nombre = listar[0];

                        receta = await RecetaBL.ConsultarNewReceta(nombre, "", 0);

                        if (receta.Id > 0)
                        {
                            var resultSug = await SugerenciaBD.Guardar(new SugerModel(receta.Nombre, receta.Id));
                        }
                    }
                }
            }

            return receta;
        }


        public static async Task<RpstaModel> ActEstado(int IdSug, int Estado)
        {
            var resultado = await SugerenciaBD.ActEstado(IdSug, Estado);

            return resultado;
        }

        public static async Task<List<RecetaModel>> ListaSugerencia(string Nombre)
        {
            List<RecetaModel> mLista;

            if (string.IsNullOrEmpty(Nombre))
            {
                mLista = await SugerenciaBD.ListaSug();
            }
            else
            {
                mLista = await SugerenciaBD.ListaSug(Nombre);
            }

            return mLista;
        }
    }
}
