using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class RoleClient : CRUDClientBase<RoleViewModel>
    {
        public RoleClient(
            IHttpClientFactory httpClientFactory,
            ProtectedLocalStorage localStorage
            ) : base(httpClientFactory, localStorage) { }
    }
}
