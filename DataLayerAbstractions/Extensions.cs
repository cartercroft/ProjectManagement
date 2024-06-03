using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLayerAbstractions
{
    public static class Extensions
    {
        public static IQueryable<T> ApplyInclusion<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            if(includes == null || includes.Length == 0) return query;

            query = includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }
        public static IQueryable<T> ApplyInclusion<T>(this IQueryable<T> query, IEnumerable<Expression<Func<T, object>>>? includes) where T : class
        {
            return query.ApplyInclusion<T>(includes?.ToArray() ?? []);
        }
    }
}
