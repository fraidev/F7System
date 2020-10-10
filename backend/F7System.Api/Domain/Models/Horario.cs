using System;

namespace F7System.Api.Domain.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}