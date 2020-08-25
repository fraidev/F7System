using System.Threading;
using System.Threading.Tasks;
using F7System.Api.Domain.Commands;
using MediatR;

namespace F7System.Api.Domain.CommandHandlers
{
    public class StudentCommandHandler:
        IRequestHandler<AddDisciplinesToStudentCommand>
    {
        public Task<Unit> Handle(AddDisciplinesToStudentCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}