using System;
using F7System.Api.Infrastructure.CQRS;

namespace F7System.Api.Domain.Commands.Student
{
    public class DeleteStudentCommand: BaseCommand
    {
        public Guid Id { get; set; }
    }
}