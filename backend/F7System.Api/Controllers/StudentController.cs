using F7System.Api.Domain.Commands.Student;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7System.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController: ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudentCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
    }
}