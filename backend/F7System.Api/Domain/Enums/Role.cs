using System;

namespace F7System.Api.Domain.Models
{
    [Flags]
    public enum Role
    {
        Administrator = 0,
        Student = 1,
        Teacher = 2,
        Secretary = 4,
    }
}