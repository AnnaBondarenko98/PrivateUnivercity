using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Controllers
{
    [Authorize(Roles = "Operator")]
    public class FacultyController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFacultyService _facultyService;
        private readonly IFacultySubjectService _facultySubjectService;
        private readonly IExaminationSubjectService _examinationSubjectService;

        public FacultyController(IMapper mapper, IFacultyService facultyService, IFacultySubjectService facultySubjectService, IExaminationSubjectService examinationSubjectService)
        {
            _mapper = mapper;
            _facultyService = facultyService;
            _facultySubjectService = facultySubjectService;
            _examinationSubjectService = examinationSubjectService;
        }

        public ActionResult GetFaculty()
        {
            var faculties = _facultyService.GetAll();

            return View(faculties);
        }
        public ActionResult SortByName()
        {
            var faculties = _facultyService.GetAll().OrderByDescending(f=>f.Name);

            return View("GetFaculty",faculties);
        }

        [HttpGet]
        public ActionResult FacultyCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FacultyCreate(Faculty faculty)
        {
            if (!ModelState.IsValid)
            {
                return View(faculty);
            }
            else if ( _facultyService.Find(f => f.FacultyNumber == faculty.FacultyNumber).Any() )
            {
                ModelState.AddModelError("FacultyCreate", "This faculty already exists.");

                return View(faculty);
            }
            else if (faculty.AllPlaces<faculty.BudgetPlaces)
            {

                ModelState.AddModelError("FacultyCreate", "Number of budget seats mast be less or equal than all seats number.");

                return View(faculty);
            }

            _facultyService.Create(faculty);

            return RedirectToAction("GetFaculty", "Faculty", new { area = "Operator" });
        }

        [HttpGet]
        public ActionResult FacultyEdit(int id)
        {
            var subjects = _facultySubjectService.GetAll();
            var examSubjects = _examinationSubjectService.GetAll();

            var checkTags = _mapper.Map<ICollection<CheckModel>>(subjects);
            var checkExamTags = _mapper.Map<ICollection<CheckExamSubjects>>(examSubjects);

            var faculty = _facultyService.Get(id);
            var editFaculty = _mapper.Map<EditFaculty>(faculty);

            foreach (var item in checkTags)
            {
                if (editFaculty.FacultySubjects.Any(subject => subject.Name == item.Subject?.Name))
                {
                    item.Checked = true;
                }
            }

            foreach (var item in checkExamTags)
            {
                if (editFaculty.ExaminationSubjects.Any(subject => subject.Name == item.Subject?.Name))
                {
                    item.Checked = true;
                }
            }

            editFaculty.CheckSubjects = checkTags;
            editFaculty.CheckExamSubjects = checkExamTags;

            return View(editFaculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FacultyEdit(EditFaculty editFaculty, string[] names, string[] namesExam)
        {
            if (!ModelState.IsValid)
            {
                return View(editFaculty);
            }

            var faculty = _facultyService.Get(editFaculty.Id);

            _mapper.Map(editFaculty, faculty);

            if (names != null)
            {
                faculty.FacultySubjects.Clear();
                _facultyService.GetFacultyWithFacultySubjects(faculty, names);
            }
            else
            {
                faculty.FacultySubjects = _facultyService.GetFacultySubjects(faculty.Id).ToList();
            }

            if (namesExam != null)
            {
                faculty.ExaminationSubjects.Clear();
                _facultyService.GetFacultyWithExamSubjects(faculty, namesExam);
            }
            else
            {
                faculty.ExaminationSubjects = _facultyService.GetExamSubjects(faculty.Id).ToList();
            }

            _facultyService.Update(faculty);

            return RedirectToAction("GetFaculty", "Faculty", new { area = "Operator" });
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

            if (faculty.Enrollees.Count != 0)
            {
                ModelState.AddModelError("FacultyDelete", "Students are still enrolled in this faculty");
                return RedirectToAction("GetFaculty", "Faculty", new { area = "Operator" }); ;
            }

            return View(faculty);
        }

        [HttpPost, ActionName("FacultyDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _facultyService.Delete(id);

            return RedirectToAction("GetFaculty", "Faculty", new { area = "Operator" });
        }
    }
}