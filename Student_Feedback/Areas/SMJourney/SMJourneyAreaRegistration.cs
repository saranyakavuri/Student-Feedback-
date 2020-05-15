using System.Web.Mvc;

namespace Gios_mvcSolution.Areas.SMJourney
{
    public class SMJourneyAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SMJourney";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SMJourney_default",
                "SMJourney/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}