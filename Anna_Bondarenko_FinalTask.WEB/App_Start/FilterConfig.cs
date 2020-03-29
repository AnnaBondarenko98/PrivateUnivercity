using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.WEB.Filters;
using Autofac.Integration.Mvc;
using NLog;

namespace Anna_Bondarenko_FinalTask.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var logger = AutofacDependencyResolver.Current.GetService<ILogger>();

            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilter(logger));
        }
    }
}
