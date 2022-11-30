using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CollectionsOfMine.Services;
using CollectionsOfMine.Settings;
using CollectionsOfMine.ViewModels;

namespace CollectionsOfMine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var users = _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdAsync(long id)
        {
            var user = _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateOrUpdateUser model)
        {
            var users = await _userService.CreateAsync(model);
            return Ok(users);
        }


        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateRequest model)
        {
            var users = await _userService.AuthenticateAsync(model);
            return Ok(users);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserAsync(CreateOrUpdateUser model)
        {
            await _userService.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAsnyc(int id)
        {
            _userService.DeleteAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}