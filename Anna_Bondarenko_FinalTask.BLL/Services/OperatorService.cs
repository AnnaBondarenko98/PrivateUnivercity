using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NLog;

namespace Anna_Bondarenko_FinalTask.BLL.Services
{
    public class OperatorService : IOperatorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;



        public OperatorService(IUnitOfWork unitOfWork, ILogger logger,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<Operator> GetAll()
        {
            _logger.Info("Finding the  all operators");

            var operators = _unitOfWork.OperatorGenericRepository.GetAll();

            return operators;
        }

        public Operator Get(int id)
        {
            _logger.Info($"Getting operator by id {id}");

            var operatorModel = _unitOfWork.OperatorGenericRepository.Get(id);

            return operatorModel;
        }

        public async Task<bool> Create(OperatorDto operatorDto)
        {
            _logger.Info($"Fingding by Email an operator {operatorDto.Email} ");

            var user = await _unitOfWork.UserManager.FindByEmailAsync(operatorDto.Email);

            if (user != null)
            {
                return false;
            }

            user = new IdentityUser { Email = operatorDto.Email, UserName = operatorDto.Email };

            var result = await _unitOfWork.UserManager.CreateAsync(user, operatorDto.Password);

            if (result.Errors.Count() != 0)
            {
                throw new Exception();
            }

            _logger.Info($"Adding the role to new operator.");

            await _unitOfWork.UserManager.AddToRoleAsync(user.Id, "Operator");

            var profile = _mapper.Map<Operator>(operatorDto);
            profile.AppCustomer = user;

            _unitOfWork.OperatorGenericRepository.Create(profile);

            return true;
        }

        public async Task<ClaimsIdentity> Authenticate(LoginDto loginDto)
        {
            ClaimsIdentity claim = null;

            _logger.Info($"Authenticating the operator {loginDto.Email}");

            var user = await _unitOfWork.UserManager.FindAsync(loginDto.Email, loginDto.Password);

            if (user != null)
            {
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claim;
        }

        public void Update(Operator operatorModel)
        {
            _logger.Info($"Updating operator {operatorModel.Name}");

            _unitOfWork.OperatorGenericRepository.Update(operatorModel);
        }

        public void Delete(int id)
        {
            _logger.Info($"Deleting operator by id {id}");

            var operat = _unitOfWork.OperatorGenericRepository.Get(id);

            var user =  _unitOfWork.UserManager.FindByEmail(operat.Email);

            _unitOfWork.UserManager.Delete(user);

            _unitOfWork.OperatorGenericRepository.Delete(id);
        }

        public void Dispose()
        {
        }

        public void  AddLockRole(Enrollee enrollee)
        {
            _logger.Info("Adding lock or unlock role to the user ");

            _unitOfWork.UserManager.SetLockoutEnabled(enrollee.AppCustomer.Id,true);

            if (_unitOfWork.UserManager.IsLockedOut(enrollee.AppCustomer.Id))
            {
                enrollee.Lock = false;

                _unitOfWork.UserManager.SetLockoutEndDate(enrollee.AppCustomer.Id, DateTimeOffset.Now);
            }
            else
            {
                enrollee.Lock = true;

                _unitOfWork.UserManager.SetLockoutEndDate(enrollee.AppCustomer.Id,DateTimeOffset.MaxValue);
            }

            _unitOfWork.EnrolleeGenericRepository.Update(enrollee);
        }
    }
}
