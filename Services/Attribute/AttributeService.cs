using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Extensions;
using CollectionsOfMine.ViewModels;
using Attribute = CollectionsOfMine.Data.Models.Attribute;


namespace CollectionsOfMine.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttributeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task InsertAsync(Attribute entity)
        {
            await _unitOfWork.AttributeRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Attribute> UpdateAsync(AttributeViewModel viewModel)
        {
            var attribute = await GetByIdAsync(viewModel.Id);

            attribute.UpdatedOn = DateTime.Now;
            attribute.Name = viewModel.Name;
            attribute.Description = viewModel.Description;

            await _unitOfWork.AttributeRepository.SaveChangeAsync();

            return attribute;
        }

        public async Task DeleteAsync(Attribute entity)
        {
            await _unitOfWork.AttributeRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Attribute> GetByIdAsync(long id)
        {
            return await Task.Run(() =>
                _unitOfWork.AttributeRepository.Query()
                .Where(a => a.Id == id)
                .Include(i => i.Items)
                .FirstOrDefault());
        }

        public async Task<Attribute> GetByName(string name)
        {
            return await Task.Run(() =>
                _unitOfWork.AttributeRepository.Query()
                .Where(a => a.Name == name)
                .Include(i => i.Items)
                .FirstOrDefault());
        }

        public async Task<IQueryable<Attribute>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork
                .AttributeRepository.Query()
                .Include(i => i.Items));
        }

    }
}