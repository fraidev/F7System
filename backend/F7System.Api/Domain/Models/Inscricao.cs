using System;
using System.Linq;

namespace F7System.Api.Domain.Models
{
    public class Inscricao
    {
        public Guid Id { get; set; }
        public DateTime DataInscricao { get; set; }
        public decimal P1 { get; set; }
        public decimal P2 { get; set; }
        public decimal P3 { get; set; }
        public decimal VF { get; set; }
        public decimal NotaFinal => Math.Round(new [] {(P1 + P2) / 2, (P1 + P3) / 2, (P2 + P3) / 2, VF}.Max(), 1);
        public Matricula Matricula { get; set; }
        public Turma Turma { get; set; }
        public bool Completa { get; set; }
        public Guid MatriculaId { get; set; }
    }
}