using F7System.Api.Domain.Commands;
using F7System.Api.Domain.Services;
using F7System.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7System.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(F7DbContext f7DbContext, IUserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel loginModel)
        {
            var user = _userService.Authenticate(loginModel);
            return Ok(new { Id = user.UserId, user.Username, user.Token });
        }
    }
}