using AutoMapper;
using ProjectManagement.Services;

namespace ProjectManagement.API
{
    public static class StartupExtensions
    {
        public static void AddMappingProfile(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectManagementMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
