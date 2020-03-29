using Anna_Bondarenko_FinalTask.Models.Models;
using Anna_Bondarenko_FinalTask.WEB.Areas.Operator.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper
{
    public class ViewModelToDomainModel : Profile
    {
        public ViewModelToDomainModel()
        {
            CreateMap<EditFaculty, Faculty>();

            CreateMap<string, FacultySubject>()
                .ForMember(dest => dest.Name, otp => otp.MapFrom(src => src.ToString()));
        }
    }
}