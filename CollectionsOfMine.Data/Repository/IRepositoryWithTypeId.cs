using Microsoft.EntityFrameworkCore.Storage;

namespace CollectionsOfMine.Data.Repository
{
    public interface IRepositoryWithTypedId<T, in TId>
    {
        IQueryable<T> Query();

        Task AddAsync(T entity);

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task SaveChangeAsync();

        Task RemoveAsync(T entity);
    }
}
