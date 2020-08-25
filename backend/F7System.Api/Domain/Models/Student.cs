using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Period ActualPeriod { get; set; }
        public IList<Period> LastPeriods { get; set; }
    }
}