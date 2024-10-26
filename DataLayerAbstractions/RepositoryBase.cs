using AutoMapper;
using LayerAbstractions;
using LayerAbstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace DataLayerAbstractions
{
    public class RepositoryBase<TKey, TModel> : IRepository<TKey, TModel> 
        where TModel : ModelBase<TKey>
    {
        private DbContext _dbContext;
        private readonly DbSet<TModel> _dbSet;
        private readonly IMapper _mapper;
        public RepositoryBase(DbContext context, IMapper mapper)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TModel>();
            _mapper = mapper;
        }
        protected virtual List<Expression<Func<TModel, object>>> AlwaysInclude { get; } = null!;
        protected virtual List<Expression<Func<TModel, object>>> IncludeOnGet { get; } = null!;
        protected virtual List<Expression<Func<TModel, object>>> IncludeOnGetAll { get; } = null!;
        public virtual void Delete(TModel model)
        {
            model.IsDeleted = true;
            Save(model);
        }
        public virtual TModel Save(TModel model)
        {
            TKey key = model.Id;

            model.UpdatedWhen = DateTime.Now;

            if (key is not null && !key.Equals(default(TKey)))
            {
                Update(model);
            }
            else
            {
                Add(model);
            }
            _dbContext.SaveChanges();
            return Get(model.Id);
        }
        protected virtual void Add(TModel model)
        {
            model.CreatedWhen = model.UpdatedWhen;
            if (_dbSet.Find(model.Id) != null)
            {
                throw new Exception($"{typeof(TModel).Name} Entity already exists and cannot be added.");
            }
            _dbSet.Add(model);
        }
        protected virtual void Update(TModel model)
        {
            var existingModel = _dbSet.Find(model.Id);
            if (existingModel == null)
                throw new Exception("Tried to update a model that did not exist. Contact dev.");

            existingModel = _mapper.Map(model, existingModel);
            _dbSet.Update(existingModel);
        }
        public virtual TModel Get(TKey id)
        {
            TModel? model = _dbSet
                .ApplyInclusion(AlwaysInclude)
                .ApplyInclusion(IncludeOnGet)
                .FirstOrDefault(m => m.Id.Equals(id));

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
