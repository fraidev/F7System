using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using F7System.Api.Domain.Commands.Estudante;
using F7System.Api.Domain.Models;
using F7System.Api.Domain.Services;
using F7System.Api.Infrastructure.Models;
using F7System.Api.Infrastructure.Persistence;
using MediatR;

namespace F7System.Api.Domain.CommandHandlers
{
    public class PessoaCommandHandler:
        IRequestHandler<CriarPessoaCommand>,
        IRequestHandler<AlterarPessoaCommand>,
        IRequestHandler<DeletarPessoaCommand>
        
    {
        private readonly IUserService _userService;
        private readonly F7DbContext _f7DbContext;

        public PessoaCommandHandler(IUserService userService, F7DbContext f7DbContext)
        {
            _userService = userService;
            _f7DbContext = f7DbContext;
        }
        public Task<Unit> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
        {
            var student = new PessoaUsuario()
            {
                Id = request.Id,
                Nome = request.Nome,
                CPF = request.CPF,
                DataNascimento = request.DataNascimento,
                Perfil = request.Perfil
            };

            _f7DbContext.Add(student);
            _f7DbContext.SaveChanges();
            
            var login = new LoginModel()
            {
                Username = request.Username,
                Password = request.Password
            };
            
            _userService.GiveAccess(student, login);
            
            return Unit.Task;
        }

        public Task<Unit> Handle(AlterarPessoaCommand request, CancellationToken cancellationToken)
        {
            var student = _f7DbContext.PessoaUsuarioDbSet.FirstOrDefault(x => x.Id == request.Id);

            student.Nome = request.Nome;
            student.Username = request.Username;
            student.CPF = request.CPF;
            student.DataNascimento = request.DataNascimento;
            student.Perfil = student.Perfil;
            _f7DbContext.SaveChanges();

            return Unit.Task;
        }

        public Task<Unit> Handle(DeletarPessoaCommand request, CancellationToken cancellationToken)
        {
            var estudante = _f7DbContext.PessoaUsuarioDbSet.FirstOrDefault(x => x.Id == request.Id);
            _f7DbContext.Remove(estudante);
            _f7DbContext.SaveChanges();
            
            return Unit.Task;
        }
    }
}