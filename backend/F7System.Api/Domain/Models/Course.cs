using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Course
    {
        public IList<Discipline> MandatoryDisciplines { get; set; }
        public IList<Discipline> ElectiveDisciplines { get; set; }
    }
}