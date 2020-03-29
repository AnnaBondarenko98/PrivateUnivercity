using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface ISchoolSubjectsService
    {
        /// <summary>
        /// Getting all school subjects
        /// </summary>
        /// <returns>Collection of School subject</returns>
        IEnumerable<SchoolSubject> GetAll();

        /// <summary>
        /// Get <see cref="SchoolSubject"/>
        /// </summary>
        /// <returns></returns>
        SchoolSubject Get(int id);

        /// <summary>
        /// Creates new  <see cref="SchoolSubject"/>
        /// </summary>
        /// <returns></returns>
        void Create(SchoolSubject schoolSubject);

        /// <summary>
        /// Updates new  <see cref="SchoolSubject"/>
        /// </summary>
        /// <returns></returns>
        void Update(SchoolSubject schoolSubject);

        /// <summary>
        /// Deletes new  <see cref="SchoolSubject"/>
        /// </summary>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Finds <see cref="SchoolSubject"/>s by parameter
        /// </summary>
        /// <returns></returns>
        IEnumerable<SchoolSubject> Find(Func<SchoolSubject, bool> parameter);
    }
}
