using System.Linq;
using F7System.Api.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F7System.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CursoController: ControllerBase
    {
        private IMediator _mediator;
        private F7DbContext _f7DbContext;

        public CursoController(IMediator mediator, F7DbContext f7DbContext)
        {
            _mediator = mediator;
            _f7DbContext = f7DbContext;
        }
        
        [HttpGet("")]
        public IActionResult GetCursos()
        {
            var estudantes= _f7DbContext.CursoDbSet
                .Include(x => x.Grades)
                .ThenInclude(x => x.Disciplinas).ToList();
            return Ok(estudantes);
        }
    }
}