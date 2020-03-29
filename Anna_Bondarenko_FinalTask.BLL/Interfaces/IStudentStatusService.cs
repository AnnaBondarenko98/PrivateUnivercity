using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IStudentStatusService
    {
        /// <summary>
        /// Getting all statuses
        /// </summary>
        /// <returns>Status collection</returns>
        IEnumerable<StudentStatus> GetAll();

        /// <summary>
        /// Get <see cref="StudentStatus"/>
        /// </summary>
        /// <returns>Status</returns>
        StudentStatus Get(int id);

        /// <summary>
        /// Creates new  <see cref="StudentStatus"/>
        /// </summary>
        /// <param name="status">Status for creating</param>
        void Create(StudentStatus status);

        /// <summary>
        /// Updates the <see cref="StudentStatus"/>
        /// </summary>
        /// <param name="status">Status for updating</param>
        void Update(StudentStatus status);

        /// <summary>
        /// Deletes new  <see cref="StudentStatus"/>
        /// </summary>
        /// <param name="id">Status id</param>
        void Delete(int id);

        /// <summary>
        /// Finds  <see cref="StudentStatus"/>s by parameter
        /// </summary>
        /// <param name="parameter">parameter for searching</param>
        /// <returns>Collection of Comments</returns>
        IEnumerable<StudentStatus> Find(Func<StudentStatus, bool> parameter);
    }
}