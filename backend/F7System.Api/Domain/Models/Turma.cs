using System;
using System.Collections.Generic;
using System.Linq;

namespace F7System.Api.Domain.Models
{
    public class Turma
    {
        public Guid Id { get; set; }
        public string Sala { get; set; }
        public Disciplina Disciplina { get; set; }
        public PessoaUsuario Professor { get; set; }
        public Semestre Semestre { get; set; }
        // public IList<PessoaUsuario> Estudantes { get; set; }

        public IList<Horario> Horarios => TurmaHorarios.Select(x => x.Horario).ToList();
        
        public IList<TurmaHorario> TurmaHorarios { get; set; } = new List<TurmaHorario>();
        // public IList<Inscricao> Inscricoes { get; set; } = new List<Inscricao>();
    }
}