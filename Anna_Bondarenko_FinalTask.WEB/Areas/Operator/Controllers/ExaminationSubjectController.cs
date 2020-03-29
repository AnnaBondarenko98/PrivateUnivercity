using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Controllers
{
    [Authorize(Roles = "Operator")]
    public class ExaminationSubjectController : Controller
    {
        private readonly IExaminationSubjectService _examinationSubjectService;

        public ExaminationSubjectController(IExaminationSubjectService examinationSubjectService)
        {
            _examinationSubjectService = examinationSubjectService;
        }

        public ActionResult GetExaminationSubject()
        {
            var subjects = _examinationSubjectService.GetAll();

            return View(subjects);
        }

        [HttpGet]
        public ActionResult ExaminationSubjectCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExaminationSubjectCreate(ExaminationSubject schoolSubject)
        {
            if (!ModelState.IsValid | _examinationSubjectService.Find(s => s.Name == schoolSubject.Name).Any())
            {
                ModelState.AddModelError("ExamError", "This exam subject already exists.");

                return View(schoolSubject);
            }

            _examinationSubjectService.Create(schoolSubject);

            return RedirectToAction("GetExaminationSubject", "ExaminationSubject", new { area = "Operator" });
        }

        [HttpGet]
        public ActionResult ExaminationSubjectEdit(int id)
        {
            var sub = _examinationSubjectService.Get(id);

            return View(sub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExaminationSubjectEdit(ExaminationSubject examinationSubject)
        {
            if (!ModelState.IsValid)
            {
                return View(examinationSubject);
            }

            _examinationSubjectService.Update(examinationSubject);

            return RedirectToAction("GetExaminationSubject", "ExaminationSubject", new { area = "Operator" });
        }

        public ActionResult ExaminationSubjectDelete(int id)
        {
            var subject = _examinationSubjectService.Get(id);
            if (subject.Marks.Any())
            {
                ModelState.AddModelError("ExSubjectDelete", "The enrolles have already added their results on this subject.");
            }
            return View(subject);
        }

        [HttpPost, ActionName("ExaminationSubjectDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _examinationSubjectService.Delete(id);

            return RedirectToAction("GetExaminationSubject", "ExaminationSubject", new { area = "Operator" });
        }
    }
}