using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Extensions;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;


namespace CollectionsOfMine.Services
{
    public class ContentTypeService : IContentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContentTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task InsertAsync(ContentType entity)
        {
            await _unitOfWork.ContentTypeRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ContentType> UpdateAsync(ContentTypeViewModel viewModel)
        {
            var contentType = await GetByIdAsync(viewModel.Id);

            contentType.UpdatedOn = DateTime.Now;
            contentType.Name = viewModel.Name;
            contentType.Description = viewModel.Description;

            await _unitOfWork.ContentTypeRepository.SaveChangeAsync();

            return contentType;
        }

        public async Task DeleteAsync(ContentType entity)
        {
            await _unitOfWork.ContentTypeRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<ContentType> GetByIdAsync(long id)
        {
            return await Task.Run(() =>
                _unitOfWork.ContentTypeRepository.Query()
                .Where(a => a.Id == id)
                .Include(i => i.Collections)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.Attributes)
                .FirstOrDefault());
        }

        public async Task<ContentType> GetByName(string name)
        {
            return await Task.Run(() =>
                _unitOfWork.ContentTypeRepository.Query()
                .Where(a => a.Name == name)
                .Include(i => i.Collections)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.Attributes)
                .FirstOrDefault());
        }

        public async Task<IQueryable<ContentType>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork
                .ContentTypeRepository.Query()
                .Include(a => a.Collections));
        }

    }
}