using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IEnrolleeService:IDisposable
    {
        /// <summary>
        /// Getting all enrollees
        /// </summary>
        /// <returns>Collection of Enrollee</returns>
        IEnumerable<Enrollee> GetAll();

        /// <summary>
        /// Get <see cref="Enrollee"/> by id
        /// </summary>
        /// <param name="id">Enrollee id</param>
        /// <returns>Enrollee</returns>
        Enrollee Get(int id);

        /// <summary>
        /// Creates new <see cref="Enrollee"/>
        /// </summary>
        /// ///<param name="enrollee">Enrollee for creating</param>
        /// <returns>Bool result of creating</returns>
        Task<bool> Create(EnrolleeDto enrollee);

        /// <summary>
        /// Updates new  <see cref="Enrollee"/>
        /// </summary>
        /// <param name="enrollee">Enrollee for updating</param>
        void Update(Enrollee enrollee);

        /// <summary>
        /// Authenticates Enrollee with <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <param name="loginDto">login & password</param>
        Task<ClaimsIdentity> Authenticate(LoginDto loginDto);

        /// <summary>
        /// Deletes new <see cref="Enrollee"/>
        /// </summary>
        /// <param name="id">Enrollee id</param>
        void Delete(int id);

        /// <summary>
        ///Finds collection of <see cref="Enrollee"/> by parameter
        /// </summary>
        /// <returns>Collection of Enrollee</returns>
        IEnumerable<Enrollee> Find(Func<Enrollee, bool> predicate);

        /// <summary>
        ///Sends a message
        /// </summary>
        /// <param name="sendMessage">Model for sending</param>
        void SendMessage(MessageDto sendMessage);

        /// <summary>
        ///Gets all enrollees without added faculties
        /// </summary>
        /// <returns>Collection of Enrollee</returns>
        IEnumerable<Enrollee> GetEnrolleWithoutAddedFaculty();

        /// <summary>
        ///Verifying the image
        /// </summary>
        ///<param name="enrollee">Enrollee</param>
        /// <param name="image">Image for verifying</param>
        /// <returns>Enrollee</returns>
        Enrollee VerifyImage(Enrollee enrollee, HttpPostedFileBase image);

    }
}
