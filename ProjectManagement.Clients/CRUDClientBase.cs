using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ProjectManagement.Classes;

namespace ProjectManagement.Clients
{
    public class CRUDClientBase<TViewModel> : ProjectManagementClientBase
    {
        public CRUDClientBase(
            IHttpClientFactory httpClientFactory,
            ProtectedSessionStorage sessionStorage
            ) : base(httpClientFactory, sessionStorage){}

        public async Task<Response<List<TViewModel>>> GetAll()
        {
            return await GetAsync<List<TViewModel>>("GetAll");
        }
        public async Task<Response<TViewModel>> Save(TViewModel model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));
            return await PostAsync<TViewModel>("Save", model);
        }
        public async Task<Response> Delete(TViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            return await PostAsync<TViewModel>("Delete", model);
        }
    }
}
