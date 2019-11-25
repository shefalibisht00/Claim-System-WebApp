using AutoMapper;

namespace ClaimSystem.BLL.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Mapper;

        public static void Init()
        {// mapper.ConfigurationProvider

            var config = new MapperConfiguration(x =>
            {
                x.AllowNullCollections = true;
                x.AddProfile(new DomainEntityMappingProfile());
            });
            Mapper = new Mapper(config);
        }
    }
}
