using System;
using System.Collections.Generic;
using System.Linq;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public FacultyService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Faculty> GetAll()
        {
            _logger.Info("Getting all faculties ");

            var faculties = _unitOfWork.FacultyGenericRepository.GetAll();

            return faculties;
        }

        public Faculty Get(int id)
        {
            _logger.Info($"Getting faculty by id={id}");

            var faculty = _unitOfWork.FacultyGenericRepository.Get(id);

            return faculty;
        }

        public void Create(Faculty faculty)
        {
            _logger.Info($"Creating new faculty {faculty.Name}");

            _unitOfWork.FacultyGenericRepository.Create(faculty);
        }

        public void Update(Faculty faculty)
        {
            _logger.Info($"Updating the faculty {faculty.Name}");

            _unitOfWork.FacultyGenericRepository.Update(faculty);
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting the faculty by id={id}");

            var faculty= _unitOfWork.FacultyGenericRepository.Get(id);

            _unitOfWork.FacultyGenericRepository.Delete(id);
        }

        public IEnumerable<FacultySubject> GetFacultySubjects(int id)
        { 
            _logger.Info($"Finding faculty subjects by faculty id {id}");

            var faculty = _unitOfWork.FacultyGenericRepository.Get(id).FacultySubjects;

            return faculty;
        }

        public IEnumerable<ExaminationSubject> GetExamSubjects(int id)
        {
            _logger.Info($"Finding exam subjects by id = {id}");

            var faculty = _unitOfWork.FacultyGenericRepository.Get(id).ExaminationSubjects;

            return faculty;
        }

        public void GetFacultyWithFacultySubjects(Faculty faculty, IEnumerable<string> names)
        {
            _logger.Info($"Getting faculty {faculty.Name} with faculty subject");

            var tags = _unitOfWork.FacultySubjectGenericRepository.Find(subject => names.Contains(subject.Name));

            faculty.FacultySubjects = tags.ToList();
        }

        public void GetFacultyWithExamSubjects(Faculty faculty, IEnumerable<string> names)
        {
            _logger.Info($"Getting faculty {faculty.Name} with exam subject");

            var tags = _unitOfWork.ExamGenericRepository.Find(subject => names.Contains(subject.Name));

            faculty.ExaminationSubjects = tags.ToList();
        }

        public IEnumerable<Faculty> Find(Func<Faculty, bool> parameter)
        {
            _logger.Info($"Finding faculties by parameter");

            var faculties = _unitOfWork.FacultyGenericRepository.Find(parameter);

            return faculties;
        }

        public IEnumerable<Faculty> Sort(int id,string from , string to, IEnumerable<Faculty> faculties)
        {
            faculties = faculties.Where(f => f.BudgetPlaces >= Double.Parse(from) && f.BudgetPlaces <= Double.Parse(to));
            switch (id)
            {
                case 0:
                    {
                        faculties = faculties.OrderBy(faculty => faculty.BudgetPlaces).ToList();
                        break;
                    }
                case 1:
                    {
                        faculties = faculties.OrderByDescending(faculty => faculty.BudgetPlaces).ToList();
                        break;
                    }
                case 2:
                    {
                        faculties = faculties.OrderBy(faculty => faculty.Name).ToList();
                        break;
                    }
                case 3:
                    {
                        faculties = faculties.OrderByDescending(faculty => faculty.Name).ToList();
                        break;
                    }
                case 4:
                    {
                        faculties = faculties.OrderBy(faculty => faculty.AllPlaces).ToList();
                        break;
                    }
                case 5:
                    {
                        faculties = faculties.OrderByDescending(faculty => faculty.AllPlaces).ToList();
                        break;
                    }
                default: faculties = faculties.OrderByDescending(faculty => faculty.AllPlaces).ToList(); break;
            }

            return (faculties);
        }
    }
}
