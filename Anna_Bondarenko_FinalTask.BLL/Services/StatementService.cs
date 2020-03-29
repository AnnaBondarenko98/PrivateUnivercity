using System;
using System.Collections.Generic;
using System.Linq;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class StatementService : IStatementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMessageSender _messageSender;
        private readonly IStudentStatusService _studentStatusService;

        private readonly IMarkService _markService;


        public StatementService(IUnitOfWork unitOfWork, ILogger logger, IMessageSender messageSender, IStudentStatusService studentStatusService, IMarkService markService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _messageSender = messageSender;
            _studentStatusService = studentStatusService;
            _markService = markService;
        }

        public IEnumerable<Statement> GetAll()
        {
            _logger.Info("Getting all statements");

            var statements = _unitOfWork.StatementGenericRepository.GetAll();

            return statements;
        }

        public Statement Get(int id)
        {
            _logger.Info($"Getting statement by id {id}");

            var statement = _unitOfWork.StatementGenericRepository.Get(id);

            return statement;
        }

        public void Create(Statement statement)
        {
            _logger.Info($"Creating new statement {statement}");

            _unitOfWork.StatementGenericRepository.Create(statement);
        }

        public void Update(Statement statement)
        {
            _logger.Info($"TUpdating statement {statement}");

            _unitOfWork.StatementGenericRepository.Update(statement);
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting statement by id {id}");

            _unitOfWork.StatementGenericRepository.Delete(id);
        }

        public void DeleteRange(IEnumerable<Statement> statements)
        {
            _logger.Info("Deleting the range of statements");

            _unitOfWork.StatementGenericRepository.DeleteRange(statements);
        }

        public IEnumerable<Statement> Find(Func<Statement, bool> parameter)
        {
            _logger.Info("Finding statements by parameter");

            var statements = _unitOfWork.StatementGenericRepository.Find(parameter);

            return statements;
        }

        public IEnumerable<Enrollee> GetBudget(Statement statement, Faculty faculty)
        {
            var budget = new List<Enrollee>();

            var budgetCount = faculty.BudgetPlaces;

            if (faculty.StudentStatuses.Any())
            {
                budgetCount = faculty.BudgetPlaces - faculty.StudentStatuses.Count(s => s.FacultyStatus == true && s.Faculty.FacultyNumber == faculty.FacultyNumber);
            }

            _logger.Info("Getting budget students");

            if (budgetCount > 0)
            {

                budget = statement.Enrollee
           .Where(e => e.Faculties.SkipWhile(f => f.Name != faculty.Name).Any())
           .OrderByDescending(o => o.ExaminationSubjects.Sum(s => s.Marks.First(m => m.ExaminationSubject.Name==s.Name && m.Enrollee.Email == o.Email).StudentMark) + o.SchoolSubjects.Sum(s => s.Marks.First(m => m.SchoolSubject.Name == s.Name && m.Enrollee.Email == o.Email).StudentMark))
           .Take(budgetCount).ToList();
            }

            return budget;
        }

        public IEnumerable<Enrollee> GetContract(Statement statement, Faculty faculty)
        {
            var contract = new List<Enrollee>();

            _logger.Info("Getting contract students");

            var placesCount = faculty.AllPlaces;

            if (faculty.StudentStatuses.Any())
            {
                placesCount = faculty.AllPlaces - faculty.StudentStatuses.Count(s => s.FacultyStatus == true && s.Faculty.FacultyNumber == faculty.FacultyNumber);
            }

            if (placesCount > 0 && placesCount <= (faculty.AllPlaces - faculty.BudgetPlaces))
            {

                var added = faculty.Enrollees.Where(e => e.StudentStatuses.Any(s=>s.FacultyStatus==true && s.Faculty.FacultyNumber==faculty.FacultyNumber)).ToList();

                contract = statement.Enrollee.ToList();

                foreach (var item in added)
                {
                    if (contract.Contains(item))
                    {
                        contract.Remove(item);
                    }
                }

                contract = contract.OrderByDescending(o => o.ExaminationSubjects.Sum(s => s.Marks.First(m => m.ExaminationSubject.Name == s.Name && m.Enrollee.Email == o.Email).StudentMark) + o.SchoolSubjects.Sum(s => s.Marks.First(m => m.SchoolSubject.Name == s.Name && m.Enrollee.Email == o.Email).StudentMark)).Take(placesCount).ToList();
            }

            return contract;
        }

        public void SendMessageForAddedStudents()
        {
            var statement = _unitOfWork.StatementGenericRepository.GetAll().Last();

            foreach (var faculty in _unitOfWork.FacultyGenericRepository.GetAll().Where(f => f.StudentStatuses.Any(s => s.FacultyStatus == false)))
            {
                var budget = GetBudget(statement, faculty);

                _logger.Info("Sending E-mails to budget students");

                foreach (var item1 in budget)
                {
                    if (faculty.Enrollees.Where(e => e.Email == item1.Email).All(e => e.Added != true))
                    {
                        var notAdded = item1.Faculties.First(f => f.FacultyNumber == faculty.FacultyNumber);

                        if (notAdded != null)
                        {
                            SendEmail(item1.Id, faculty.Name, "Budget Place");
                            var status = item1.StudentStatuses.First(s => s.Faculty.FacultyNumber == faculty.FacultyNumber);
                            status.Status = "Budget";
                            status.FacultyStatus = true;

                            _studentStatusService.Update(status);

                            _unitOfWork.FacultyGenericRepository.Update(faculty);

                            _unitOfWork.EnrolleeGenericRepository.Update(item1);
                        }
                    }
                }

                var contract = GetContract(statement, faculty);

                _logger.Info("Sending E-mails to contract students");

                foreach (var item1 in contract)
                {
                    if (faculty.Enrollees.Where(e => e.Email == item1.Email).All(e => e.Added != true))
                    {
                        var notAdded = item1.Faculties.First(f => f.FacultyNumber == faculty.FacultyNumber);

                        if (notAdded != null)
                        {
                            SendEmail(item1.Id, faculty.Name, "Contract Place");
                            var status = item1.StudentStatuses.First(s => s.Faculty.FacultyNumber == faculty.FacultyNumber);
                            status.Status = "Contract";
                            status.FacultyStatus = true;

                            _studentStatusService.Update(status);

                            _unitOfWork.FacultyGenericRepository.Update(faculty);

                            _unitOfWork.EnrolleeGenericRepository.Update(item1);

                        }
                    }
                }
            }

            var count = statement.Enrollee.Count();
            for (int i = 0; i < count; i++)
            {
                if (statement.Enrollee.ToArray()[i].StudentStatuses.Any(s => s.FacultyStatus != true))
                {
                    var status = statement.Enrollee.ToArray()[i].StudentStatuses.Where(s => s.FacultyStatus != true).ToList();
                    foreach (var item in status)
                    {
                        statement.Enrollee.ToArray()[i].StudentStatuses.Remove(item);

                        statement.Enrollee.ToArray()[i].Faculties.Remove(item.Faculty);

                        _studentStatusService.Delete(item.Id);
                    }
                }



            }
            statement.IsEnrolled = true;
            _unitOfWork.StatementGenericRepository.Update(statement);
        }

        private void SendEmail(int id, string facultyName, string format)
        {
            var model = new MessageDto
            {
                From = "nyutka831@gmail.com",
                To = "anna.bondarenko@nure.ua",
                Body = $"{_unitOfWork.EnrolleeGenericRepository.Get(id).Email},you were registered into the faculty " + facultyName + " (" + format + ")\n"
            };
            SendMessage(model);
        }

        private void SendMessage(MessageDto sendMessage)
        {
            _logger.Info("Sending the message");

            _messageSender.SendToUs(sendMessage);
        }
    }
}
