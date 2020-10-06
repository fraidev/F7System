using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Estudante
{
    public class AlterarPessoaCommand : BaseCommand
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}