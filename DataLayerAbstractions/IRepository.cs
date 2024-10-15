namespace DataLayerAbstractions
{
    public interface IRepository
    {
        public object Save(object model);
        public void Delete(object model);
        public object Get(int id);
        public List<object> GetAll();
    }
    public interface IRepository<TModel, TKey>
    {
        public TModel Save(TModel model);
        public void Delete(TModel model);
        public TModel Get(TKey id);
        public List<TModel> GetAll();
    }
}
