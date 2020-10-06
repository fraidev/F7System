using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        public Curso Curso { get; set; }
        public Guid CursoId { get; set; }
        public int Ano { get; set; }
        public IList<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    }
}