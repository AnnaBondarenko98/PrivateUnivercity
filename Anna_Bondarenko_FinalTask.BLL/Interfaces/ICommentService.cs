using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// Getting all entities
        /// </summary>
        /// <returns>Comment collection</returns>
        IEnumerable<Comment> GetAll();

        /// <summary>
        /// Get <see cref="Comment"/>
        /// </summary>
        /// <returns>Comment</returns>
        Comment Get(int id);

        /// <summary>
        /// Creates new  <see cref="Comment"/>
        /// </summary>
        /// <param name="comment">Comment for creating</param>
        /// <param name="name">User name</param>
        void Create(Comment comment,string name);

        /// <summary>
        /// Updates the <see cref="Comment"/>
        /// </summary>
        /// <param name="comment">Comment for updating</param>
        void Update(Comment comment);

        /// <summary>
        /// Deletes new  <see cref="Comment"/>E:\FINAL TASK\Anna_Bondarenko_FinalTask.BLL\Interfaces\ICommentService.cs
        /// </summary>
        /// <param name="id">Comment id</param>
        void Delete(int id);

        /// <summary>
        /// Finds  <see cref="Comment"/>s by parameter
        /// </summary>
        /// <param name="parameter">parameter for searching</param>
        /// <returns>Collection of Comments</returns>
        IEnumerable<Comment> Find(Func<Comment, bool> parameter);
    }
}
