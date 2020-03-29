using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IFacultySubjectService
    {
        /// <summary>
        /// Getting all Faculty subject
        /// </summary>
        /// <returns></returns>
        IEnumerable<FacultySubject> GetAll();

        /// <summary>
        /// Get <see cref="FacultySubject"/>
        /// </summary>
        /// <returns></returns>
        FacultySubject Get(int id);

        /// <summary>
        /// Creates new  <see cref="FacultySubject"/>
        /// </summary>
        /// <returns></returns>
        void Create(FacultySubject facultySubject);

        /// <summary>
        /// Creates new  <see cref="FacultySubject"/>
        /// </summary>
        /// <returns></returns>
        void Update(FacultySubject facultySubject);

        /// <summary>
        /// Creates new  <see cref="FacultySubject"/>
        /// </summary>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Finds <see cref="FacultySubject"/>s by parameter
        /// </summary>
        /// <returns></returns>
        IEnumerable<FacultySubject> Find(Func<FacultySubject, bool> parameter);
    }
}
