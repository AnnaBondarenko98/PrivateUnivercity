using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Infrastructure.Autofac;
using Anna_Bondarenko_FinalTask.WEB.Infrastructure.Autofac.DIModules;
using Autofac;
using Autofac.Integration.Mvc;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new ServiceModule("CommitteeContext"));
            builder.RegisterModule(new WebModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}