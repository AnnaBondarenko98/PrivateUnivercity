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
    public class MarkServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMarkService _sut;

        public MarkServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new MarkService(_unitOfWorkMock.Object, logger.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var marks = new List<Mark>
            {
                new Mark(),
                new Mark()
            };
            _unitOfWorkMock.Setup(uof => uof.MarkGenericRepository.GetAll()).Returns(marks);

            var result = _sut.GetAll();

            Assert.Equal(marks.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectMarkId()
        {
            var id = 1;
            var mark = new Mark { Id = id };
            _unitOfWorkMock.Setup(uof => uof.MarkGenericRepository.Get(id)).Returns(mark);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.MarkGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }
    }
}