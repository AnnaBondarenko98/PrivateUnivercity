using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class SchoolSunjectService : ISchoolSubjectsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public SchoolSunjectService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<SchoolSubject> GetAll()
        {
            _logger.Info("Getting all school subjects");

            var subjects = _unitOfWork.SchoolSubjectGenericRepository.GetAll();

            return subjects;
        }

        public SchoolSubject Get(int id)
        {
            _logger.Info($"Getting school subject by id {id}");

            var subject = _unitOfWork.SchoolSubjectGenericRepository.Get(id);

            return subject;
        }

        public void Create(SchoolSubject schoolSubject)
        {
            _logger.Info($"Creating new school subject {schoolSubject.Name}");

            _unitOfWork.SchoolSubjectGenericRepository.Create(schoolSubject);
        }

        public void Update(SchoolSubject schoolSubject)
        {
            _logger.Info($"Updating school subject {schoolSubject.Name}");

            _unitOfWork.SchoolSubjectGenericRepository.Update(schoolSubject);
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting school subject by id {id}");

            _unitOfWork.SchoolSubjectGenericRepository.Delete(id);
        }

        public IEnumerable<SchoolSubject> Find(Func<SchoolSubject, bool> parameter)
        {
            _logger.Info($"Finding subject by parameter");

            var subjects = _unitOfWork.SchoolSubjectGenericRepository.Find(parameter);

            return subjects;
        }
    }
}
