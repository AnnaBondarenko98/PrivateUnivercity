using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.Models.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper
{
    public class DtoModelToDomainModel : Profile
    {
        public DtoModelToDomainModel()
        {
            CreateMap<OperatorDto, Operator>().ForMember(d=>d.AppCustomer , s=>s.Ignore());
            CreateMap<EnrolleeDto, Enrollee>().ForMember(d=>d.AppCustomer,s=>s.Ignore()).ForMember(d=>d.Lock,s=>s.Ignore());
        }
    }
}