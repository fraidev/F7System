using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Matricula
{
    public class AtivarMatriculaCommand: BaseCommand
    {
        public Guid MatriculaId { get; set; }
    }
}