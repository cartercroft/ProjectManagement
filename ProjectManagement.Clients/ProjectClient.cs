using LayerAbstractions;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectClient : CRUDClientBase<ProjectViewModel>
    {
        public ProjectClient(
            IHttpClientFactory httpClientFactory,
            ProtectedLocalStorage localStorage
            ) : base(httpClientFactory, localStorage){}

        public async Task<Response<List<ProjectViewModel>>> GetProjectsForCurrentUser()
        {
            return await GetAsync<List<ProjectViewModel>>("GetProjectsForCurrentUser");
        }
    }
}
