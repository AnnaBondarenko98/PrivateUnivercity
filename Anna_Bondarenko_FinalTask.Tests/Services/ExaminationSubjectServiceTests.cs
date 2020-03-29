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
    public class ExaminationSubjectServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IExaminationSubjectService _sut;

        public ExaminationSubjectServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new ExaminationSubjectService(_unitOfWorkMock.Object, logger.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var examinationSubjects = new List<ExaminationSubject>
            {
                new ExaminationSubject(),
                new ExaminationSubject()
            };
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.GetAll()).Returns(examinationSubjects);

            var result = _sut.GetAll();

            Assert.Equal(examinationSubjects.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectExaminationSubjectId()
        {
            var id = 1;
            var examinationSubject = new ExaminationSubject { Id = id };
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Get(id)).Returns(examinationSubject);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Create_ExaminationSubject_Created()
        {
            var examinationSubject = new ExaminationSubject();
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Create(examinationSubject));

            _sut.Create(examinationSubject);

            _unitOfWorkMock.Verify(uof => uof.ExamGenericRepository.Create(It.IsAny<ExaminationSubject>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Create_Comment_CorrectExaminationSubjectName()
        {
            var name = "name";
            var examinationSubject = new ExaminationSubject(){Name = name };
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Create(examinationSubject));

            _sut.Create(examinationSubject);

            Assert.Equal(name, examinationSubject.Name);
        }

        [Fact]
        public void Update_ExaminationSubject_Updated()
        {
            var examinationSubject = new ExaminationSubject();
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Update(examinationSubject));

            _sut.Update(examinationSubject);

            _unitOfWorkMock.Verify(uof => uof.ExamGenericRepository.Update(It.IsAny<ExaminationSubject>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Delete_ExistingId_Deleted()
        {
            var id = 1;
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Delete(id));

            _sut.Delete(id);

            _unitOfWorkMock.Verify(uof => uof.ExamGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Find_NotNullPredicate_CorrectQuantity()
        {
            var examinationSubjects = new List<ExaminationSubject>
            {
                new ExaminationSubject(),
                new ExaminationSubject()
            };
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Find(It.IsAny<Func<ExaminationSubject, bool>>())).Returns(examinationSubjects);

            var actual = _sut.Find(It.IsAny<Func<ExaminationSubject, bool>>());

            Assert.Equal(examinationSubjects.Count, actual.Count());
        }

        [Fact]
        public void Find_Null_Null()
        {
            _unitOfWorkMock.Setup(uof => uof.ExamGenericRepository.Find(null));

            var actual = _sut.Find(It.IsAny<Func<ExaminationSubject, bool>>());

            Assert.Null(actual);
        }

    }
}