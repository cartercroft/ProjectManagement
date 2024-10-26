using AutoMapper;
using DataLayerAbstractions;
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
            //AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(x => x.GetTypes())
            //    .Where(x => x.BaseType == typeof(RepositoryBase<ModelBase>))
            //    .ToList()
            //    .ForEach(x => services.AddScoped(x));
            services.AddScoped<ProjectTaskRepository>();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<RoleRepository>();

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ProjectTaskService>();
            services.AddScoped<ProjectService>();
            services.AddScoped<RoleService>();

            return services;
        }
    }
}
