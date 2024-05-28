using Newtonsoft.Json;
using ProjectManagement.Classes;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using System.Net.Http.Headers;

namespace ProjectManagement.Clients
{
    public class ProjectTaskClient : ProjectManagementClientBase<ProjectTaskViewModel>
    {
        public ProjectTaskClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory){}
    }
}
