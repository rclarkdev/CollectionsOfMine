using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using CollectionsOfMine.Data.Context;

namespace CollectionsOfMine.Data.Repository
{
    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class
    {
        public RepositoryWithTypedId(CollectionsOfMineDbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        protected DbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangeAsync()
        {
            await Context.SaveChangesAsync();
        }

        public IQueryable<T> Query()
        {
            return DbSet;
        }

        public async Task RemoveAsync(T entity)
        {
            await Task.Run(() => DbSet.Remove(entity));
        }
    }
}
