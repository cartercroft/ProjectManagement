using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectClient : ProjectManagementClientBase<ProjectViewModel>
    {
        public ProjectClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory){}
    }
}
