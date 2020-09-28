using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Student
{
    public class ChangeStudentCommand: BaseCommand
    {
        public string Name { get; set; }
        public Guid StudentId { get; set; }
        public string Username { get; set; }
    }
}