using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.BLL.Services;
using Anna_Bondarenko_FinalTask.DAL.Identity;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using NLog;
using Xunit;

namespace Anna_Bondarenko_FinalTask.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMessageSender> _messageSenderMock;

        private readonly IAccountService _sut;

        public AccountServiceTests()
        {
            var logger = new Mock<ILogger>();

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _messageSenderMock = new Mock<IMessageSender>();

            _sut = new AccountService(_unitOfWorkMock.Object, logger.Object, _messageSenderMock.Object);
        }

        [Fact]
        public void GenerateForgotCode_ForgotPasswordDto_EqualId()
        {
            var email = "email@email.email";
            var id = "1";
            var forgotPasswordDto = new ForgotPasswordDto { Email = email };
            var user = new IdentityUser() { Email = email, Id = id };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            userManager.Setup(_ => _.FindByEmailAsync(email)).Returns(Task.FromResult(user));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            var actual = _sut.GenerateForgotCode(forgotPasswordDto);

            Assert.Equal(id, actual.Result.Id);
        }

        [Fact]
        public void GenerateForgotCode_ForgotPasswordDto_UserNotFound()
        {
            var email = "email@email.email";
            var id = "1";
            var forgotPasswordDto = new ForgotPasswordDto { Email = email };
            var user = new IdentityUser() { Email = email, Id = id };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            userManager.Setup(_ => _.FindByEmailAsync(email)).Returns(Task.FromResult<IdentityUser>(null));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            var actual = _sut.GenerateForgotCode(forgotPasswordDto);

            Assert.Null(actual.Result);
        }

        [Fact]
        public void ResetPassword_ResetPasswordModel_SuccessfulyReseted()
        {
            var email = "email@email.email";
            var id = "1";
            var resetPassword = new ResetPassword { Email = email };
            var user = new IdentityUser() { Email = email, Id = id };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            var identityResult = IdentityResult.Success;
            userManager.Setup(_ => _.FindByEmailAsync(email)).Returns(Task.FromResult(user));
            userManager.Setup(_ => _.ResetPasswordAsync(id, It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(identityResult));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            var actual = _sut.ResetPassword(resetPassword);

            Assert.True(actual.Result);
        }

        [Fact]
        public async Task ResetPassword_ResetPasswordModel_Exception()
        {
            var email = "email@email.email";
            var id = "1";
            var resetPassword = new ResetPassword { Email = email };
            var user = new IdentityUser() { Email = email, Id = id };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            var identityResult = IdentityResult.Failed(null);
            userManager.Setup(_ => _.FindByEmailAsync(email)).Returns(Task.FromResult(user));
            userManager.Setup(_ => _.ResetPasswordAsync(id, It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(identityResult));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            await Assert.ThrowsAsync<Exception>(()=>_sut.ResetPassword(resetPassword));
        }

        [Fact]
        public void ResetPassword_ResetPasswordModel_UserNotFound()
        {
            var email = "email@email.email";
            var resetPassword = new ResetPassword { Email = email };
            var userStore = Mock.Of<IUserStore<IdentityUser>>();
            var userManager = new Mock<ApplicationUserManager>(userStore);
            userManager.Setup(_ => _.FindByEmailAsync(email)).Returns(Task.FromResult<IdentityUser>(null));
            _unitOfWorkMock.SetupGet(uof => uof.UserManager).Returns(userManager.Object);

            var actual = _sut.ResetPassword(resetPassword);

            Assert.False(actual.Result);
        }

        [Fact]
        public void SendForgotLink_ExistingCallbackUrlAndEmail_()
        {
            var callbackUrl = "callbackUrl";
            var email = "email";
            _messageSenderMock.Setup(uof => uof.SendForgotLink(callbackUrl, email));

            _sut.SendForgotLink(callbackUrl, email);

            _messageSenderMock.Verify(uof => uof.SendForgotLink(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void VerifyImage_ExistingEnrolle()
        {
            var enrolee = new EnrolleeDto();
            var image = new Mock<HttpPostedFileBase>();
            image.SetupGet(file => file.ContentType).Returns("jpg");
            image.SetupGet(file => file.ContentLength).Returns(1);
            image.Setup(file => file.InputStream).Returns(new FileStream("path", FileMode.OpenOrCreate, FileAccess.Read));

            _sut.VerifyImage(enrolee, image.Object);

            Assert.Equal(image.Object.ContentType, enrolee.ImageMimeType);
        }
    }
}