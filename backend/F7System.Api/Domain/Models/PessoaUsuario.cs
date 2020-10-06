using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using F7System.Api.Domain.Enums;

namespace F7System.Api.Domain.Models
{
    public class PessoaUsuario
    {
        public PessoaUsuario()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public Perfil Perfil { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public Matricula Matricula { get; set; }
        public IList<Turma> Turmas { get; set; } = new List<Turma>();
    }
}