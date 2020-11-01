using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Matricula
    {
        public Guid Id { get; set; }
        public Grade Grade { get; set; }
        public Guid PessoaUsuarioId { get; set; }
        public PessoaUsuario PessoaUsuario { get; set; }
        public List<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
        public bool Ativo { get; set; }
    }
}