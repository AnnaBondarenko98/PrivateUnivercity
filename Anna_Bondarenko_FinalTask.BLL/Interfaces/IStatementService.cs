using System;
using System.Collections.Generic;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.BLL.Interfaces
{
    public interface IStatementService
    {
        /// <summary>
        /// Getting all Statements
        /// </summary>
        /// <returns></returns>
        IEnumerable<Statement> GetAll();

        /// <summary>
        /// Get <see cref="Statement"/>
        /// </summary>
        /// <returns></returns>
        Statement Get(int id);

        /// <summary>
        /// Creates new  <see cref="Statement"/>
        /// </summary>
        /// <returns></returns>
        void Create(Statement satatement);

        /// <summary>
        /// Uodates new  <see cref="Statement"/>
        /// </summary>
        /// <returns></returns>
        void Update(Statement satatement);

        /// <summary>
        /// Deletes new  <see cref="Statement"/>
        /// </summary>
        /// <returns></returns>
        void Delete(int id);

        /// <summary>
        /// Deletes all   <see cref="Statement"/>s
        /// </summary>
        /// <returns></returns>
        void DeleteRange(IEnumerable<Statement> statements);

        /// <summary>
        /// Finds  <see cref="Statement"/>s by parameter
        /// </summary>
        /// <returns></returns>
        IEnumerable<Statement> Find (Func<Statement,bool> parameter);

        /// <summary>
        /// Gets budget <see cref="Enrollee"/>s 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Enrollee> GetBudget(Statement statementб, Faculty faculty);

        /// <summary>
        /// Gets contract <see cref="Enrollee"/>s 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Enrollee> GetContract(Statement statementб, Faculty faculty);

        /// <summary>
        /// Send message for contract and budget student
        /// </summary>
        /// <returns></returns>
        void SendMessageForAddedStudents();

    }
}
