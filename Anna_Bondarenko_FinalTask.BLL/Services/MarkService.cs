using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class MarkService : IMarkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MarkService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Mark> GetAll()
        {
                _logger.Info("Getting the all marks");

                var marks = _unitOfWork.MarkGenericRepository.GetAll();

                return marks;
        }

        public Mark Get(int id)
        {
                _logger.Info($"Getting mark by id {id}");

                var mark = _unitOfWork.MarkGenericRepository.Get(id);

                return mark;
        }

        public void Create(Mark mark)
        {
            _unitOfWork.MarkGenericRepository.Create(mark);
        }

        public void Update(Mark mark)
        {
            _unitOfWork.MarkGenericRepository.Update(mark);
        }

        public void Delete(int id)
        {
            _unitOfWork.MarkGenericRepository.Delete(id);
        }

        public IEnumerable<Mark> Find(Func<Mark, bool> parameter)
        {
            return _unitOfWork.MarkGenericRepository.Find(parameter);
        }
    }
}
