using System.Web.Mvc;

namespace ydRSoft.Web.Areas.AConfiguracion
{
    public class AConfiguracionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AConfiguracion";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AConfiguracion_default",
                "AConfiguracion/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}