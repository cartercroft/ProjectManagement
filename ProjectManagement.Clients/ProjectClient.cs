using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectClient : CRUDClientBase<ProjectViewModel>
    {
        public ProjectClient(
            IHttpClientFactory httpClientFactory,
            ProtectedSessionStorage sessionStorage
            ) : base(httpClientFactory, sessionStorage){}
        public async Task<Response<List<ProjectViewModel>>> GetAllProjectsForUser()
        {
            return await base.GetAsync<List<ProjectViewModel>>("GetAllProjectsForUser");
        }
    }
}
