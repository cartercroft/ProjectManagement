using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;

namespace ProjectManagement.Clients
{
    public class ProjectManagementClientBase : ClientBase
    {
        private string _controllerName = null!;
        private readonly ProtectedLocalStorage _localStorage = null!;
        public ProjectManagementClientBase(
            IHttpClientFactory httpClientFactory,
            ProtectedLocalStorage localStorage
            ) : base(httpClientFactory.CreateClient("ProjectManagementClient"))
        {
            _localStorage = localStorage;
        }
        public ProjectManagementClientBase(
            IHttpClientFactory httpClientFactory,
            ProtectedLocalStorage localStorage,
            string controllerName
            ) : base(httpClientFactory.CreateClient("ProjectManagementClient"))
        {
            _controllerName = controllerName;
            _localStorage = localStorage;
        }

        public override async Task<Response<TResponse>> PostAsync<TResponse>(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            headers = await AddAuthToken(headers);
            return await base.PostAsync<TResponse>(GetEndpointWithControllerName(endpoint), bodyContent, headers);
        }
        public override async Task<Response<TResponse>> GetAsync<TResponse>(string endpoint, Dictionary<string, object>? pathParameters = null, Dictionary<string, string>? headers = null)
        {
            headers = await AddAuthToken(headers);
            return await base.GetAsync<TResponse>(GetEndpointWithControllerName(endpoint), pathParameters, headers);
        }
        private string GetRuntimeClassName()
        {
            return this.GetType().Name;
        }
        protected string GetControllerName()
        {
            if (_controllerName == null)
                _controllerName = GetRuntimeClassName().Replace("Client", "");
            return _controllerName;
        }
        private async Task<Dictionary<string,string>?> AddAuthToken(Dictionary<string, string>? headers)
        {
            if (headers == null)
                headers = new Dictionary<string, string>();
            if (!headers.ContainsKey("Authorization"))
            {
                var storageEntry = await _localStorage.GetAsync<string>("AuthToken");
                if(storageEntry.Success && storageEntry.Value != null)
                {
                    headers.Add("Authorization", $"Bearer {storageEntry.Value}");
                }
            }
            return headers;
        }
        protected string GetEndpointWithControllerName(string endpoint)
        {
            string controllerName = GetControllerName();
            return $"{(!string.IsNullOrEmpty(controllerName) ? $"{controllerName}/{endpoint}" : endpoint)}";
        }
    }
}
