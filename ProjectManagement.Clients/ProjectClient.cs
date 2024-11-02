using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectClient : CRUDClientBase<ProjectViewModel>
    {
        public ProjectClient(
            IHttpClientFactory httpClientFactory,
            ProtectedLocalStorage localStorage
            ) : base(httpClientFactory, localStorage){}
    }
}
