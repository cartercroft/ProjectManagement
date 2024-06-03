using ProjectManagement.Classes;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class ProjectManagementClientBase<TViewModel> : ClientBase
    {
        private string p_ControllerName = null!;
        public ProjectManagementClientBase(IHttpClientFactory httpClientFactory) : base(httpClientFactory.CreateClient("ProjectManagementClient")){}
        public async Task<Response<List<TViewModel>>> GetAll()
        {
            return await GetAsync<List<TViewModel>>($"{GetControllerName()}/GetAll");
        }
        public async Task<Response> Save(TViewModel model)
        {
            return await PostAsync<TViewModel>($"{GetControllerName()}/Save", model);
        }
        public async Task<Response> Delete (TViewModel model)
        {
            return await PostAsync<TViewModel>($"{GetControllerName()}/Delete", model);
        }
        private string GetRuntimeClassName()
        {
            return this.GetType().Name;
        }
        private string GetControllerName()
        {
            if (p_ControllerName == null)
                p_ControllerName = GetRuntimeClassName().Replace("Client", "");
            return p_ControllerName;
        }
    }
}
