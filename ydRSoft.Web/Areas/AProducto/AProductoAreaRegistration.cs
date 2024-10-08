using System.Web.Mvc;

namespace ydRSoft.Web.Areas.AProducto
{
    public class AProductoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AProducto";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AProducto_default",
                "AProducto/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}