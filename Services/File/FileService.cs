using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Extensions;
using CollectionsOfMine.ViewModels;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Services
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task InsertAsync(File entity)
        {
            await _unitOfWork.FileRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<File> UpdateAsync(FileViewModel viewModel)
        {
            var file = await GetByIdAsync(viewModel.Id);

            file.UpdatedOn = DateTime.Now;
            file.Name = viewModel.Name;
            file.Description = viewModel.Description;

            await _unitOfWork.FileRepository.SaveChangeAsync();

            return file;
        }

        public async Task DeleteAsync(File entity)
        {
            await _unitOfWork.FileRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<File> GetByIdAsync(long id)
        {
            return await Task.Run(() =>
                _unitOfWork.FileRepository.Query()
                .Where(a => a.Id == id)
                .Include(i => i.Items)
                .FirstOrDefault());
        }

        public async Task<File> GetByName(string name)
        {
            return await Task.Run(() =>
                _unitOfWork.FileRepository.Query()
                .Where(a => a.Name == name)
                .Include(i => i.Items)
                .FirstOrDefault());
        }

        public async Task<IQueryable<File>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork
                .FileRepository.Query()
                .Include(a => a.Items));
        }

    }
}