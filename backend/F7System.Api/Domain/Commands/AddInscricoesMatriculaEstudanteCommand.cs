using System;
using System.Collections.Generic;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands
{
    public class AddInscricoesMatriculaEstudanteCommand: BaseCommand
    {
        public Guid MatriculaId { get; set; }
        public IList<Guid> TurmaIds { get; set; } = new List<Guid>();
    }
}