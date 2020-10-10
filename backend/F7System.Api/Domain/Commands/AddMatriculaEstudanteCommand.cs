using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands
{
    public class AddMatriculaEstudanteCommand: BaseCommand
    {
        public Guid PessoaId { get; set; }
        public Guid GradeId { get; set; }
        public Guid MatriculaId { get; set; }
    }
}