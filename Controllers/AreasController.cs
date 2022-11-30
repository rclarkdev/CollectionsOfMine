using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollectionsOfMine.Services;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;

namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly ILogger<AreasController> _logger;
        private readonly IMapper _mapper;
        private readonly IAreaService _areaService;

        public AreasController(IMapper mapper,
            ILogger<AreasController> logger,
            IAreaService areaService)
        {
            _logger = logger;
            _mapper = mapper;
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAreas()
        {
            try
            {
                _logger.LogInformation("hello");

                var areas = await _areaService.GetAllAsync();

                var areaViewModels = _mapper.Map<List<Area>, List<AreaViewModel>>(areas.ToList());

                return Ok(areaViewModels);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
           
        }

        [HttpGet("GetById/{areaId}")]
        public async Task<IActionResult> GetById(long areaId)
        {
            var area = await _areaService.GetByIdAsync(areaId);

            var areaViewModel = _mapper.Map<AreaViewModel>(area);

            return Ok(areaViewModel);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var area = await _areaService.GetByName(name);

            var areaViewModel = _mapper.Map<AreaViewModel>(area);

            return Ok(areaViewModel);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(AreaViewModel areaViewModel)
        {
            var area = await _areaService.UpdateAsync(areaViewModel);

            _mapper.Map(area, areaViewModel);

            return Ok(areaViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AreaViewModel areaViewModel)
        {
            var area = _mapper.Map<Area>(areaViewModel);

            await _areaService.InsertAsync(area);

            return Ok();
        }

        [HttpDelete("{areaId}")]
        public async Task<IActionResult> Delete(long areaId)
        {
            var area = await _areaService.GetByIdAsync(areaId);

            await _areaService.DeleteAsync(area);

            return Ok();
        }
    }
}
