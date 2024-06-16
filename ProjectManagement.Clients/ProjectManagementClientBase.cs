using ProjectManagement.Classes;

namespace ProjectManagement.Clients
{
    public class ProjectManagementClientBase : ClientBase
    {
        private string p_ControllerName = null!;
        public ProjectManagementClientBase(IHttpClientFactory httpClientFactory) : base(httpClientFactory.CreateClient("ProjectManagementClient")){}
        public ProjectManagementClientBase(IHttpClientFactory httpClientFactory, string controllerName) : base(httpClientFactory.CreateClient("ProjectManagementClient")) 
        {
            p_ControllerName = controllerName;
        }

        public override async Task<Response<TResponse>> PostAsync<TResponse>(string endpoint, object bodyContent, Dictionary<string, string>? headers = null)
        {
            AddAuthToken(ref headers);
            return await base.PostAsync<TResponse>(GetEndpointWithControllerName(endpoint), bodyContent, headers);
        }
        public override async Task<Response<TResponse>> GetAsync<TResponse>(string endpoint, Dictionary<string, object>? pathParameters = null, Dictionary<string, string>? headers = null)
        {
            AddAuthToken(ref headers);
            return await base.GetAsync<TResponse>(GetEndpointWithControllerName(endpoint), pathParameters, headers);
        }
        private string GetRuntimeClassName()
        {
            return this.GetType().Name;
        }
        protected string GetControllerName()
        {
            if (p_ControllerName == null)
                p_ControllerName = GetRuntimeClassName().Replace("Client", "");
            return p_ControllerName;
        }
        private void AddAuthToken(ref Dictionary<string, string>? headers)
        {
            if(headers == null)
                headers = new Dictionary<string, string>();
            if (!headers.ContainsKey("Authorization")) 
            {
                //headers.Add("Authorization", _tokenProvider.AccessToken);
            }
        }
        protected string GetEndpointWithControllerName(string endpoint)
        {
            string controllerName = GetControllerName();
            return $"{(!string.IsNullOrEmpty(controllerName) ? $"{controllerName}/{endpoint}" : endpoint)}";
        }
    }
}
