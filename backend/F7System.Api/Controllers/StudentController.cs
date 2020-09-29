using System.Linq;
using F7System.Api.Domain.Commands.Student;
using F7System.Api.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace F7System.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly F7DbContext _f7DbContext;

        public StudentController(IMediator mediator, F7DbContext f7DbContext)
        {
            _mediator = mediator;
            _f7DbContext = f7DbContext;
        }
        
        [HttpGet("")]
        public IActionResult GetStudents()
        {
            var students = _f7DbContext.StudentDbSet.ToList();
            return Ok(students);
        }
        
        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody] CreateStudentCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
        
        [HttpPost("ChangeStudent")]
        public IActionResult ChangeStudent([FromBody] ChangeStudentCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
        
        [HttpPost("DeleteStudent")]
        public IActionResult DeleteStudent([FromBody] DeleteStudentCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
    }
}