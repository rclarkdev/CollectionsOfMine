using System.Data.Entity;
using System.Linq.Expressions;

namespace CollectionsOfMine.Extensions
{
    public static class IncludeExtension
    {
        public static IQueryable<TEntity> Include<TEntity>(this IDbSet<TEntity> dbSet,
                                                params Expression<Func<TEntity, object>>[] includes)
                                                where TEntity : class
        {
            IQueryable<TEntity> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query == null ? dbSet : query;
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
