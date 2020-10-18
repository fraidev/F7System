using System;

namespace F7System.Api.Domain.Models
{
    public class Configuration
    {
        public int Id { get; set; } = 1;
        public decimal NotaMedia { get; set; } = 6;

        public Semestre SemestreAtual { get; set; }
    }
}