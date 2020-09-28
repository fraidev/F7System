using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Teacher: UserPerson
    {
        public Period ActualPeriod { get; set; }
        public IList<Period> LastPeriods { get; set; }
    }
}