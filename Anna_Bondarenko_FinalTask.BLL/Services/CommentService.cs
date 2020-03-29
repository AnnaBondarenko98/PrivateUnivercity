using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{

    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CommentService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Comment> GetAll()
        {
            _logger.Info("Getting all comments");

            var comments = _unitOfWork.CommentGenericRepository.GetAll();

            return comments;
        }

        public Comment Get(int id)
        {
            _logger.Info($"Getting comment by id {id}");

            var comment = _unitOfWork.CommentGenericRepository.Get(id);

            return comment;
        }

        public void Create(Comment comment, string name)
        {
            _logger.Info("Creating new comment ");
            comment.Date = DateTime.UtcNow;
            comment.Name = name;
            _unitOfWork.CommentGenericRepository.Create(comment);
        }

        public void Update(Comment comment)
        {
            _logger.Info("Upadting comment");

            _unitOfWork.CommentGenericRepository.Update(comment);
        }

        public void Delete(int id)
        {
            _logger.Info("Deleting comment");

            _unitOfWork.CommentGenericRepository.Delete(id);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> parameter)
        {
            _logger.Info("Finding the comment");

            return _unitOfWork.CommentGenericRepository.Find(parameter);

        }
    }
}
