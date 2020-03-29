using System.Web.Mvc;
using System.Web.Routing;

namespace Anna_Bondarenko_FinalTask.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Faculty", action = "GetFaculty", id = UrlParameter.Optional },
                namespaces: new[] { "Anna_Bondarenko_FinalTask.WEB.Controllers" });

            routes.MapRoute(
                name: "Error",
                url: "Error/Error/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
