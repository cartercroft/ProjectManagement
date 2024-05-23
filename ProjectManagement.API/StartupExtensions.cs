using AutoMapper;
using ProjectManagement.Repositories;
using ProjectManagement.Services;

namespace ProjectManagement.API
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureAndAddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectManagementMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ProjectTaskRepository>();
            
            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ProjectTaskService>();

            return services;
        }
    }
}
