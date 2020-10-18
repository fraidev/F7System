using System;
using System.Globalization;

namespace F7System.Api.Domain.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string DiaDaSemana => new CultureInfo("pt-br").DateTimeFormat.GetDayName(DayOfWeek);
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}