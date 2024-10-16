using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Public.Models;

namespace ProjectManagement.Clients
{
    public class RoleClient : CRUDClientBase<RoleViewModel>
    {
        public RoleClient(
            IHttpClientFactory httpClientFactory,
            ProtectedSessionStorage sessionStorage
            ) : base(httpClientFactory, sessionStorage) { }
    }
}
