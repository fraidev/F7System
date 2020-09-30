using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Student
{
    public class CreateStudentCommand: BaseCommand
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string Name { get; set; }
        public string SocialSecurityNumber { get; set; }
        public DateTime Birth { get; set; }

    }
}