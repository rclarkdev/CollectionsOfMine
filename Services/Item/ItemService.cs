using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Extensions;
using CollectionsOfMine.Data.Models;


namespace CollectionsOfMine.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICollectionService _collectionService;

        public ItemService(IUnitOfWork unitOfWork, ICollectionService collectionService)
        {
            _unitOfWork = unitOfWork;
            _collectionService = collectionService;
        }
        public async Task InsertAsync(Item entity)
        {
            await _unitOfWork.ItemRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync()
        {
            await _unitOfWork.ItemRepository.SaveChangeAsync();
        }

        public async Task DeleteAsync(Item entity)
        {
            await _unitOfWork.ItemRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Item> GetByIdAsync(long id)
        {
            return await Task.Run(() =>
                _unitOfWork.ItemRepository.Query()
                .Where(i => i.Id == id)
                .Include(i => i.Collection)
                .ThenInclude(a => a.Area)
                .ThenInclude(c => c.Collections)
                .FirstOrDefault());
        }

        public async Task<IQueryable<Item>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork
                .ItemRepository.Query()
                .Include(i => i.Collection)
                .ThenInclude(a => a.Area)
                .ThenInclude(c => c.Collections));
        }

        public async Task<IQueryable<Item>> GetAllByCollectionIdAsync(long collectionId)
        {
            var collection = await _collectionService.GetByIdAsync(collectionId);
            return (await GetAllAsync()).Where(a => a.Collection.Id == collection.Id);
        }

    }
}