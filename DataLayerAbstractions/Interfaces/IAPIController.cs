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
    }
}
