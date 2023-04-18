using AutoMapper;
using Domain.Entities;
using HotDesks.Api.Dto;
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
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
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
                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest("User registration failed");
                }
                _logger.LogInformation($"Registration of {userDto.Email} succesfull.");
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in {nameof(Register)}.\n {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
