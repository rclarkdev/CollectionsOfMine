using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Services
{
    public interface IContentTypeService
    {
        Task InsertAsync(ContentType entity);
        Task<ContentType> UpdateAsync(ContentTypeViewModel viewModel);
        Task DeleteAsync(ContentType entity);
        Task<ContentType> GetByIdAsync(long id);
        Task<ContentType> GetByName(string name);
        Task<IQueryable<ContentType>> GetAllAsync();
    }
}
