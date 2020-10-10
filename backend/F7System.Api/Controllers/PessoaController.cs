using System;
using System.Linq;
using F7System.Api.Domain.Commands;
using F7System.Api.Domain.Commands.Estudante;
using F7System.Api.Domain.Enums;
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
    public class PessoaController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly F7DbContext _f7DbContext;

        public PessoaController(IMediator mediator, F7DbContext f7DbContext)
        {
            _mediator = mediator;
            _f7DbContext = f7DbContext;
        }
        
        [HttpGet("Estudante/{id}")]
        public IActionResult GetEstudanteById(Guid id)
        {
            var estudante = _f7DbContext.PessoaUsuarioDbSet
                .Include(x => x.Matriculas)
                .ThenInclude(x => x.Grade)
                .ThenInclude(x => x.Curso)
                .Include(x => x.Matriculas)
                .ThenInclude(x => x.Inscricoes)
                .FirstOrDefault(x => x.Id == id);
            return Ok(estudante);
        }
        
        [HttpGet("Estudantes")]
        public IActionResult GetEstudantes()
        {
            var estudantes= _f7DbContext.PessoaUsuarioDbSet.Where(x => x.Perfil == Perfil.Estudante).ToList();
            return Ok(estudantes);
        }
        
        [HttpGet("Professores")]
        public IActionResult GetProfessores()
        {
            var professores= _f7DbContext.PessoaUsuarioDbSet.Where(x => x.Perfil == Perfil.Professor).ToList();
            return Ok(professores);
        }
        //
        //
        // [HttpGet("Matricula")]
        // public IActionResult GetProfessores()
        // {
        //     var professores= _f7DbContext.PessoaUsuarioDbSet.Where(x => x.Perfil == Perfil.Professor).ToList();
        //     return Ok(professores);
        // }
        
        [HttpPost("CriarPessoa")]
        public IActionResult CriarPessoa([FromBody] CriarPessoaCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
        
        [HttpPost("AlterarPessoa")]
        public IActionResult AlterarPessoa([FromBody] AlterarPessoaCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
        
        [HttpPost("DeletarPessoa")]
        public IActionResult DeletarPessoa([FromBody] DeletarPessoaCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
        
        [HttpPost("AddMatriculaEstudante")]
        public IActionResult AddMatriculaEstudante([FromBody] AddMatriculaEstudanteCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
        
        [HttpPost("AddInscricoesMatriculaEstudante")]
        public IActionResult AddInscricoesMatriculaEstudante([FromBody] AddInscricoesMatriculaEstudanteCommand cmd)
        {
            _mediator.Send(cmd);
            return Ok();
        }
    }
}