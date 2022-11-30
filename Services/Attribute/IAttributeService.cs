using CollectionsOfMine.ViewModels;
using Attribute = CollectionsOfMine.Data.Models.Attribute;

namespace CollectionsOfMine.Services
{
    public interface IAttributeService
    {
        Task InsertAsync(Attribute entity);
        Task<Attribute> UpdateAsync(AttributeViewModel viewModel);
        Task DeleteAsync(Attribute entity);
        Task<Attribute> GetByIdAsync(long id);
        Task<Attribute> GetByName(string name);
        Task<IQueryable<Attribute>> GetAllAsync();
    }
}
