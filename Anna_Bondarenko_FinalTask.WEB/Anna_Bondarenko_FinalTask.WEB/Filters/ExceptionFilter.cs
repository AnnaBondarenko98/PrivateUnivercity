using System.Web.Mvc;
using NLog;


namespace Anna_Bondarenko_FinalTask.WEB.Filters
{
    public class ExceptionFilter:FilterAttribute,IExceptionFilter
    {
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception != null)
            {
                _logger.Error($" Error message : {filterContext.Exception.Message}, place : {filterContext.Controller}");
                filterContext.Controller.TempData.Add("Error", filterContext.Exception.Message);
                filterContext.RequestContext.HttpContext.Response.RedirectToRoute("Error");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}