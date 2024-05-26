using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectClient : ProjectManagementClientBase
    {
        public ProjectClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory){}
        public async Task SaveProject(ProjectViewModel project)
        {
            await PostAsync("Project/Save", project);
        }
        public async Task<List<ProjectViewModel>> GetAll()
        {
            return await GetAsync<List<ProjectViewModel>>("Project/GetAll");
        }
    }
}
