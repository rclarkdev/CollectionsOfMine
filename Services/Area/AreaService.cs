using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Extensions;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;


namespace CollectionsOfMine.Services
{
    public class AreaService : IAreaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task InsertAsync(Area entity)
        {
            await _unitOfWork.AreaRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Area> UpdateAsync(AreaViewModel viewModel)
        {
            var area = await GetByIdAsync(viewModel.Id);

            area.UpdatedOn = DateTime.Now;
            area.Name = viewModel.Name;
            area.Description = viewModel.Description;

            await _unitOfWork.AreaRepository.SaveChangeAsync();

            return area;
        }

        public async Task DeleteAsync(Area entity)
        {
            await _unitOfWork.AreaRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Area> GetByIdAsync(long id)
        {
            return await Task.Run(() =>
                _unitOfWork.AreaRepository.Query()
                .Where(a => a.Id == id)
                .Include(i => i.Collections)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.Attributes)
                .FirstOrDefault());
        }

        public async Task<Area> GetByName(string name)
        {
            return await Task.Run(() =>
                _unitOfWork.AreaRepository.Query()
                .Where(a => a.Name == name)
                .Include(i => i.Collections)
                .ThenInclude(c => c.Items)
                .ThenInclude(i => i.Attributes)
                .FirstOrDefault());
        }

        public async Task<IQueryable<Area>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork
                .AreaRepository.Query()
                .Include(a => a.Collections));
        }

    }
}