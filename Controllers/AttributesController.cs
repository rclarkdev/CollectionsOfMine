using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollectionsOfMine.Services;
using CollectionsOfMine.ViewModels;
using Attribute = CollectionsOfMine.Data.Models.Attribute;

namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributesController : ControllerBase
    {

        private readonly ILogger<AttributesController> _logger;
        private readonly IMapper _mapper;
        private readonly IAttributeService _attributeService;

        public AttributesController(IMapper mapper,
            ILogger<AttributesController> logger,
            IUnitOfWork unitOfWork,
            IAttributeService attributeService)
        {
            _logger = logger;
            _mapper = mapper;
            _attributeService = attributeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributes()
        {
            var attributes = await _attributeService.GetAllAsync();

            var attributeModels = _mapper.Map<List<Attribute>, List<ItemViewModel>>(attributes.ToList());

            return Ok(attributeModels);
        }

        [HttpGet("GetById/{attributeId}")]
        public async Task<IActionResult> Get(long attributeId)
        {
            var attribute = await _attributeService.GetByIdAsync(attributeId);

            var attributeViewModel = _mapper.Map<AttributeViewModel>(attribute);

            return Ok(attributeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AttributeViewModel attributeViewModel)
        {
            var attributeEntity = _mapper.Map<Attribute>(attributeViewModel);

            await _attributeService.InsertAsync(attributeEntity);

            return Ok(_mapper.Map<AttributeViewModel>(attributeEntity));
        }

        [HttpPatch]
        public async Task<IActionResult> Update(AttributeViewModel attributeViewModel)
        {
            var attribute = await _attributeService.GetByIdAsync(attributeViewModel.Id);

            _mapper.Map(attributeViewModel, attribute);

            await _attributeService.UpdateAsync(attributeViewModel);

            return Ok(attributeViewModel);
        }
    }
}
