using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Interfaces.MessageSenderInterface;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class EnrolleeService : IEnrolleeService
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageSender _messageSender;
        private readonly IMapper _mapper;

        public EnrolleeService(IUnitOfWork unitOfWork, ILogger logger, IMessageSender messageSender,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _messageSender = messageSender;
            _mapper = mapper;
        }

        public IEnumerable<Enrollee> GetAll()
        {
            _logger.Info($"Getting all enrolees.");

            var enrolees = _unitOfWork.EnrolleeGenericRepository.GetAll();

            return enrolees;
        }

        public Enrollee Get(int id)
        {
            _logger.Info($"Getting enrollee by id {id}");

            var enrolee = _unitOfWork.EnrolleeGenericRepository.Get(id);

            return enrolee;
        }

        public async Task<bool> Create(EnrolleeDto enrolleeDto)
        {
            _logger.Info($"Finding by Email {enrolleeDto.Email} an enrolee. ");

            var user = await _unitOfWork.UserManager.FindByEmailAsync(enrolleeDto.Email);

            if (user != null)
            {
                return false;
            }

            user = new IdentityUser { Email = enrolleeDto.Email, UserName = enrolleeDto.Email };

            var result = await _unitOfWork.UserManager.CreateAsync(user, enrolleeDto.Password);

            if (result.Errors.Count() != 0)
            {
                throw new Exception();
            }

            _logger.Info($"Adding the role to new enrolee.");

            await _unitOfWork.UserManager.AddToRoleAsync(user.Id, "User");

            var profile = _mapper.Map<Enrollee>(enrolleeDto);
            profile.AppCustomer = user;
            profile.Lock = false;

            _unitOfWork.EnrolleeGenericRepository.Create(profile);

            return true;
        }

        public void Update(Enrollee enrolle)
        {
            _logger.Info($"Updating the enrollee {enrolle.Email}");

            _unitOfWork.EnrolleeGenericRepository.Update(enrolle);
        }

        public async Task<ClaimsIdentity> Authenticate(LoginDto loginDto)
        {
            ClaimsIdentity claim = new ClaimsIdentity();
            _logger.Info($"Authentication the enrolle {loginDto.Email}");

            var user = await _unitOfWork.UserManager.FindAsync(loginDto.Email, loginDto.Password);

            if (user != null)
            {
                if (user.LockoutEndDateUtc > DateTime.UtcNow)
                {
                    claim.Label = "Locked";
                }
                else
                {
                    claim = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                }
            }

            return claim;
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting enrollee by id {id}");

            _unitOfWork.EnrolleeGenericRepository.Delete(id);
        }

        public IEnumerable<Enrollee> Find(Func<Enrollee, bool> predicate)
        {
            _logger.Info("Finding enrolees by parameter");

            var enrollees = _unitOfWork.EnrolleeGenericRepository.Find(predicate);

            return enrollees;
        }

        public void SendMessage(MessageDto sendMessage)
        {
            _messageSender.SendToUs(sendMessage);
        }

        public IEnumerable<Enrollee> GetEnrolleWithoutAddedFaculty()
        {
            var allEnrollees = _unitOfWork.EnrolleeGenericRepository.GetAll().ToList();

            var someEnr = new List<Enrollee>();

            foreach (var item in allEnrollees)
            {
                someEnr=allEnrollees.Where(e => e.StudentStatuses.Any(s => s.FacultyStatus == false)).ToList();
            }

            return someEnr;
        }

        public Enrollee VerifyImage(Enrollee enrollee, HttpPostedFileBase image)
        {
            enrollee.ImageMimeType = image.ContentType;
            enrollee.ImageData = new byte[image.ContentLength];
            image.InputStream.Read(enrollee.ImageData, 0, image.ContentLength);

            return enrollee;
        }

        public void Dispose()
        {
        }
    }
}
