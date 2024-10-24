using System.Web.Mvc;

namespace ydRSoft.Web.Areas.AFavoritos
{
    public class AFavoritosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AFavoritos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AFavoritos_default",
                "AFavoritos/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}