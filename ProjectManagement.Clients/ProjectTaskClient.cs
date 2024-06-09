using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectTaskClient : CRUDClientBase<ProjectTaskViewModel>
    {
        public ProjectTaskClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory){}
    }
}
