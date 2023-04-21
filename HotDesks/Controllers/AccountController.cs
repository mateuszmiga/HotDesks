using AutoMapper;
using Domain.Entities;
using HotDesks.Api.Dto;
using HotDesks.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotDesks.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;        
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(IMapper mapper, UserManager<User> userManager, ILogger<AccountController> logger, IAuthManager authManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration attempt for: {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<User>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    return BadRequest("User registration failed");
                }
                _logger.LogInformation($"Registration of {userDto.Email} succesfull.");
                await _userManager.AddToRolesAsync(user, userDto.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in {nameof(Register)}.\n {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
        {
            _logger.LogInformation($"Login attempt for: {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {                
                if (!await _authManager.ValidateUser(userDto))
                {
                    _logger.LogWarning($"{userDto.Email} tried to login, but credentials were incorrect");
                    return Unauthorized(userDto);
                }

                return Accepted(new { Token = await _authManager.GetToken()});
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in {nameof(Login)}.\n {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
