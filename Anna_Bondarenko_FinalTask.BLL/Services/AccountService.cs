using System;
using System.Threading.Tasks;
using System.Web;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMessageSender _messageSender;

        public AccountService(IUnitOfWork unitOfWork, ILogger logger, IMessageSender messageSender)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _messageSender = messageSender;
        }

        public async Task<UserForgotCode> GenerateForgotCode(ForgotPasswordDto model)
        {
            _logger.Info($"Try to generate the link for {model.Email}");

            var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return null;
            }

            var forgotCode = new UserForgotCode
            {
                Id = user.Id,
                Code = await _unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user.Id)
            };

            return forgotCode;
        }

        public void SendForgotLink(string callbackUrl, string email)
        {
            _messageSender.SendForgotLink(callbackUrl, email);

            _logger.Info($"Sending the link to {email} ");
        }

        public async Task<bool> ResetPassword(ResetPassword model)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return false;
            }

            var result = await _unitOfWork.UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

            if (result.Succeeded)
            {
                _logger.Info($"The password for  {model.Email} was reseted");

                return true;
            }

            throw new Exception("Cannot change the password");
        }

        public EnrolleeDto VerifyImage(EnrolleeDto enrolleeDto, HttpPostedFileBase image)
        {

            _logger.Info($"Verify image for {enrolleeDto.Email}");

            enrolleeDto.ImageMimeType = image.ContentType;
            enrolleeDto.ImageData = new byte[image.ContentLength];
            image.InputStream.Read(enrolleeDto.ImageData, 0, image.ContentLength);

            return enrolleeDto;
        }
    }
}
