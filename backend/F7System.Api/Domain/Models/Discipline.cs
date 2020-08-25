using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Discipline
    {
        public Guid Id { get; set; }
        public IList<Discipline> Prerequisites { get; set; } = new List<Discipline>();
        public DisciplineTime DisciplineTime { get; set; }
    }

    public class DisciplineTime
    {
        public Guid Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}