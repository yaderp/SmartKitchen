using System.Web.Mvc;

namespace ydRSoft.Web.Areas.ASugerencia
{
    public class ASugerenciaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ASugerencia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ASugerencia_default",
                "ASugerencia/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}