using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectTaskClient : CRUDClientBase<ProjectTaskViewModel>
    {
        public ProjectTaskClient(
            IHttpClientFactory httpClientFactory,
            ProtectedSessionStorage sessionStorage
            ) : base(httpClientFactory, sessionStorage){}
    }
}
