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
        public virtual int Save(TModel model)
        {
            int key = model.Id;

            model.UpdatedWhen = DateTime.Now;
            DetachChildCollectionProperties(model);

            if (key > 0)
            {
                Update(model);
            }
            else
            {
                Add(model);
            }
            _dbContext.SaveChanges();
            return model.Id;
        }
        protected virtual void Add(TModel model)
        {
            model.CreatedWhen = model.UpdatedWhen;
            if (_dbSet.Find(model.Id) != null)
            {
                throw new Exception($"{typeof(TModel).Name} Entity already exists and cannot be added.");
            }
            _dbSet.Add(model);
            _dbContext.Entry(model).State = EntityState.Added;
        }
        protected virtual void Update(TModel model)
        {
            _dbSet.Update(model);
        }
        private void DetachChildCollectionProperties(TModel model)
        {
            foreach (var property in typeof(TModel).GetProperties())
            {
                if (property.PropertyType == typeof(IEnumerable<ModelBase>))
                {
                    foreach (var childObject in (IEnumerable<ModelBase>)property.GetValue(model))
                    {
                        _dbContext.Entry(childObject).State = EntityState.Detached;
                    }
                }
            }
        }
        public virtual TModel Get(int id)
        {
            TModel? model = _dbSet
                .AsNoTracking()
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
            return GetAllNoInclusions()
                .ApplyInclusion(AlwaysInclude)
                .ApplyInclusion(IncludeOnGetAll)
                .ToList();
        }
        public virtual IQueryable<TModel> GetAllNoInclusions()
        {
            return _dbSet
                .AsNoTracking()
                .Where(i => !i.IsDeleted);
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
