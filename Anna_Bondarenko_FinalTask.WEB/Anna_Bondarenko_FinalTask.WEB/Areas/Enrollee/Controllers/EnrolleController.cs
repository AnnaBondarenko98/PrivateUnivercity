using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Enrollee.Controllers
{
    [Authorize(Roles = "User")]
    public class EnrolleController : Controller
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IFacultyService _facultyService;
        private readonly IStudentStatusService _studentStatusService;


        public EnrolleController(IEnrolleeService enrolleeService, IFacultyService facultyService, IStudentStatusService studentStatusService)
        {
            _enrolleeService = enrolleeService;
            _facultyService = facultyService;
            _studentStatusService = studentStatusService;
        }

        [HttpGet]
        public ActionResult GetFaculties()
        {
            var currentEnrollee = _enrolleeService.Find(customer => customer.AppCustomer.UserName == User.Identity.Name).First();

            return View(currentEnrollee.Faculties);
        }

        [HttpGet]
        public ActionResult GetProfile()
        {
            var currentEnrollee = _enrolleeService.Find(customer => customer.AppCustomer.UserName == User.Identity.Name).First();

            return View(currentEnrollee);
        }

        public ActionResult FacultyDetails(int id)
        {
            var faculty = _facultyService.Get(id);

            if (faculty == null)
            {
                return HttpNotFound();
            }

            return View(faculty);
        }

        public ActionResult FacultyDelete(int id)
        {
            var faculty = _facultyService.Get(id);

            if (faculty == null)
            {
                return HttpNotFound();
            }

            var currentEnrollee = _enrolleeService.Find(customer => customer.AppCustomer.UserName == User.Identity.Name).First();

            currentEnrollee.Faculties.Remove(faculty);

            var status = currentEnrollee.StudentStatuses.Where(s=>s.Faculty.FacultyNumber==faculty.FacultyNumber && s.Enrollee.Email == currentEnrollee.Email).ToList();

            status.ForEach(s=>currentEnrollee.StudentStatuses.Remove(s));

            status.ForEach(s => _studentStatusService.Delete(s.Id));

            _enrolleeService.Update(currentEnrollee);

            return RedirectToAction("GetFaculties", new { controller = "Enrolle", area = "Enrollee" });
        }

        public ActionResult EnrolleEdit(int id)
        {
            var enrolle = _enrolleeService.Get(id);

            if (enrolle == null)
            {
                return HttpNotFound();
            }

            return View(enrolle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult EnrolleEdit(Anna_Bondarenko_FinalTask.Models.Models.Enrollee enrollee, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EnrolleEdit", new {controller = "Enrolle", area = "Enrollee"});
            }

             string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            if (image != null && image.ContentType.Contains("image") && formats.Any(item =>
                    image.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)))
            {
                enrollee = _enrolleeService.VerifyImage(enrollee, image);
            }

            _enrolleeService.Update(enrollee);

            return RedirectToAction("GetProfile", new { controller = "Enrolle", area = "Enrollee" });
        }

        public FileContentResult GetImage(int id)
        {
            var enrollee = _enrolleeService.Get(id);

            if (enrollee?.ImageData != null)
            {
                return File(enrollee.ImageData, enrollee.ImageMimeType);
            }

            return null;
        }
    }
}