using System.Web.Mvc;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Enrollee
{
    public class EnrolleeAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Enrollee";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Enrollee_default",
                "Enrollee/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}