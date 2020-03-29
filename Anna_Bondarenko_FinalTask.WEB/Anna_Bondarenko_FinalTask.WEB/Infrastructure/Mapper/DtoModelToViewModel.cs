using Anna_Bondarenko_FinalTask.BLL.DTO;
using Anna_Bondarenko_FinalTask.WEB.Models;
using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper
{
    public class DtoModelToViewModel : Profile
    {
        public DtoModelToViewModel()
        {
            CreateMap<LoginDto, LoginVm>();
        }

    }
}