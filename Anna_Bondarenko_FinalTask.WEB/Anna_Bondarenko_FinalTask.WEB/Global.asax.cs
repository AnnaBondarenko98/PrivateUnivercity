using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Anna_Bondarenko_FinalTask.WEB.Infrastructure.Autofac;
using Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper;

namespace Anna_Bondarenko_FinalTask.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            MapperInitialize.InitializeAutoMapper();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
