using Anna_Bondarenko_FinalTask.Models.Models;
using Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Models;
using Anna_Bondarenko_FinalTask.WEB.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper
{
    public class DomainModelToViewModel : Profile
    {
        public DomainModelToViewModel()
        {
            CreateMap<Faculty, EditFaculty>()
                .ForMember(dest => dest.CheckSubjects, otp => otp.Ignore());

            CreateMap<FacultySubject, CheckModel>()
                .ForMember(dest => dest.Subject, otp => otp.MapFrom(src => src));

            CreateMap<Operator, OperatorVm>();

            CreateMap<ExaminationSubject, CheckExamSubjects>()
                .ForMember(dest => dest.Subject, otp => otp.MapFrom(src => src));
        }
    }
}