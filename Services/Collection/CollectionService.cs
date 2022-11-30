using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Extensions;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;


namespace CollectionsOfMine.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;

        public CollectionService(IUnitOfWork unitOfWork, IAreaService areaService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _areaService = areaService;
            _mapper = mapper;
        }
        public async Task InsertAsync(CollectionViewModel viewModel)
        {
            var area = await _areaService.GetByIdAsync(viewModel.SelectedArea);

            var collection = new Collection()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                FileId = viewModel.FileId,
                ContentTypes = _mapper.Map<List<ContentType>>(viewModel.ContentTypes)
            };

            await _unitOfWork.CollectionRepository.AddAsync(collection);

            area.Collections.Add(collection);
            await _areaService.UpdateAsync(_mapper.Map<AreaViewModel>(area));
        }

        public async Task UpdateAsync(CollectionViewModel viewModel)
        {
            var collection = await GetByIdAsync(viewModel.Id);

            if (collection is not null)
            {
                collection.Name = viewModel.Name;
                collection.UpdatedOn = DateTime.Now;
                collection.Description = viewModel.Description;

                if (viewModel.Area.Id != collection.Area.Id)
                {
                    collection.Area = await _areaService.GetByIdAsync(viewModel.SelectedArea);
                }

                await _unitOfWork.CollectionRepository.SaveChangeAsync();
            }
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            await _unitOfWork.CollectionRepository.RemoveAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Collection> GetByIdAsync(long id)
        {
            return await Task.Run(() =>
                _unitOfWork.CollectionRepository.Query()
                .Where(c => c.Id == id)
                .Include(c => c.Area)
                .Include(i => i.Items)
                .Include(c => c.ContentTypes)
                .FirstOrDefault());
        }

        public async Task<IQueryable<Collection>> GetAllAsync()
        {
            return await Task.Run(() => _unitOfWork
                .CollectionRepository.Query()
                .Include(i => i.Area)
                .Include(c => c.ContentTypes));
        }

        public async Task<IQueryable<Collection>> GetAllByAreaIdAsync(long areaId)
        {
            var area = await _areaService.GetByIdAsync(areaId);
            return (await GetAllAsync()).Where(a => a.Area.Id == area.Id);
        }
    }
}