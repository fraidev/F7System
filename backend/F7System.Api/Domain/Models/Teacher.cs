using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public Period ActualPeriod { get; set; }
        public IList<Period> LastPeriods { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}