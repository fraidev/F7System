using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F7System.Api.Domain.Commands.Matricula;
using F7System.Api.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace F7System.Api.Domain.CommandHandlers
{
    public class MatriculaCommandHandler: 
        IRequestHandler<AtivarMatriculaCommand>
    {
        private readonly F7DbContext _f7DbContext;

        public MatriculaCommandHandler(F7DbContext f7DbContext)
        {
            _f7DbContext = f7DbContext;
        }
        
        public Task<Unit> Handle(AtivarMatriculaCommand request, CancellationToken cancellationToken)
        {
            var matricula = _f7DbContext.MatriculaDbSet
                .Include(x => x.PessoaUsuario).ThenInclude(x => x.Matriculas)
                .FirstOrDefault(x => x.Id == request.MatriculaId);

            if (matricula != null)
            {
                foreach (var pessoaUsuarioMatricula in matricula.PessoaUsuario.Matriculas)
                {
                    pessoaUsuarioMatricula.Ativo = false;
                }

                matricula.Ativo = true;
            }

            return Unit.Task;
        }
    }
}