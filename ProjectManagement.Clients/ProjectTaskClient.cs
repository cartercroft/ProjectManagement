using Newtonsoft.Json;
using ProjectManagement.Classes;
using ProjectManagement.Models;
using ProjectManagement.Public.Models;
using System.Net.Http.Headers;

namespace ProjectManagement.Clients
{
    public class ProjectTaskClient
    {
        private readonly HttpClient _client;
        public ProjectTaskClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7055");
        }
        public async Task<ProjectTask> GetProjectTask(int id)
        {
            ProjectTask dataResponse = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>
            {
                KeyValuePair.Create<string, object>("id", id)
            };

            var response = await _client.GetAsync("/ProjectTask/Get" + parameters.AsQueryString());

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                dataResponse = JsonConvert.DeserializeObject<ProjectTask>(responseBody);
            }
            return dataResponse;
        } 

        public async Task SaveProjectTask(ProjectTaskViewModel projectTask)
        {
            string body = JsonConvert.SerializeObject(projectTask);
            HttpContent content = new StringContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _client.PostAsync("/api/ProjectTask/Save", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success!");
            }
        }
    }
}
