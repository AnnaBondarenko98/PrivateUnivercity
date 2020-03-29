using AutoMapper;

namespace Anna_Bondarenko_FinalTask.WEB.Infrastructure.Mapper
{
    public class MapperInitialize
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ForAllMaps((map, expression) => map.PreserveReferences = true);

                cfg.AddProfile(new ViewModelToDto());
                cfg.AddProfile(new ViewModelToDomainModel());
                cfg.AddProfile(new DomainModelToViewModel());
                cfg.AddProfile(new DtoModelToViewModel());
                cfg.AddProfile(new DtoModelToDomainModel());

            });

            return config;
        }
    }
}