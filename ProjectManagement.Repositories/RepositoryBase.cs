using Microsoft.EntityFrameworkCore;
using ProjectManagement.Classes.Interfaces;
using ProjectManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProjectManagement.Repositories
{
    public class RepositoryBase<TModel> : IRepository<TModel> where TModel : ModelBase
    {
        private DbContext _dbContext;
        private readonly DbSet<TModel> _dbSet;
        public RepositoryBase(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TModel>();
        }
        public virtual void Delete(TModel model)
        {
            model.IsDeleted = true;
            Save(model);
        }
        public virtual void Save(TModel model)
        {
            int key = GetModelKey(model);

            model.UpdatedWhen = DateTime.Now;

            if(key > 0)
            {
                Update(model);
            }
            else
            {
                Add(model);
            }

            _dbContext.SaveChanges();
        }

        protected virtual void Add(TModel model)
        {
            model.CreatedWhen = DateTime.Now;
            _dbSet.Add(model);
        }

        protected virtual void Update(TModel model) 
        { 
            _dbSet.Update(model);
        }
        public virtual TModel Get(int id)
        {
            TModel? model = _dbSet.Find(id);
            if(model == null)
            {
                throw new KeyNotFoundException($"No {typeof(TModel).Name} found with ID {id}.");
            }
            return model;
        }
        public virtual List<TModel> GetAll()
        {
            return _dbSet.Any() ? _dbSet.ToList() : new List<TModel>();
        }
        private int GetModelKey(TModel model)
        {
            foreach (PropertyInfo property in typeof(TModel).GetProperties())
            {
                var attribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute))
                    as KeyAttribute;

                if (attribute != null) // This property has a KeyAttribute
                {
                    if (int.TryParse(property.GetValue(model)?.ToString(), out int key))
                    {
                        return key;
                    }
                    else
                    {
                        throw new NotSupportedException("Unable to use types other than int for model keys. Please contact a developer.");
                    }
                }
            }
            return -1;
        }
    }
}
