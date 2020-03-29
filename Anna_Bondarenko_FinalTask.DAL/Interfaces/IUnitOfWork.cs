using Anna_Bondarenko_FinalTask.DAL.Identity;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Enrollee> EnrolleeGenericRepository { get; }

        IGenericRepository<Faculty> FacultyGenericRepository { get; }

        IGenericRepository<ExaminationSubject> ExamGenericRepository { get; }

        IGenericRepository<FacultySubject> FacultySubjectGenericRepository { get; }

        IGenericRepository<Mark> MarkGenericRepository { get; }

        IGenericRepository<Operator> OperatorGenericRepository { get; }

        IGenericRepository<SchoolSubject> SchoolSubjectGenericRepository { get; }

        IGenericRepository<Statement> StatementGenericRepository { get; }

        IGenericRepository<Comment> CommentGenericRepository { get; }

        IGenericRepository<StudentStatus> StudentStatusRepository { get; }

        ApplicationRoleManager RoleManager { get; }

        ApplicationUserManager UserManager { get; }

        void Commit();
    }
}
