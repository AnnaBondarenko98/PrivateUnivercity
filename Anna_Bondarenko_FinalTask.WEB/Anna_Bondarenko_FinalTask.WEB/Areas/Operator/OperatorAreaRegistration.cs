using System.Web.Mvc;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator
{
    public class OperatorAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Operator";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Operator_default",
                "Operator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}