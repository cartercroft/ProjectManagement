namespace DataLayerAbstractions
{
    public interface IRepository
    {
        public object Save(object model);
        public void Delete(object model);
        public object Get(int id);
        public List<object> GetAll();
    }
    public interface IRepository<TModel>
    {
        public TModel Save(TModel model);
        public void Delete(TModel model);
        public TModel Get(int id);
        public List<TModel> GetAll();
    }
}
