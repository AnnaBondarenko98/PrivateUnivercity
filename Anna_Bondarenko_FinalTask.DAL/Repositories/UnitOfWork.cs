using System;
using Anna_Bondarenko_FinalTask.DAL.EF;
using Anna_Bondarenko_FinalTask.DAL.Identity;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Anna_Bondarenko_FinalTask.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IСommitteeContext _db;

        private readonly Lazy<GenericGenericRepository<Enrollee>> _enrolleeRepos;
        private readonly Lazy<GenericGenericRepository<ExaminationSubject>> _examRepos;
        private readonly Lazy<GenericGenericRepository<Faculty>> _facultyRepos;
        private readonly Lazy<GenericGenericRepository<FacultySubject>> _facultySubjectRepos;
        private readonly Lazy<GenericGenericRepository<Mark>> _markRepos;
        private readonly Lazy<GenericGenericRepository<Operator>> _operatorRepos;
        private readonly Lazy<GenericGenericRepository<SchoolSubject>> _schoolSubjectRepos;
        private readonly Lazy<GenericGenericRepository<Statement>> _statementRepos;
        private readonly Lazy<GenericGenericRepository<Comment>> _commentRepos;
        private readonly Lazy<GenericGenericRepository<StudentStatus>> _studentStatusRepos;


        public UnitOfWork(IСommitteeContext db)
        {
            _db = db;

            _enrolleeRepos = new Lazy<GenericGenericRepository<Enrollee>>(
                () => new GenericGenericRepository<Enrollee>(_db));

            _examRepos = new Lazy<GenericGenericRepository<ExaminationSubject>>(
                () => new GenericGenericRepository<ExaminationSubject>(_db));

            _facultyRepos = new Lazy<GenericGenericRepository<Faculty>>(
                () => new GenericGenericRepository<Faculty>(_db));

            _facultySubjectRepos = new Lazy<GenericGenericRepository<FacultySubject>>(
                () => new GenericGenericRepository<FacultySubject>(_db));

            _markRepos = new Lazy<GenericGenericRepository<Mark>>(
                () => new GenericGenericRepository<Mark>(_db));

            _operatorRepos = new Lazy<GenericGenericRepository<Operator>>(
                () => new GenericGenericRepository<Operator>(_db));

            _schoolSubjectRepos = new Lazy<GenericGenericRepository<SchoolSubject>>(
                () => new GenericGenericRepository<SchoolSubject>(_db));

            _statementRepos = new Lazy<GenericGenericRepository<Statement>>(
                () => new GenericGenericRepository<Statement>(_db));

            _commentRepos = new Lazy<GenericGenericRepository<Comment>>(
                () => new GenericGenericRepository<Comment>(_db));

            _studentStatusRepos = new Lazy<GenericGenericRepository<StudentStatus>>(
                () => new GenericGenericRepository<StudentStatus>(_db));

            UserManager = new ApplicationUserManager(new UserStore<IdentityUser>((CommitteeContext)_db));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>((CommitteeContext)_db));
        }

        public IGenericRepository<Enrollee> EnrolleeGenericRepository => _enrolleeRepos.Value;

        public IGenericRepository<ExaminationSubject> ExamGenericRepository => _examRepos.Value;

        public IGenericRepository<Faculty> FacultyGenericRepository => _facultyRepos.Value;

        public IGenericRepository<FacultySubject> FacultySubjectGenericRepository => _facultySubjectRepos.Value;

        public IGenericRepository<Mark> MarkGenericRepository => _markRepos.Value;

        public IGenericRepository<Operator> OperatorGenericRepository => _operatorRepos.Value;

        public IGenericRepository<SchoolSubject> SchoolSubjectGenericRepository => _schoolSubjectRepos.Value;

        public IGenericRepository<Statement> StatementGenericRepository => _statementRepos.Value;

        public IGenericRepository<Comment> CommentGenericRepository => _commentRepos.Value;

        public IGenericRepository<StudentStatus> StudentStatusRepository => _studentStatusRepos.Value;

        public ApplicationRoleManager RoleManager { get; }

        public ApplicationUserManager UserManager { get; }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}
