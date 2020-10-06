using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Estudante
{
    public class DeletarPessoaCommand: BaseCommand
    {
        public Guid Id { get; set; }
    }
}