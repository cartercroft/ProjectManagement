
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace DataLayerAbstractions
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
        protected virtual List<Expression<Func<TModel, object>>> AlwaysInclude { get; } = null!;
        protected virtual List<Expression<Func<TModel, object>>> IncludeOnGet { get; } = null!;
        protected virtual List<Expression<Func<TModel, object>>> IncludeOnGetAll { get; } = null!;
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
            TModel? model = _dbSet
                .ApplyInclusion(AlwaysInclude)
                .ApplyInclusion(IncludeOnGet)
                .FirstOrDefault(m => m.Id == id);

            if(model == null)
            {
                throw new KeyNotFoundException($"No {typeof(TModel).Name} found with ID {id}.");
            }
            return model;
        }
        public virtual List<TModel> GetAll()
        {
            return _dbSet.Any() ? _dbSet
                .Where(i => !i.IsDeleted)
                .ApplyInclusion(AlwaysInclude)
                .ApplyInclusion(IncludeOnGetAll)
                .ToList() : new List<TModel>();
        }
        public virtual List<TModel> ApplyQuery()
        {
            return _dbSet.Any() ? _dbSet.Where(i => !i.IsDeleted).ToList() : new List<TModel>();
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
