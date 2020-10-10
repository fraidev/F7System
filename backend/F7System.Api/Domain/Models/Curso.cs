using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IList<Grade> Grades { get; set; } = new List<Grade>();
    }
}