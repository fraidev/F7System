using System;

namespace F7System.Api.Domain.Models
{
    public class Classroom
    {
        public Guid Id { get; set; }
        public Discipline Discipline { get; set; }
        public Teacher Teacher { get; set; }
    }
}