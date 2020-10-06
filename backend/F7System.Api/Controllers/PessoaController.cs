using System.Linq;
using F7System.Api.Domain.Commands.Estudante;
using F7System.Api.Domain.Enums;
using F7System.Api.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}