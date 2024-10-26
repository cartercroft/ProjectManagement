using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayerAbstractions.Interfaces
{
    public interface IAPIController<TKey,
        TViewModel,
        TDataModel,
        TService,
        TRepository>
        where TViewModel : IViewModel<TKey>
        where TDataModel : IModel<TKey>
        where TRepository : IRepository<TKey, TDataModel>
        where TService : IService<TKey, TViewModel, TDataModel, TRepository>
    {
        public TViewModel Get(TKey id);
        public IEnumerable<TViewModel> GetAll();
        public TViewModel Save(TViewModel viewModel);
        public void Delete(TViewModel viewModel);
    }
}
