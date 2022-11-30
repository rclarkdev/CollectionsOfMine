using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollectionsOfMine.Services;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;


namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ILogger<CollectionsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICollectionService _collectionService;

        public CollectionsController(IMapper mapper,
            ILogger<CollectionsController> logger,
            ICollectionService collectionService)
        {
            _logger = logger;
            _mapper = mapper;
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {
            var collections = await _collectionService.GetAllAsync();

            var collectionViewModels = _mapper.Map<List<Collection>, List<CollectionViewModel>>(collections.ToList());
            
            return Ok(collectionViewModels);
        }

        [HttpGet("GetById/{collectionId}")]
        public async Task<IActionResult> GetById(long collectionId)
        {
            var collection = await _collectionService.GetByIdAsync(collectionId);

            var collectionViewModel = _mapper.Map<CollectionViewModel>(collection);

            if (collection.Area is not null)
                collectionViewModel.SelectedArea = collection.Area.Id;

            return Ok(collectionViewModel);
        }

        [HttpGet("AreaCollections/{areaId}")]
        public async Task<IActionResult> AreaCollections(long areaId)
        {
            var collections = await _collectionService.GetAllByAreaIdAsync(areaId);

            var collectionViewModels = _mapper.Map<List<CollectionViewModel>>(collections);

            return Ok(collectionViewModels);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(CollectionViewModel collectionViewModel)
        {
            await _collectionService.UpdateAsync(collectionViewModel);

            return Ok(collectionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CollectionViewModel collectionViewModel)
        {
            await _collectionService.InsertAsync(collectionViewModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _collectionService.DeleteAsync(id);

            return Ok();
        }
    }
}
