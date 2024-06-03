﻿namespace DataLayerAbstractions
{
    public interface IRepository<TModel>
    {
        public void Save(TModel model);
        public void Delete(TModel model);
        public TModel Get(int id);
        public List<TModel> GetAll();
    }
}
