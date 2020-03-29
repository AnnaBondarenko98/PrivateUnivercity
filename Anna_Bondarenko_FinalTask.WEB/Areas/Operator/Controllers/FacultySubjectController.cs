using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Controllers
{
    [Authorize(Roles = "Operator")]
    public class FacultySubjectController : Controller
    {
        private readonly IFacultySubjectService _facultySubjectService;

        public FacultySubjectController(IFacultySubjectService facultySubjectService)
        {
            _facultySubjectService = facultySubjectService;
        }

        public ActionResult GetFacultySubjects()
        {
            var subjects = _facultySubjectService.GetAll();

            return View(subjects);
        }

        [HttpGet]
        public ActionResult FacultySubjectCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FacultySubjectCreate(FacultySubject facultySubject)
        {
            if (!ModelState.IsValid | _facultySubjectService.Find(f => f.Name == facultySubject.Name).Any())
            {
                ModelState.AddModelError("FacultySubjectError", "This exam subject already exists.");

                return View(facultySubject);
            }

            _facultySubjectService.Create(facultySubject);

            return RedirectToAction("GetFacultySubjects", "FacultySubject", new { area = "Operator" });
        }

        [HttpGet]
        public ActionResult FacultySubjectEdit(int id)
        {
            var sub = _facultySubjectService.Get(id);

            return View(sub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacultySubjectEdit(FacultySubject facultySubject)
        {
            if (!ModelState.IsValid)
            {
                return View(facultySubject);
            }

            _facultySubjectService.Update(facultySubject);

            return RedirectToAction("GetFacultySubjects", "FacultySubject", new { area = "Operator" });
        }

        public ActionResult FacultySubjectDelete(int id)
        {
            var facultySubject = _facultySubjectService.Get(id);

            return View(facultySubject);
        }

        [HttpPost, ActionName("FacultySubjectDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _facultySubjectService.Delete(id);

            return RedirectToAction("GetFacultySubjects", "FacultySubject", new { area = "Operator" });
        }
    }
}