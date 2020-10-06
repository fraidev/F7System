using System;
using System.Text.Json.Serialization;
using F7System.Api.Domain.Enums;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Estudante
{
    public class CriarPessoaCommand: BaseCommand
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public Perfil Perfil { get; set; }
    }
}