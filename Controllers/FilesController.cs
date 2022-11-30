using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CollectionsOfMine.Services;
using CollectionsOfMine.ViewModels;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {

        private readonly ILogger<FilesController> _logger;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FilesController(IMapper mapper,
            ILogger<FilesController> logger,
            IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet("GetById/{fileId}")]
        public async Task<IActionResult> GetById(long fileId)
        {
            var file = await _fileService.GetByIdAsync(fileId);

            var fileViewModel = _mapper.Map<FileViewModel>(file);

            return Ok(fileViewModel);
        }

        [HttpGet]
        public async Task<IEnumerable<FileViewModel>> Get()
        {
            var files = await _fileService.GetAllAsync();
            var filesViewModels = _mapper.Map<List<File>, List<FileViewModel>>(files.ToList());
            return filesViewModels;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileName = $"{file.FileName}_{DateTime.UtcNow}";

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        var newFile = new File()
                        {
                            Bytes = fileBytes,
                            Base64 = Convert.ToBase64String(fileBytes),
                            Name = fileName,
                            Type = file.ContentType,
                            CreatedOn = DateTime.Now
                        };
                        await _fileService.InsertAsync(newFile);
                    }

                    var fileEntity = (await _fileService.GetAllAsync())
                        .Where(o => o.Name == fileName).FirstOrDefault();

                    return Ok(fileEntity.Id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return UnprocessableEntity($"There was an error processing this request. Message: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var file = (await _fileService.GetAllAsync())
                .Where(c => c.Id == id).FirstOrDefault();

            await _fileService.DeleteAsync(file);

            return Ok();
        }
    }
}
