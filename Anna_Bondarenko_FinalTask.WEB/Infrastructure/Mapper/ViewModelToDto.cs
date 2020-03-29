using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.WEB.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper
{
    public class ViewModelToDto : Profile
    {
        public ViewModelToDto()
        {
            CreateMap<LoginVm, LoginDto>();
        }
    }
}