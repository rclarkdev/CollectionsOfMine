using CollectionsOfMine.ViewModels;

namespace CollectionsOfMine.Services
{
    public interface IUserService
    {
        Task<CreateOrUpdateUser> CreateAsync(CreateOrUpdateUser model);
        Task<CreateOrUpdateUser> UpdateAsync(CreateOrUpdateUser model);
        Task DeleteAsync(long id);
        Task<CreateOrUpdateUser> GetByIdAsync(long id);
        Task<IEnumerable<CreateOrUpdateUser>> GetAllAsync();
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    }
}
