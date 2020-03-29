using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IOperatorService:IDisposable
    {
        /// <summary>
        /// Getting all Operators
        /// </summary>
        /// <returns></returns>
        IEnumerable<Operator> GetAll();

        /// <summary>
        /// Get <see cref="Operator"/> by id
        /// </summary>
        /// <returns></returns>
        Operator Get(int id);

        /// <summary>
        /// Creates new <see cref="Operator"/>
        /// </summary>
        /// <returns></returns>
        Task<bool> Create(OperatorDto operatirDto);

        /// <summary>
        /// Authenticates Operator with <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <returns></returns>
        Task<ClaimsIdentity> Authenticate(LoginDto loginDto);

        /// <summary>
        /// Updates the <see cref="Operator"/> by id
        /// </summary>
        /// <returns></returns>
        void Update(Operator operatirModel);

        /// <summary>
        /// Deletes the <see cref="Operator"/> by id
        /// </summary>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Adding lock or unlock Role to User
        /// </summary>
        /// <param name="enrollee">Enrollee</param>
        /// <returns></returns>
        void AddLockRole(Enrollee enrollee);
    }
}
