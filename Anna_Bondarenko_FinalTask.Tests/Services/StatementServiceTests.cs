using System;
using System.Collections.Generic;
using System.Linq;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.BLL.Services;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using Moq;
using NLog;
using Xunit;

namespace Anna_Bondarenko_FinalTask.Tests.Services
{
    public class StatementServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMessageSender> _messageSender;
        private readonly Mock<IStudentStatusService> _studentStatusService;
        private readonly Mock<IMarkService> _markService;
        private readonly IStatementService _sut;

        public StatementServiceTests()
        {
            var logger = new Mock<ILogger>();
            _messageSender = new Mock<IMessageSender>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _studentStatusService = new Mock<IStudentStatusService>();
            _markService = new Mock<IMarkService>();

            _sut = new StatementService(_unitOfWorkMock.Object, logger.Object, _messageSender.Object, _studentStatusService.Object,_markService.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var statements = new List<Statement>
            {
                new Statement(),
                new Statement()
            };
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.GetAll()).Returns(statements);

            var result = _sut.GetAll();

            Assert.Equal(statements.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectStatementId()
        {
            var id = 1;
            var statement = new Statement() { Id = id };
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Get(id)).Returns(statement);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Create_Statement_Created()
        {
            var statement = new Statement();
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Create(statement));

            _sut.Create(statement);

            _unitOfWorkMock.Verify(uof => uof.StatementGenericRepository.Create(It.IsAny<Statement>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Create_Statement_CorrectStatementName()
        {
            var name = "name";
            var statement = new Statement();
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Create(statement));

            _sut.Create(statement);

            _unitOfWorkMock.Verify(uof => uof.StatementGenericRepository.Create(It.IsAny<Statement>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Update_Statement_Updated()
        {
            var statement = new Statement();
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Update(statement));

            _sut.Update(statement);

            _unitOfWorkMock.Verify(uof => uof.StatementGenericRepository.Update(It.IsAny<Statement>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Delete_ExistingId_Deleted()
        {
            var id = 1;
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Delete(id));

            _sut.Delete(id);

            _unitOfWorkMock.Verify(uof => uof.StatementGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Find_NotNullPredicate_CorrectQuantity()
        {
            var statements = new List<Statement>()
            {
                new Statement(),
                new Statement()
            };

            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Find(It.IsAny<Func<Statement, bool>>()))
                .Returns(statements);

            var actual = _sut.Find(It.IsAny<Func<Statement, bool>>());

            Assert.Equal(statements.Count, actual.Count());
        }

        [Fact]
        public void Find_Null_Null()
        {
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.Find(null));

            var actual = _sut.Find(It.IsAny<Func<Statement, bool>>());

            Assert.Null(actual);
        }

        [Fact]
        public void DeleteRange_Statement_CorrectStatementName()
        {
            var statements = new List<Statement>()
            {
                new Statement(),
                new Statement()
            };
            _unitOfWorkMock.Setup(uof => uof.StatementGenericRepository.DeleteRange(statements));

            _sut.DeleteRange(statements);

            _unitOfWorkMock.Verify(uof => uof.StatementGenericRepository.DeleteRange(It.IsAny<IEnumerable<Statement>>()), Times.AtLeastOnce);
        }
    }
}