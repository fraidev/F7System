using System;

namespace F7System.Api.Domain.Models
{
    public class TurmaHorario
    {
        public int HorarioId { get; set; }
        public Horario Horario { get; set; }
        public Guid TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}