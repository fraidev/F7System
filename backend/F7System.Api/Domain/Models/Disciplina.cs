using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Disciplina
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Creditos { get; set; }
        public IList<Turma> Turmas { get; set; } = new List<Turma>();
 
        public IList<Disciplina> Prerequisites { get; set; } = new List<Disciplina>();
    }
}