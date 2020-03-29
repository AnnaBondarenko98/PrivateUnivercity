using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Anna_Bondarenko_FinalTask.DAL.Interfaces;
using Anna_Bondarenko_FinalTask.Models.Models;

namespace Anna_Bondarenko_FinalTask.DAL.EF
{
    public class CommitteeContext : IdentityDbContext<IdentityUser>, IСommitteeContext
    {
        static CommitteeContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public CommitteeContext(string connection) : base(connection)
        {

        }

        public DbSet<Enrollee> Enrollees { get; set; }

        public DbSet<ExaminationSubject> ExaminationSubjects { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<StudentStatus> StudentStatuses { get; set; }

        public DbSet<FacultySubject> FacultySubjects { get; set; }

        public DbSet<Mark> Marks { get; set; }

        public DbSet<Operator> Operators { get; set; }

        public DbSet<SchoolSubject> SchoolSubjects { get; set; }

        public DbSet<Statement> Statements { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
