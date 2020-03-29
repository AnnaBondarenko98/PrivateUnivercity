using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Controllers
{
    [Authorize(Roles = "Operator")]
    public class StatementController : Controller
    {
        private readonly IEnrolleeService _enrolleeService;
        private readonly IStatementService _statementService;
        private readonly IStudentStatusService _studentStatusService;

        public StatementController(IEnrolleeService enrolleeService, IStatementService statementService, IStudentStatusService studentStatusService)
        {
            _enrolleeService = enrolleeService;
            _statementService = statementService;
            _studentStatusService = studentStatusService;
        }

        public ActionResult GetStatement()
        {
            var statements = _statementService.GetAll().ToList();

            if (!statements.Any() || statements.Last().IsEnrolled)
            {
                return View();
            }

            var statement = statements.Last();

            return View(statement);
        }

        public ActionResult GetAllAddedStudent()
        {
            var enrollees = _enrolleeService.GetAll().Where(e => e.StudentStatuses.Any(s=>s.FacultyStatus==true)).ToList();

            return View(enrollees);
        }

        public ActionResult AddedStudentDelete(int id, int facultyid)
        {
            var enrolee = _enrolleeService.Get(id);

            var faculty = enrolee.Faculties.First(f => f.Id == facultyid);

            if (enrolee.Faculties.Count == 1 && enrolee.Faculties.First().Id == facultyid)
            {

                var statement = _statementService.Find(s => s.Enrollee.Count(e => e.Faculties.Contains(faculty) && e.Id==enrolee.Id) > 0).First();

                enrolee.Faculties.Remove(faculty);

                var status = enrolee.StudentStatuses.Where(s =>
                    s.Faculty.FacultyNumber == faculty.FacultyNumber && s.Enrollee.Email == enrolee.Email).First();

                enrolee.StudentStatuses.Remove(status);

                statement.Enrollee.Remove(enrolee);

                _studentStatusService.Delete(status.Id);

                _enrolleeService.Update(enrolee);
                _statementService.Update(statement);
            }
            else
            {
                var status = enrolee.StudentStatuses.Where(s =>
                    s.Faculty.FacultyNumber == faculty.FacultyNumber && s.Enrollee.Email == enrolee.Email).First();

                 enrolee.StudentStatuses.Remove(status);

                enrolee.Faculties.Remove(faculty);

                _studentStatusService.Delete(status.Id);

                _enrolleeService.Update(enrolee);
            }
            return RedirectToAction("GetAllAddedStudent", "Statement", new { area = "Operator" });
        }

        [HttpGet]
        public ActionResult StatementDelete(int id)
        {
            var enrolee = _enrolleeService.Get(id);

            return View(enrolee);
        }

        [HttpPost, ActionName("StatementDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var enrolle = _enrolleeService.Get(id);

            var statements = _statementService.GetAll();

            if (!statements.Any() | !statements.Last().Enrollee.Contains(enrolle))
            {
                return RedirectToAction("GetStatement", "Statement", new { area = "Operator" });
            }

            var statement = _statementService.GetAll().Last();
            statement.Enrollee.Remove(enrolle);
            _statementService.Update(statement);

            return RedirectToAction("GetStatement", "Statement", new { area = "Operator" });
        }

        public ActionResult Calculate()
        {
            var statement = _statementService.GetAll().Last();

            if (statement.IsEnrolled)
            {
                return RedirectToAction("GetStatement", "Statement", new { area = "Operator" });
            }

            _statementService.SendMessageForAddedStudents();

            return RedirectToAction("GetStatement", "Statement", new { area = "Operator" });
        }

        public void SendEmail(int id, string facultyName, string format)
        {
            var model = new MessageDto
            {
                From = "nyutka831@gmail.com",
                To = _enrolleeService.Get(id).Email,
                Body = "you are registered at the faculty " + facultyName + " (" + format + ")\n"
            };

            _enrolleeService.SendMessage(model);
        }
    }
}