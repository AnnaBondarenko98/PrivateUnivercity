using System;
using System.Collections.Generic;
using System.Linq;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Services;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using Moq;
using NLog;
using Xunit;

namespace Anna_Bondarenko_FinalTask.Tests.Services
{
    public class CommentServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ICommentService _sut;

        public CommentServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new CommentService(_unitOfWorkMock.Object, logger.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var comments = new List<Comment>
            {
                new Comment(),
                new Comment()
            };
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.GetAll()).Returns(comments);

            var result = _sut.GetAll();

            Assert.Equal(comments.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectCommentId()
        {
            var id = 1;
            var comment = new Comment { Id = id };
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Get(id)).Returns(comment);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Create_Comment_Created()
        {
            var comment = new Comment();
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Create(comment));

            _sut.Create(comment, It.IsNotNull<string>());

            _unitOfWorkMock.Verify(uof => uof.CommentGenericRepository.Create(It.IsAny<Comment>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Create_Comment_CorrectCommentName()
        {
            var name = "name";
            var comment = new Comment();
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Create(comment));

            _sut.Create(comment, name);

            Assert.Equal(name, comment.Name);
        }

        [Fact]
        public void Update_Comment_Updated()
        {
            var comment = new Comment();
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Update(comment));

            _sut.Update(comment);

            _unitOfWorkMock.Verify(uof => uof.CommentGenericRepository.Update(It.IsAny<Comment>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Delete_ExistingId_Deleted()
        {
            var id = 1;
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Delete(id));

            _sut.Delete(id);

            _unitOfWorkMock.Verify(uof => uof.CommentGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Find_NotNullPredicate_CorrectQuantity()
        {
            var comments = new List<Comment>()
            {
                new Comment(),
                new Comment()
            };
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Find(It.IsAny<Func<Comment, bool>>())).Returns(comments);

            var actual = _sut.Find(It.IsAny<Func<Comment,bool>>());

            Assert.Equal(comments.Count, actual.Count());
        }

        [Fact]
        public void Find_Null_Null()
        {
            _unitOfWorkMock.Setup(uof => uof.CommentGenericRepository.Find(null));

            var actual = _sut.Find(It.IsAny<Func<Comment, bool>>());

            Assert.Null(actual);
        }
    }
}