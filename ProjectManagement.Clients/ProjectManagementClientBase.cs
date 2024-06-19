using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;

namespace ProjectManagement.Clients
{
    public class ProjectManagementClientBase : ClientBase
    {
        private string _controllerName = null!;
        private readonly ProtectedSessionStorage _sessionStorage = null!;
        public ProjectManagementClientBase(
            IHttpClientFactory httpClientFactory,
            ProtectedSessionStorage sessionStorage
            ) : base(httpClientFactory.CreateClient("ProjectManagementClient"))
        {
            _sessionStorage = sessionStorage;
        }
        public ProjectManagementClientBase(
            IHttpClientFactory httpClientFactory,
            ProtectedSessionStorage sessionStorage,
            string controllerName
            ) : base(httpClientFactory.CreateClient("ProjectManagementClient"))
        {
            _controllerName = controllerName;
            _sessionStorage = sessionStorage;
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
                var sessionEntry = await _sessionStorage.GetAsync<string>("AuthToken");
                if(sessionEntry.Success && sessionEntry.Value != null)
                {
                    headers.Add("Authorization", $"Bearer {sessionEntry.Value}");
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
