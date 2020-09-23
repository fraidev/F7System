using System;
using System.Threading;
using System.Threading.Tasks;
using F7System.Api.Domain.Commands.Student;
using F7System.Api.Domain.Models;
using F7System.Api.Domain.Services;
using F7System.Api.Infrastructure.Persistence;
using MediatR;

namespace F7System.Api.Domain.CommandHandlers
{
    public class StudentCommandHandler:
        IRequestHandler<CreateStudentCommand>
    {
        private readonly IUserService _userService;
        private readonly F7DbContext _f7DbContext;

        public StudentCommandHandler(IUserService userService, F7DbContext f7DbContext)
        {
            _userService = userService;
            _f7DbContext = f7DbContext;
        }
        public Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
            };

            _f7DbContext.Add(student);
            _f7DbContext.SaveChanges();
            
            // var user = _userService.Authenticate(loginModel);

            return Unit.Task;
        }
    }
}