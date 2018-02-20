using DbModels = PartsUnlimited.Models;
using ApiModels = PartsUnlimitedStoreService.Models;

namespace PartsUnlimitedStoreService.App_Start
{
    public static class AutoMapperConfig
    {
        private static bool _initialized;

        public static void RegisterMappings()
        {
            if (!_initialized)
            {
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<DbModels.Category, ApiModels.Category>()
                        .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
                    cfg.CreateMap<DbModels.Product, ApiModels.Product>();
                });

                AutoMapper.Mapper.AssertConfigurationIsValid();

                _initialized = true;
            }
        }
    }
}
