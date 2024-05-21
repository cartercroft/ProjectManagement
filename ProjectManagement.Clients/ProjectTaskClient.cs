using Newtonsoft.Json;
using ProjectManagement.Classes;
using ProjectManagement.Models;

namespace ProjectManagement.Clients
{
    public class ProjectTaskClient
    {
        private readonly HttpClient _client;
        public ProjectTaskClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<ProjectTask> GetProjectTask(int id)
        {
            ProjectTask dataResponse = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>
            {
                KeyValuePair.Create<string, object>("id", id)
            };

            var response = await _client.GetAsync("/ProjectTasks/Get" + parameters.AsQueryString());

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                dataResponse = JsonConvert.DeserializeObject<ProjectTask>(responseBody);
            }
            return dataResponse;
        } 

        public async Task SaveProjectTask(ProjectTask projectTask)
        {

        }
    }
}
