using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7System.Api.Controllers
{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController: ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}