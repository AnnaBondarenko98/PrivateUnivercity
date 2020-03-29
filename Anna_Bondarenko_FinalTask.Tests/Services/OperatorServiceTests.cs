using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Services;
using Anna_Bondarenko_FinalTask.DAL.Identity;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using NLog;
using Xunit;

namespace Anna_Bondarenko_FinalTask.Tests.Services
{
    public class OperatorServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IOperatorService _sut;
        private readonly Mock<IMapper> _mapper;


        public OperatorServiceTests()
        {
            var logger = new Mock<ILogger>();
            _mapper = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new OperatorService(_unitOfWorkMock.Object, logger.Object,_mapper.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var operators = new List<Operator>
            {
                new Operator(),
                new Operator()
            };
            _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.GetAll()).Returns(operators);

            var result = _sut.GetAll();

            Assert.Equal(operators.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectOperatorId()
        {
            var id = 1;
            var _operator = new Operator { Id = id };
            _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.Get(id)).Returns(_operator);

            var result = _sut.Get(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Create_Operator_CorrectOperatorName()
        {
            var name = "name";
            var _operator = new Operator() { Name = name };
            var operatorDto = new OperatorDto() { Name = name };
            _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.Create(_operator));

            _sut.Create(operatorDto);

            Assert.Equal(name, _operator.Name);
        }

        [Fact]
        public void Update_Operator_Updated()
        {
            var _operator = new Operator();
            var operators = new List<Operator>() { _operator };
            _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.Find(It.IsAny<Func<Operator, bool>>())).Returns(operators);
            _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.Update(_operator));

            _sut.Update(_operator);

            _unitOfWorkMock.Verify(uof => uof.OperatorGenericRepository.Update(It.IsAny<Operator>()), Times.AtLeastOnce);
        }

        //[Fact]
        //public void Delete_ExistingId_Deleted()
        //{
        //    var id = 1;
        //    _unitOfWorkMock.Setup(uof => uof.OperatorGenericRepository.Delete(id));

        //    _sut.Delete(id);

        //    _unitOfWorkMock.Verify(uof => uof.OperatorGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        //}

        [Fact]
        public void Authenticate_ExistingUser()
        {
            var email = "email@email.email";
            var loginDto = new LoginDto { Email = email };
            _unitOfWorkMock.Setup(uof => uof.FacultyGenericRepository.Find(null));
            var user = new IdentityUser() { Email = email };
            var claim = new ClaimsIdentity() { Label = email };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            userManager.Setup(_ => _.FindAsync(email, It.IsAny<string>())).Returns(Task.FromResult(user));
            userManager.Setup(_ => _.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)).Returns(Task.FromResult(claim));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            var actual = _sut.Authenticate(loginDto);

            Assert.Equal(email, actual.Result.Label);
        }
    }
}