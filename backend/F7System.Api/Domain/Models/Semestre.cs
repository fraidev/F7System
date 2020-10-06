using System;

namespace F7System.Api.Domain.Models
{
    public class Semestre
    {
        public Guid Id { get; set; }
        public int Ano { get; set; }
        public bool SegundoSemestre { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}