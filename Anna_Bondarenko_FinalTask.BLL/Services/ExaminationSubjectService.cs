using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class ExaminationSubjectService : IExaminationSubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ExaminationSubjectService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<ExaminationSubject> GetAll()
        {
            _logger.Info("Getting all examination subjects ");

            var subjects = _unitOfWork.ExamGenericRepository.GetAll();

            return subjects;
        }

        public ExaminationSubject Get(int id)
        {
            _logger.Info($"Getting examination subgect by id {id}");

            var subject = _unitOfWork.ExamGenericRepository.Get(id);

            return subject;
        }

        public void Create(ExaminationSubject examSubject)
        {
            _logger.Info($"Creating new examination subject {examSubject}");

            _unitOfWork.ExamGenericRepository.Create(examSubject);
        }

        public void Update(ExaminationSubject examSubject)
        {
            _logger.Info($"Updating examination subject {examSubject}");

            _unitOfWork.ExamGenericRepository.Update(examSubject);
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting examination subject by id {id}");

            _unitOfWork.ExamGenericRepository.Delete(id);
        }

        public IEnumerable<ExaminationSubject> Find(Func<ExaminationSubject, bool> parameter)
        {
            _logger.Info($"Finding examination subject by parameter ");

            var subjects = _unitOfWork.ExamGenericRepository.Find(parameter);

            return subjects;
        }
    }
}
