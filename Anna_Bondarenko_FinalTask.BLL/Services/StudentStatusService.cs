using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class StudentStatusService : IStudentStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public StudentStatusService(IUnitOfWork unitOfWork, ILogger logger, IMessageSender messageSender)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<StudentStatus> GetAll()
        {
            return _unitOfWork.StudentStatusRepository.GetAll();
        }

        public StudentStatus Get(int id)
        {
            return _unitOfWork.StudentStatusRepository.Get(id);
        }

        public void Create(StudentStatus status)
        {
            _unitOfWork.StudentStatusRepository.Create(status);
        }

        public void Update(StudentStatus status)
        {
            _unitOfWork.StudentStatusRepository.Update(status);
        }

        public void Delete(int id)
        {
            _unitOfWork.StudentStatusRepository.Delete(id);
        }

        public IEnumerable<StudentStatus> Find(Func<StudentStatus, bool> parameter)
        {
            return _unitOfWork.StudentStatusRepository.Find(parameter);
        }
    }
}
