using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IFacultyService
    {
        /// <summary>
        /// Getting all Faculties
        /// </summary>
        /// <returns><see cref="Faculty"/></returns>
        IEnumerable<Faculty> GetAll();

        /// <summary>
        /// Get <see cref="Faculty"/>
        /// </summary>
        /// <returns><see cref="Faculty"/></returns>
        Faculty Get(int id);

        /// <summary>
        /// Creates new  <see cref="Faculty"/>
        /// </summary>
        /// <param name="faculty">Faculty for creating</param>
        void Create(Faculty faculty);

        /// <summary>
        /// Updates new  <see cref="Faculty"/>
        /// </summary>
        /// <param name="faculty">Faculty for updating</param>
        void Update(Faculty faculty);

        /// <summary>
        /// Deletes new  <see cref="Faculty"/>
        /// </summary>
        /// <param name="id">Faculty id</param>
        void Delete(int id);

        /// <summary>
        /// Gets subjects of any faculty
        /// </summary>
        /// <returns> Collection of faculty subject</returns>
        IEnumerable<FacultySubject> GetFacultySubjects(int id);

        /// <summary>
        /// Gets  faculty with some faculty subjects
        /// </summary>
        /// <param name="faculty">Faculty</param>
        /// <param name="names">Names of subject</param>
        void GetFacultyWithFacultySubjects(Faculty faculty, IEnumerable<string> names);

        /// <summary>
        /// Sorting faculties by parameters
        /// </summary>
        /// <param name="id">Faculties's ID </param>
        /// <param name="faculties"> faculties for sorting</param>
        /// <returns>Sorted Faculties </returns>
        IEnumerable<Faculty> Sort(int id, string from, string to, IEnumerable<Faculty> faculties);

        /// <summary>
        /// Gets  exam subjects by faculty id
        /// </summary>
        /// <param name="id">Faculty id</param>
        /// <returns>Collection of exam subject</returns>
        IEnumerable<ExaminationSubject> GetExamSubjects(int id);

        /// <summary>
        /// Gets faculty  with some exam subjects
        /// </summary>
        /// <param name="faculty">Faculty</param>
        /// <param name="names">names of subject</param>
        void GetFacultyWithExamSubjects(Faculty faculty, IEnumerable<string> names);

        /// <summary>
        /// Finds  <see cref="Faculty"/> by parameter
        /// </summary>
        /// <returns>Collection of faculty</returns>
        IEnumerable<Faculty> Find(Func<Faculty,bool> parameter);
    }
}
