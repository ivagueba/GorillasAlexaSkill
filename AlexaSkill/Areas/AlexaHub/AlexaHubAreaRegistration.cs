using System.Web.Mvc;

namespace AlexaSkill.Areas.AlexaHub
{
    public class AlexaHubAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AlexaHub";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AlexaHub_default",
                "AlexaHub/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}