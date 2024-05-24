using Newtonsoft.Json;
using ProjectManagement.Classes;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using System.Net.Http.Headers;

namespace ProjectManagement.Clients
{
    public class ProjectTaskClient : ProjectManagementClientBase
    {
        public ProjectTaskClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory){}
        public async Task<ProjectTask> GetProjectTask(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters["id"] = id;
            return await GetAsync<ProjectTask>("ProjectManagement/Get", parameters);
        } 

        public async Task SaveProjectTask(ProjectTaskViewModel projectTask)
        {
            await PostAsync("ProjectTask/Save", projectTask);
        }
    }
}
