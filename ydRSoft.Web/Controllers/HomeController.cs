﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ydRSoft.BL;
using ydRSoft.Modelo;

namespace ydRSoft.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            //var resultado = await UsuarioBL.SetUsuario(new UsuarioModel());
            //var lista = await UsuarioBL.getALl();

            //var prod = await ProductoBL.ConsultaProdAPI("PAPA");
            //if (prod != null) {
            //    prod.Nombre = prod.Nombre == null ? "" : prod.Nombre.ToUpper();
            //    var resultado = await ProductoBL.Guardar(prod);
            //}
            

            Session["objUser"] = await UsuarioBL.GetUsuarioId(2);
            return RedirectToAction("Index", "Configuracion", new { Area = "AConfiguracion" });

            //UsuarioModel model = (UsuarioModel)Session["objUser"];
            //if (model == null)
            //{
            //    return RedirectToAction("Login");
            //}

            //return View(model);
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult VerBotones()
        {            
            return PartialView("_verBotones");
        }

        public ActionResult VerComandos()
        {
            return PartialView("_verComandos");
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

// Clase para representar un Item
public class Item
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }
}