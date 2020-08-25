using System;
using System.Linq;
using System.Security;
using F7System.Api.Domain.Commands;
using F7System.Api.Domain.Services;
using F7System.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7System.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public IActionResult GetPagedUsers(string? searchText, int page, int limit)
        {
            var query = _userService.GetAll();

            if (searchText != null)
            {
                query = query.Where(x => x.Name.Contains(searchText));
            }

            query = query.Skip((page - 1) * limit).Take(limit);
            
            return Ok(query);
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel loginModel)
        {
            var user = _userService.Authenticate(loginModel);
            return Ok(new { Id = user.UserId, user.Username, user.Token });
        }
        
        [HttpPost("create")]
        public IActionResult Create([FromBody] LoginModel loginModel)
        {
            _userService.Create(loginModel);
            return Ok();
        }
        
        [HttpPost("update")]
        public IActionResult Update([FromBody] UpdateUserCommand cmd)
        {
            _userService.Update(cmd);
            return Ok();
        }
        
        [HttpPost("delete")]
        public IActionResult Delete(Guid userId)
        {
            _userService.Delete(userId);
            return Ok();
        }
    }
}