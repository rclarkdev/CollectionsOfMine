using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollectionsOfMine.Services;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentTypesController : ControllerBase
    {
        private readonly ILogger<ContentTypesController> _logger;
        private readonly IMapper _mapper;
        private readonly IContentTypeService _contentTypeService;

        public ContentTypesController(IMapper mapper,
            ILogger<ContentTypesController> logger,
            IContentTypeService contentTypeService)
        {
            _logger = logger;
            _mapper = mapper;
            _contentTypeService = contentTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContentTypes()
        {
            var contentTypes = await _contentTypeService.GetAllAsync();

            var contentTypeViewModels = _mapper.Map<List<ContentType>, List<ContentTypeViewModel>>(contentTypes.ToList());

            return Ok(contentTypeViewModels);
        }

        [HttpGet("GetById/{contentTypeId}")]
        public async Task<IActionResult> GetById(long contentTypeId)
        {
            var contentType = await _contentTypeService.GetByIdAsync(contentTypeId);

            var contentTypeViewModel = _mapper.Map<ContentTypeViewModel>(contentType);

            return Ok(contentTypeViewModel);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var contentType = await _contentTypeService.GetByName(name);

            var contentTypeViewModel = _mapper.Map<ContentTypeViewModel>(contentType);

            return Ok(contentTypeViewModel);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(ContentTypeViewModel contentTypeViewModel)
        {
            var contentType = await _contentTypeService.UpdateAsync(contentTypeViewModel);

            _mapper.Map(contentType, contentTypeViewModel);

            return Ok(contentTypeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContentTypeViewModel contentTypeViewModel)
        {
            var contentType = _mapper.Map<ContentType>(contentTypeViewModel);

            await _contentTypeService.InsertAsync(contentType);

            return Ok();
        }

        [HttpDelete("{contentTypeId}")]
        public async Task<IActionResult> Delete(long contentTypeId)
        {
            var contentType = await _contentTypeService.GetByIdAsync(contentTypeId);

            await _contentTypeService.DeleteAsync(contentType);

            return Ok();
        }
    }
}
