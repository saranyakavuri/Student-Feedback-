using System.Web.Mvc;

namespace Gios_mvcSolution.Areas.GIOS
{
    public class GIOSAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GIOS";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GIOS_default",
                "GIOS/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}