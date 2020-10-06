using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Matricula
    {
        public Guid Id { get; set; }
        public Curso Curso { get; set; }
        public IList<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
        public Guid PessoaUsuarioId { get; set; }
        public PessoaUsuario PessoaUsuario { get; set; }
    }
}