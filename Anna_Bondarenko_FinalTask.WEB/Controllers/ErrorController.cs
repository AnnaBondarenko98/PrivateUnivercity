using System.Web.Mvc;

namespace Anna_Bondarenko_FinalTask.WEB.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            if (!(TempData["Error"] is string message))
            {
                return HttpNotFound();
            }

            TempData.Remove("Error");
            return View("Error", "_Layout", message);
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}