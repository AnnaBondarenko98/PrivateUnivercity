using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService, IMapper mapper)
        {
            _facultyService = facultyService;
        }

        public ActionResult GetFaculty()
        {
            var faculties = _facultyService.GetAll();

            return View(faculties);
        }

        [HttpPost]
        public ActionResult FacultySort(int id,string from = null, string to = null)
        {
            if (from == null || from == "")
            {
                from =_facultyService.GetAll().Min(f=>f.BudgetPlaces).ToString();
            }
            if (to == null || to == "")
            {
                to = _facultyService.GetAll().Max(f => f.BudgetPlaces).ToString();
            }

            var faculties = _facultyService.GetAll();
            faculties = _facultyService.Sort(id,from,to,faculties).ToList();

            return PartialView("GetFacultyPartial", faculties);
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
    }
}