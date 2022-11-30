using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Services
{
    public interface IAreaService
    {
        Task InsertAsync(Area entity);
        Task<Area> UpdateAsync(AreaViewModel viewModel);
        Task DeleteAsync(Area entity);
        Task<Area> GetByIdAsync(long id);
        Task<Area> GetByName(string name);
        Task<IQueryable<Area>> GetAllAsync();
    }
}
