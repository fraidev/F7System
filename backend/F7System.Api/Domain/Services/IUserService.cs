using System;
using System.Linq;
using F7System.Api.Domain.Commands;
using F7System.Api.Domain.Models;
using F7System.Api.Infrastructure.Models;

namespace F7System.Api.Domain.Services
{
    public interface IUserService
    {
        PessoaUsuario Authenticate(LoginModel loginModel);
        IQueryable<PessoaUsuario> GetAll();
        PessoaUsuario GetById(Guid id);
        void GiveAccess(PessoaUsuario pessoaUsuario, LoginModel loginModel);
        void Update(UpdateUserCommand cmd);
        void Delete(Guid id);
        void CreateAdminUserWhenDontHaveManagerUsers();
    }
}