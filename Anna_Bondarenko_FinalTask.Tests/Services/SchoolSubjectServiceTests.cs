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

namespace Anna_Bondarenko_FinalTask.BLL.Tests.Services
{
    public class SchoolSubjectServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ISchoolSubjectsService _sut;

        public SchoolSubjectServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new SchoolSunjectService(_unitOfWorkMock.Object, logger.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var schoolSubjects = new List<SchoolSubject>
            {
                new SchoolSubject(),
                new SchoolSubject()
            };
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.GetAll()).Returns(schoolSubjects);

            var result = _sut.GetAll();

            Assert.Equal(schoolSubjects.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectSchoolSubjectId()
        {
            var id = 1;
            var schoolSubject = new SchoolSubject() { Id = id };
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Get(id)).Returns(schoolSubject);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Create_SchoolSubject_Created()
        {
            var schoolSubject = new SchoolSubject();
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Create(schoolSubject));

            _sut.Create(schoolSubject);

            _unitOfWorkMock.Verify(uof => uof.SchoolSubjectGenericRepository.Create(It.IsAny<SchoolSubject>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Create_SchoolSubject_CorrectSchoolSubjectName()
        {
            var name = "name";
            var schoolSubject = new SchoolSubject() { Name = name };
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Create(schoolSubject));

            _sut.Create(schoolSubject);

            Assert.Equal(name, schoolSubject.Name);
        }

        [Fact]
        public void Update_SchoolSubject_Updated()
        {
            var schoolSubject = new SchoolSubject();
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Update(schoolSubject));

            _sut.Update(schoolSubject);

            _unitOfWorkMock.Verify(uof => uof.SchoolSubjectGenericRepository.Update(It.IsAny<SchoolSubject>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Delete_ExistingId_Deleted()
        {
            var id = 1;
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Delete(id));

            _sut.Delete(id);

            _unitOfWorkMock.Verify(uof => uof.SchoolSubjectGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Find_NotNullPredicate_CorrectQuantity()
        {
            var schoolSubjects = new List<SchoolSubject>
            {
                new SchoolSubject(),
                new SchoolSubject()
            };
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Find(It.IsAny<Func<SchoolSubject, bool>>())).Returns(schoolSubjects);

            var actual = _sut.Find(It.IsAny<Func<SchoolSubject, bool>>());

            Assert.Equal(schoolSubjects.Count, actual.Count());
        }

        [Fact]
        public void Find_Null_Null()
        {
            _unitOfWorkMock.Setup(uof => uof.SchoolSubjectGenericRepository.Find(null));

            var actual = _sut.Find(It.IsAny<Func<SchoolSubject, bool>>());

            Assert.Null(actual);
        }
    }
}