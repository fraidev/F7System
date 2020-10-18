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
    public class MatriculaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly F7DbContext _f7DbContext;

        public MatriculaController(IMediator mediator, F7DbContext f7DbContext)
        {
            _mediator = mediator;
            _f7DbContext = f7DbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetMatriculaById(Guid id)
        {
            var config = _f7DbContext.Configuration;

            var matricula = _f7DbContext.MatriculaDbSet
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.TurmaHorarios).ThenInclude(x => x.Horario)
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Professor)
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Semestre)
                .Include(x => x.Inscricoes)
                .Include(x => x.PessoaUsuario)
                .FirstOrDefault(x => x.Id == id);


            return Ok(matricula);
        }

        [HttpGet("Atual/{id}")]
        public IActionResult GetMatriculaAtualById(Guid id)
        {
            var config = _f7DbContext.Configuration;

            var matricula = _f7DbContext.MatriculaDbSet
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.TurmaHorarios).ThenInclude(x => x.Horario)
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Professor)
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Disciplina)
                .Include(x => x.Grade).ThenInclude(x => x.Disciplinas).ThenInclude(x => x.Turmas)
                .ThenInclude(x => x.Semestre)
                .Include(x => x.Inscricoes)
                .Include(x => x.PessoaUsuario)
                .FirstOrDefault(x => x.Id == id);

            //Somente primeiro semestre atual
            if (matricula != null)
            {
                foreach (var disciplina in matricula.Grade.Disciplinas)
                {
                    disciplina.Turmas = disciplina.Turmas.Where(x => x.Semestre.Id == config.SemestreAtual.Id).ToList();
                }
            }


            return Ok(matricula);
        }
    }
}