using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Services
{
    public interface ICollectionService
    {
        Task InsertAsync(CollectionViewModel viewModel);
        Task UpdateAsync(CollectionViewModel viewModel);
        Task DeleteAsync(long id);
        Task<Collection> GetByIdAsync(long id);
        Task<IQueryable<Collection>> GetAllAsync();
        Task<IQueryable<Collection>> GetAllByAreaIdAsync(long areaId);
    }
}
