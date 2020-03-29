using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
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
    public class EnrolleeServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMessageSender> _messageSender;
        private readonly IEnrolleeService _sut;
        private readonly Mock<IMapper> _mapper;


        public EnrolleeServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _messageSender = new Mock<IMessageSender>();
            _mapper = new Mock<IMapper>();

            _sut = new EnrolleeService(_unitOfWorkMock.Object, logger.Object, _messageSender.Object,_mapper.Object);
        }

        [Fact]
        public void GetAll_CorrectQuantiity()
        {
            var enrollees = new List<Enrollee>
            {
                new Enrollee(),
                new Enrollee()
            };
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.GetAll()).Returns(enrollees);

            var result = _sut.GetAll();

            Assert.Equal(enrollees.Count, result.Count());
        }

        [Fact]
        public void Get_ExistingId_CorrectEnrolee()
        {
            var id = 1;
            var name = "name";
            var enrollee = new Enrollee() { Id = id, FirstName = name };
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.Get(id)).Returns(enrollee);

            var result = _sut.Get(id);

            Assert.Equal(name, result.FirstName);
        }

        [Fact]
        public void Get_NotExistingId_Null()
        {
            var id = 0;
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.Get(id));

            var result = _sut.Get(id);

            Assert.Null(result);
        }

        [Fact]
        public void Update_Enrollee_Updated()
        {
            var enrollee = new Enrollee();
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.Update(enrollee));

            _sut.Update(enrollee);

            _unitOfWorkMock.Verify(uof => uof.EnrolleeGenericRepository.Update(It.IsAny<Enrollee>()), Times.AtLeastOnce);
        }

        [Fact]
        public void Delete_ExistingId_Deleted()
        {
            var id = 1;
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.Delete(id));

            _sut.Delete(id);

            _unitOfWorkMock.Verify(uof => uof.EnrolleeGenericRepository.Delete(It.IsAny<int>()), Times.AtLeastOnce);
        }

        [Fact]
        public void SendMessage_ExistingMessage()
        {
            _messageSender.Setup(uof => uof.SendToUs(It.IsAny<MessageDto>()));

            _sut.SendMessage(It.IsAny<MessageDto>());

            _messageSender.Verify(uof => uof.SendToUs(It.IsAny<MessageDto>()), Times.AtLeastOnce);
        }

        [Fact]
        public void VerifyImage_ExistingEnrolle()
        {
            var enrolee = new Enrollee();
            var image = new Mock<HttpPostedFileBase>();
            image.SetupGet(file => file.ContentType).Returns("jpg");
            image.SetupGet(file => file.ContentLength).Returns(0);
            image.Setup(file => file.InputStream).Returns(new FileStream("path", FileMode.OpenOrCreate, FileAccess.Read));

            _sut.VerifyImage(enrolee, image.Object);

            Assert.Equal(image.Object.ContentType, enrolee.ImageMimeType);
        }

      
        [Fact]
        public void Find_NotNullPredicate_CorrectQuantity()
        {
            var enrollees = new List<Enrollee>()
            {
                new Enrollee(),
                new Enrollee()
            };
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.Find(It.IsAny<Func<Enrollee, bool>>())).Returns(enrollees);

            var actual = _sut.Find(It.IsAny<Func<Enrollee, bool>>());

            Assert.Equal(enrollees.Count, actual.Count());
        }

        [Fact]
        public void Find_Null_Null()
        {
            _unitOfWorkMock.Setup(uof => uof.EnrolleeGenericRepository.Find(null));

            var actual = _sut.Find(It.IsAny<Func<Enrollee, bool>>());

            Assert.Null(actual);
        }

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

        [Fact]
        public void Authenticate_ExistingLockedUser()
        {
            var email = "email@email.email";
            var expected = "Locked";
            var loginDto = new LoginDto { Email = email };
            _unitOfWorkMock.Setup(uof => uof.FacultyGenericRepository.Find(null));
            var user = new IdentityUser() { Email = email, LockoutEndDateUtc = DateTime.Now.AddDays(1)};
            var claim = new ClaimsIdentity() { Label = email };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            userManager.Setup(_ => _.FindAsync(email, It.IsAny<string>())).Returns(Task.FromResult(user));
            userManager.Setup(_ => _.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)).Returns(Task.FromResult(claim));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            var actual = _sut.Authenticate(loginDto);

            Assert.Equal(expected, actual.Result.Label);
        }
    }
}