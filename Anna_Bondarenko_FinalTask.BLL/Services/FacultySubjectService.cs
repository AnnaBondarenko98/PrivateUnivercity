using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class FacultySubjectService : IFacultySubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public FacultySubjectService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<FacultySubject> GetAll()
        {
            _logger.Info("Gettting all faculty subjects  ");

            var subjects = _unitOfWork.FacultySubjectGenericRepository.GetAll();

            return subjects;
        }

        public FacultySubject Get(int id)
        {
            _logger.Info($"Getting faculty subject by id {id}");

            var subjects = _unitOfWork.FacultySubjectGenericRepository.Get(id);

            return subjects;
        }

        public void Create(FacultySubject facultySubject)
        {
            _logger.Info($"Creating new faculry subject {facultySubject.Name}");

            _unitOfWork.FacultySubjectGenericRepository.Create(facultySubject);
        }

        public void Update(FacultySubject facultySubject)
        {
            _logger.Info($"Updating faculty subject {facultySubject.Name}, Id: {facultySubject.Id} ");

            _unitOfWork.FacultySubjectGenericRepository.Update(facultySubject);
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting faculty subject  by id  {id}");

            _unitOfWork.FacultySubjectGenericRepository.Delete(id);
        }

        public IEnumerable<FacultySubject> Find(Func<FacultySubject, bool> parameter)
        {
            _logger.Info($"Finding faculty subject  by parameter ");

            var subjects = _unitOfWork.FacultySubjectGenericRepository.Find(parameter);

            return subjects;
        }
    }
}
