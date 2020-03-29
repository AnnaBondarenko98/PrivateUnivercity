using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IExaminationSubjectService
    {
        /// <summary>
        /// Getting all Examination Subjects
        /// </summary>
        /// <returns></returns>
        IEnumerable<ExaminationSubject> GetAll();

        /// <summary>
        /// Get <see cref="ExaminationSubject"/> by id 
        /// </summary>
        /// <param name="id"> Examination subject id</param>
        /// <returns>Examination Subject</returns>
        ExaminationSubject Get(int id);

        /// <summary>
        /// Creates new  <see cref="ExaminationSubject"/>
        /// </summary>
        /// <returns></returns>
        void Create(ExaminationSubject examinationSubject);

        /// <summary>
        /// Uodates  <see cref="ExaminationSubject"/>
        /// </summary>
        /// <param name="examinationSubject"> Subject for updating</param>
        /// <returns></returns>
        void Update(ExaminationSubject examinationSubject);

        /// <summary>
        /// Deletes <see cref="ExaminationSubject"/>
        /// </summary>
        /// <param name="id"> Subject id</param>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Finds <see cref="ExaminationSubject"/>s by parameter
        /// </summary>
        /// <param name="parameter">Parameter for searching</param>
        /// <returns>Collection of ExaminationSubject </returns>
        IEnumerable<ExaminationSubject> Find(Func<ExaminationSubject, bool> parameter);
    }
}
