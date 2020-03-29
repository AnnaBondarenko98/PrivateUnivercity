using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IMarkService
    {
        /// <summary>
        /// Getting all Marks
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mark> GetAll();

        /// <summary>
        /// Get <see cref="Mark"/> by id
        /// </summary>
        /// <returns></returns>
        Mark Get(int id);

        /// <summary>
        /// Creates new  <see cref="Mark"/>
        /// </summary>
        /// <param name="Mark">Mark for creating</param>
        void Create(Mark mark);

        /// <summary>
        /// Updates the <see cref="Mark"/>
        /// </summary>
        /// <param name="comment">Mark for updating</param>
        void Update(Mark mark);

        /// <summary>
        /// Deletes new  <see cref="Mark"/>
        /// </summary>
        /// <param name="id">Mark id</param>
        void Delete(int id);

        /// <summary>
        /// Finds  <see cref="Mark"/>s by parameter
        /// </summary>
        /// <param name="parameter">parameter for searching</param>
        /// <returns>Collection of Marks</returns>
        IEnumerable<Mark> Find(Func<Mark, bool> parameter);

    }
}
