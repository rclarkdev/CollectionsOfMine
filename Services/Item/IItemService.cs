using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Services
{
    public interface IItemService
    {
        Task InsertAsync(Item entity);
        Task UpdateAsync();
        Task DeleteAsync(Item entity);
        Task<Item> GetByIdAsync(long id);
        Task<IQueryable<Item>> GetAllAsync();
        Task<IQueryable<Item>> GetAllByCollectionIdAsync(long collectionId);
    }
}
