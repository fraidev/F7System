using System;
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
    public class GradeController: ControllerBase
    {
        private IMediator _mediator;
        private F7DbContext _f7DbContext;

        public GradeController(IMediator mediator, F7DbContext f7DbContext)
        {
            _mediator = mediator;
            _f7DbContext = f7DbContext;
        }
        
        [HttpGet("GradesByCurso/{cursoId}")]
        public IActionResult GetGradesByCursoId(Guid cursoId)
        {
            var grade = _f7DbContext.GradeDbSet
                .Include(x => x.Curso)
                .Where(x => x.CursoId == cursoId);
            return Ok(grade);
        }
        
        [HttpGet("")]
        public IActionResult GetGrades()
        {
            var estudantes= _f7DbContext.GradeDbSet
                .Include(x => x.Disciplinas).ToList();
            return Ok(estudantes);
        }
    }
}