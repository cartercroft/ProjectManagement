using ProjectManagement.Clients;
using System.Net.Mime;

namespace ProjectManagement.UI
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddScoped<ProjectTaskClient>();
            services.AddScoped<ProjectClient>();

            return services;
        }
        public static WebApplicationBuilder? AddConfiguredHttpClients(this WebApplicationBuilder? builder)
        {
            var baseURI = new Uri("https://localhost:7055/api/");
            builder?.Services.AddHttpClient<ProjectTaskClient>("ProjectManagementClient", c =>
            {
                c.BaseAddress = baseURI;
            });
            return builder;
        }
    }
}
