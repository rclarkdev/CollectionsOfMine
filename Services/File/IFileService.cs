using CollectionsOfMine.ViewModels;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Services
{
    public interface IFileService
    {
        Task InsertAsync(File entity);
        Task<File> UpdateAsync(FileViewModel viewModel);
        Task DeleteAsync(File entity);
        Task<File> GetByIdAsync(long id);
        Task<File> GetByName(string name);
        Task<IQueryable<File>> GetAllAsync();
    }
}
