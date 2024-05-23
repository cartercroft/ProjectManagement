using ProjectManagement.Clients;

namespace ProjectManagement.UI
{
    public static class StartupExtentions
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddScoped<ProjectTaskClient>();

            return services;
        }
    }
}
