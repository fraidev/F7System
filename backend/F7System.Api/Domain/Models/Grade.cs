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
        public IList<SemestreDisciplina> SemestreDisciplinas { get; set; } = new List<SemestreDisciplina>();
    }
}