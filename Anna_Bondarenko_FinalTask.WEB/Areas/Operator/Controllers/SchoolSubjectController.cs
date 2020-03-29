using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Controllers
{
    [Authorize(Roles = "Operator")]
    public class SchoolSubjectController : Controller
    {
        private readonly ISchoolSubjectsService _schoolSubjectsService;

        public SchoolSubjectController(ISchoolSubjectsService schoolSubjectsService)
        {
            _schoolSubjectsService = schoolSubjectsService;
        }

        public ActionResult GetSchoolSubjects()
        {
            var subjects = _schoolSubjectsService.GetAll();

            return View(subjects);
        }

        [HttpGet]
        public ActionResult SchoolSubjectCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SchoolSubjectCreate(SchoolSubject schoolSubject)
        {
            if (!ModelState.IsValid | _schoolSubjectsService.Find(s => s.Name == schoolSubject.Name).Any())
            {
                ModelState.AddModelError("SchoolSubjectError", "This exam subject already exists.");

                return View(schoolSubject);
            }

            _schoolSubjectsService.Create(schoolSubject);

            return RedirectToAction("GetSchoolSubjects", "SchoolSubject", new { area = "Operator" });
        }

        [HttpGet]
        public ActionResult SchoolSubjectEdit(int id)
        {
            var sub = _schoolSubjectsService.Get(id);

            return View(sub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SchoolSubjectEdit(SchoolSubject schoolSubject)
        {
            if (!ModelState.IsValid)
            {
                return View(schoolSubject);
            }

            _schoolSubjectsService.Update(schoolSubject);

            return RedirectToAction("GetSchoolSubjects", "SchoolSubject", new { area = "Operator" });
        }

        public ActionResult SchoolSubjectDelete(int id)
        {
            var schoolSubject = _schoolSubjectsService.Get(id);
            if (schoolSubject.Marks.Any())
            {
                ModelState.AddModelError("SchoolSubjectDelete", "The enrolles have already added their results on this subject.");
            }

            return View(schoolSubject);
        }

        [HttpPost, ActionName("SchoolSubjectDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _schoolSubjectsService.Delete(id);

            return RedirectToAction("GetSchoolSubjects", "SchoolSubject", new { area = "Operator" });
        }
    }
}