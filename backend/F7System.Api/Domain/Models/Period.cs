using System;
using System.Collections.Generic;

namespace F7System.Api.Domain.Models
{
    public class Period
    {
        public Guid Id { get; set; }
        public IList<Discipline> Disciplines { get; set; }
    }
}