using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollectionsOfMine.Services;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;
using Attribute = CollectionsOfMine.Data.Models.Attribute;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {

        private readonly ILogger<ItemsController> _logger;
        private readonly IItemService _itemService;
        private readonly IFileService _fileService;
        private readonly ICollectionService _collectionService;
        private readonly IAttributeService _attributeService;
        private readonly IMapper _mapper;

        public ItemsController(ILogger<ItemsController> logger,
            IItemService itemService,
            IFileService fileService,
            ICollectionService collectionService,
            IAttributeService attributeService,
            IMapper mapper)
        {
            _logger = logger;
            _itemService = itemService;
            _fileService = fileService;
            _collectionService = collectionService;
            _attributeService = attributeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _itemService.GetAllAsync();

            var areaViewModels = _mapper.Map<List<Item>, List<ItemViewModel>>(items.ToList());

            return Ok(areaViewModels);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = (await _itemService.GetAllAsync())
                .Where(o => o.Id == id).FirstOrDefault();

            var itemViewModel = _mapper.Map<ItemViewModel>(item);

            if (item.Collection is not null)
                itemViewModel.SelectedCollection = item.Collection.Id;

            return Ok(itemViewModel);
        }

        [HttpGet("CollectionItems/{collectionId}")]
        public async Task<IActionResult> CollectionItems(long collectionId)
        {
            var items = await _itemService.GetAllByCollectionIdAsync(collectionId);

            var itemViewModels = _mapper.Map<List<ItemViewModel>>(items.ToList());

            return Ok(itemViewModels);
        }


        [HttpPost]
        public async Task<IActionResult> Post(ItemViewModel itemViewModel)
        {
            try
            {
                var collection = (await _collectionService
                        .GetByIdAsync(itemViewModel.Collection.Id));

                var files = new List<File>();

                if (itemViewModel.Files is not null)
                {
                    foreach (var fileViewModel in itemViewModel.Files)
                    {
                        var file = (await _fileService
                            .GetByIdAsync(fileViewModel.Id));

                        if (file is not null) files.Add(file);
                    }
                }

                var attributes = new List<Attribute>();

                if (itemViewModel.SelectedAttributes is not null)
                {
                    foreach (var selectedAttribute in itemViewModel.SelectedAttributes)
                    {
                        var attribute = (await _attributeService
                            .GetByIdAsync(selectedAttribute.Id));

                        if (attribute is not null) attributes.Add(attribute);
                    }
                }

                var itemEntity = new Item()
                {
                    Name = itemViewModel.Name,
                    Description = itemViewModel.Description,
                    Source = itemViewModel.Source,
                    HtmlContent = itemViewModel.HtmlContent,
                    IframeSrc = itemViewModel.IframeSrc
                };

                await _itemService.InsertAsync(itemEntity);

                itemEntity.Files = files;
                itemEntity.Collection = collection;
                itemEntity.Attributes = attributes;

                await _itemService.UpdateAsync();

                return Ok(itemViewModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPatch]
        public async Task<IActionResult> Update(ItemViewModel itemViewModel)
        {
            var itemEntity = (await _itemService.GetAllAsync())
                .Where(o => o.Id == itemViewModel.Id).FirstOrDefault();

            if (itemEntity is not null)
            {
                itemViewModel.CreatedOn = itemEntity.CreatedOn;
                itemViewModel.UpdatedOn = DateTime.Now;
                _mapper.Map(itemViewModel, itemEntity);

                var collection = (await _collectionService
                    .GetByIdAsync(itemViewModel.Collection.Id));

                var files = new List<File>();

                if (itemViewModel.Files is not null && itemViewModel.Files.Any())
                {
                    foreach (var fileViewModel in itemViewModel.Files)
                    {
                        var file = (await _fileService
                            .GetByIdAsync(fileViewModel.Id));

                        if (file is not null) files.Add(file);
                    }
                }

                itemEntity.Files = files;
                itemEntity.Collection = collection;

                await _itemService.UpdateAsync();
            }

            _mapper.Map(itemEntity, itemViewModel);

            return Ok(itemViewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = (await _itemService.GetAllAsync())
                .Where(c => c.Id == id).FirstOrDefault();

            await _itemService.DeleteAsync(item);

            return Ok();
        }
    }
}
