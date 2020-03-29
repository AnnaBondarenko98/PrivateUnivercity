using Anna_Bondarenko_FinalTask.BLL.Interfaces;
using Anna_Bondarenko_FinalTask.BLL.Services;
using Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper;
using Autofac;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Autofac.DIModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EnrolleeService>().As<IEnrolleeService>();
            builder.RegisterType<FacultyService>().As<IFacultyService>();
            builder.RegisterType<OperatorService>().As<IOperatorService>();
            builder.RegisterType<ExaminationSubjectService>().As<IExaminationSubjectService>();
            builder.RegisterType<FacultySubjectService>().As<IFacultySubjectService>();
            builder.RegisterType<MarkService>().As<IMarkService>();
            builder.RegisterType<StatementService>().As<IStatementService>();
            builder.RegisterType<SchoolSunjectService>().As<ISchoolSubjectsService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<StudentStatusService>().As<IStudentStatusService>();
            builder.RegisterType<MarkService>().As<IMarkService>();


            var mapper = MapperInitialize.InitializeAutoMapper().CreateMapper();

            builder.RegisterInstance(mapper);
        }
    }
}