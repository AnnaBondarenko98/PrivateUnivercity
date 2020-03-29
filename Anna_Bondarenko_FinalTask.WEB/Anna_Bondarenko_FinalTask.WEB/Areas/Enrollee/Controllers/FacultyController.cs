using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Enrollee.Controllers
{
    [Authorize(Roles = "User")]
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly ISchoolSubjectsService _schoolSubjectsService;
        private readonly IStudentStatusService _studentStatusService;
        private readonly IExaminationSubjectService _examinationSubjectService;
        private readonly IMarkService _markService;

        public FacultyController(IFacultyService facultyService, IMarkService markService, IEnrolleeService enrolleeService, ISchoolSubjectsService schoolSubjectsService, IStudentStatusService studentStatusService, IExaminationSubjectService examinationSubjectService)
        {
            _facultyService = facultyService;
            _enrolleeService = enrolleeService;
            _schoolSubjectsService = schoolSubjectsService;
            _studentStatusService = studentStatusService;
            _examinationSubjectService = examinationSubjectService;
            _markService = markService;
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            var currentEnrollee = _enrolleeService.Find(customer => customer.AppCustomer.UserName == User.Identity.Name).First();

            var faculty = _facultyService.Get(id);

            var addedFaculty = _facultyService.GetAll().Where(f => f.FacultyNumber == faculty.FacultyNumber && faculty.Enrollees.Any()).ToList();

            if (currentEnrollee.Faculties.Any(f => f.FacultyNumber == faculty.FacultyNumber))
            {
                ModelState.AddModelError("AddFaculty", "This faculty already added.");
                return View();
            }
            else if (addedFaculty.Any() && addedFaculty.First().Enrollees.Any() &&
                     addedFaculty.First().Enrollees.Count(e => e.StudentStatuses.Any(s => s.FacultyStatus == true && s.Faculty.FacultyNumber == faculty.FacultyNumber))
                     == faculty.AllPlaces)
            {
                ModelState.AddModelError("AddFaculty", "No places are there :(");

                return View();
            }
            else
            {
                currentEnrollee.ExaminationSubjects.Clear();
                currentEnrollee.SchoolSubjects.Clear();
                foreach (var item in _schoolSubjectsService.GetAll().ToList())
                {
                    if (!currentEnrollee.SchoolSubjects.Contains(item))
                    {
                        currentEnrollee.SchoolSubjects.Add(item);
                    }
                }

                var exSubjects = _examinationSubjectService.Find(e => e.Faculties.Any(f => f.FacultyNumber == faculty.FacultyNumber)).ToList();

                foreach (var item in exSubjects)
                {
                    if (!currentEnrollee.ExaminationSubjects.Any(s => s.Name == item.Name))
                    {
                        currentEnrollee.ExaminationSubjects.Add(item);
                    }
                }

                currentEnrollee.Faculties.Add(faculty);

                var status = new StudentStatus() { Faculty = faculty, FacultyStatus = false, Enrollee = currentEnrollee, Status = "Enrollee" };

                _studentStatusService.Create(status);

                _enrolleeService.Update(currentEnrollee);

                return View("Create", currentEnrollee);
            }
        }

        [HttpPost]
        public ActionResult AddFaculty(int[] examMark, int[] schoolMark)
        {
            var currentEnrollee = _enrolleeService.Find(customer => customer.AppCustomer.UserName == User.Identity.Name).First();

            var exSubjects = _examinationSubjectService.Find(ex => ex.Enrollees.Any(e => e.Email == currentEnrollee.Email)).ToList();

            if (examMark != null)
            {
                for (var i = 0; i < examMark.Length; i++)
                {
                    var mark = new Mark { StudentMark = examMark[i], Enrollee = currentEnrollee, ExaminationSubject = exSubjects[i] };

                    _markService.Create(mark);

                    if (exSubjects[i].Marks.Any(m => m.ExaminationSubject.Name == exSubjects[i].Name &&
                                                     m.Enrollee.Email == currentEnrollee.Email))
                    {
                        exSubjects[i].Marks.First(m => m.ExaminationSubject.Name == exSubjects[i].Name &&
                             m.Enrollee.Email == currentEnrollee.Email).StudentMark = examMark[i];

                    }
                    else
                    {
                        exSubjects[i].Marks.Add(_markService.Find(m => m.Enrollee.Email == currentEnrollee.Email && 
                                                                       m.ExaminationSubject.Name == exSubjects[i].Name).Last());

                    }
                }
            }

            exSubjects.ForEach(s => _examinationSubjectService.Update(s));

            var schoolSubjects = _schoolSubjectsService.Find(ex => ex.Enrollees.Any(e => e.Email == currentEnrollee.Email)).ToList();

            if (schoolMark != null)
            {
                for (var i = 0; i < schoolMark.Length; i++)
                {
                    var mark = new Mark { StudentMark = schoolMark[i], Enrollee = currentEnrollee,
                        SchoolSubject = schoolSubjects[i] };

                    _markService.Create(mark);

                    if (schoolSubjects[i].Marks.Any(m => m.SchoolSubject.Name == schoolSubjects[i].Name && 
                                                         m.Enrollee.Email == currentEnrollee.Email))
                    {
                        schoolSubjects[i].Marks.First(m => m.SchoolSubject.Name == schoolSubjects[i].Name &&
                                                       m.Enrollee.Email == currentEnrollee.Email).StudentMark = schoolMark[i];
                    }
                    else
                    {
                        schoolSubjects[i].Marks.Add(_markService.Find(m => m.Enrollee.Email == currentEnrollee.Email &&
                                                                           m.SchoolSubject.Name == schoolSubjects[i].Name).Last());

                    }
                }
            }

            schoolSubjects.ForEach(s => _schoolSubjectsService.Update(s));

            _enrolleeService.Update(currentEnrollee);

            return RedirectToAction("GetFaculty", new { area = string.Empty, controller = "Faculty" });
        }
    }
}