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
    public class FacultySubjectServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IFacultySubjectService _sut;

        public FacultySubjectServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new FacultySubjectService(_unitOfWorkMock.Object, logger.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var facultySubjects = new List<FacultySubject>
            {
                new FacultySubject(),
                new FacultySubject()
            };
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.GetAll()).Returns(facultySubjects);

            var result = _sut.GetAll();

            Assert.Equal(facultySubjects.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectFacultySubjectId()
        {
            var id = 1;
            var facultySubject = new FacultySubject() { Id = id };
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Get(id)).Returns(facultySubject);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Create_FacultySubject_Created()
        {
            var facultySubject = new FacultySubject();
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Create(facultySubject));

            _sut.Create(facultySubject);

            _unitOfWorkMock.Verify(uof => uof.FacultySubjectGenericRepository.Create(It.IsAny<FacultySubject>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Create_FacultySubject_CorrectFacultySubjectName()
        {
            var name = "name";
            var facultySubject = new FacultySubject() { Name = name };
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Create(facultySubject));

            _sut.Create(facultySubject);

            Assert.Equal(name, facultySubject.Name);
        }

        [Fact]
        public void Update_FacultySubject_Updated()
        {
            var facultySubject = new FacultySubject();
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Update(facultySubject));

            _sut.Update(facultySubject);

            _unitOfWorkMock.Verify(uof => uof.FacultySubjectGenericRepository.Update(It.IsAny<FacultySubject>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Delete_ExistingId_Deleted()
        {
            var id = 1;
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Delete(id));

            _sut.Delete(id);

            _unitOfWorkMock.Verify(uof => uof.FacultySubjectGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Find_NotNullPredicate_CorrectQuantity()
        {
            var facultySubjects = new List<FacultySubject>
            {
                new FacultySubject(),
                new FacultySubject()
            };
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Find(It.IsAny<Func<FacultySubject, bool>>())).Returns(facultySubjects);

            var actual = _sut.Find(It.IsAny<Func<FacultySubject, bool>>());

            Assert.Equal(facultySubjects.Count, actual.Count());
        }

        [Fact]
        public void Find_Null_Null()
        {
            _unitOfWorkMock.Setup(uof => uof.FacultySubjectGenericRepository.Find(null));

            var actual = _sut.Find(It.IsAny<Func<FacultySubject, bool>>());

            Assert.Null(actual);
        }
    }
}