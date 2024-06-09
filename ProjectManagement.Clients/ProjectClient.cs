using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectClient : CRUDClientBase<ProjectViewModel>
    {
        public ProjectClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory){}
    }
}
