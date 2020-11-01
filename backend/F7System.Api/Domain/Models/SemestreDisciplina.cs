using System;

namespace F7System.Api.Domain.Models
{
    public class SemestreDisciplina
    {
        public Guid Id { get; set; }
        public int Semestre { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}