namespace ProjectManagement.Clients
{
    public class ProjectManagementClientBase : ClientBase
    {
        public ProjectManagementClientBase(IHttpClientFactory httpClientFactory) : base(httpClientFactory.CreateClient("ProjectManagementClient")){}
    }
}
