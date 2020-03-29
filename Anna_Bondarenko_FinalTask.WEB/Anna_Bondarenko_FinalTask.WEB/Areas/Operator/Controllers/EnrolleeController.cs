using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Controllers
{
    [Authorize(Roles = "Operator")]
    public class EnrolleeController : Controller
    {
        private readonly IOperatorService _operatorService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IStatementService _statementService;
        private readonly IStudentStatusService _studentStatusService;


        public EnrolleeController(IOperatorService operatorService, IEnrolleeService enrolleeService, IStatementService statementService,IStudentStatusService studentStatusService)
        {
            _operatorService = operatorService;
            _enrolleeService = enrolleeService;
            _statementService = statementService;
            _studentStatusService = studentStatusService;
        }

        public ActionResult GetEnrollees()
        {
            var someEnr = _enrolleeService.GetEnrolleWithoutAddedFaculty();

            return View(someEnr);
        }

        public ActionResult AddRoleLock(int id)
        {
            _operatorService.AddLockRole(_enrolleeService.Get(id));

            return RedirectToAction("GetEnrollees", new { controller = "Enrollee", area = "Operator" });
        }

        public ActionResult AddToStatement(int id)
        {
            var enrolle = _enrolleeService.Get(id);

            var statement = new Statement();

            var statements = _statementService.GetAll();

            if (!statements.Any())
            {
                statement.Enrollee = new List<Anna_Bondarenko_FinalTask.Models.Models.Enrollee>
                {
                    enrolle
                };

                _statementService.Create(statement);
            }
            else if(!statements.Last().Enrollee.Contains(enrolle) && !statements.Last().IsEnrolled)
            {
                    statement = _statementService.GetAll().Last();
                    statement.Enrollee.Add(enrolle);
                    _statementService.Update(statement);
            }
            else if(statements.Last().IsEnrolled)
            {
                statement.Enrollee = new List<Anna_Bondarenko_FinalTask.Models.Models.Enrollee>
                    {
                        enrolle
                    };

                _statementService.Create(statement);
            }
            return RedirectToAction("GetEnrollees", new { controller = "Enrollee", area = "Operator" });
        }

        public ActionResult EnroleeDelete(int id)
        {
            var enrollee = _enrolleeService.Get(id);

            return View(enrollee);
        }

        [HttpPost, ActionName("EnroleeDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var enrollee = _enrolleeService.Get(id);

            var statuses = enrollee.StudentStatuses.Where(s => s.FacultyStatus == false && s.Enrollee.Email == enrollee.Email).ToList();

            var faculties = enrollee.Faculties.Where(f=>f.StudentStatuses.Any(s => s.FacultyStatus==false)).ToList();

            faculties.ForEach(f=> enrollee.Faculties.Remove(f));

            statuses.ForEach(s => enrollee.StudentStatuses.Remove(s)); 
            
            statuses.ForEach(s=>_studentStatusService.Delete(s.Id));

            _enrolleeService.Update(enrollee);

            var statements = _statementService.GetAll();

            if (!statements.Any() | !statements.Last().Enrollee.Contains(enrollee))
            {
                return RedirectToAction("GetStatement", "Statement", new { area = "Operator" });
            }

            var statement = _statementService.GetAll().Last();
            statement.Enrollee.Remove(enrollee);
            _statementService.Update(statement);

            return RedirectToAction("GetEnrollees", "Enrollee", new { area = "Operator" });
        }

        public FileContentResult GetImage(int id)
        {
            var enrollee = _enrolleeService.Get(id);

            return enrollee?.ImageData != null ? File(enrollee.ImageData, enrollee.ImageMimeType) : null;
        }
    }
}