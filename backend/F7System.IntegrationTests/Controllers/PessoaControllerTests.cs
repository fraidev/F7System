using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using F7System.Api.Domain.Commands.Estudante;
using FluentAssertions;
using Xunit;

namespace F7System.IntegrationTests.Controllers
{
    public class PessoaControllerTests : BaseIntegrationTest
    {
        [Fact]
        public async Task CriarPessoaEstudante()
        {
            var cmd = CriarComandoDeCriarEstudante();
            var response = await CriarEstudante(cmd);
            
            var student = _f7DbContext.PessoaUsuarioDbSet.First(x => x.Username == cmd.Username);

            student.Should().NotBeNull();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            student.Nome.Should().Be(cmd.Nome);
            student.Username.Should().Be(cmd.Username);
        }
        
        [Fact]
        public async Task AlterarPessoaEstudante()
        {
            var cmdCriaEstudante = CriarComandoDeCriarEstudante();
            await CriarEstudante(cmdCriaEstudante);
            
            
            var cmd = new AlterarPessoaCommand()
            {
                Id = cmdCriaEstudante.Id,
                Username = Fixture.Create<string>(),
                Nome = Fixture.Create<string>(),
                CPF = Fixture.Create<string>(),
                DataNascimento = Fixture.Create<DateTime>()
            };
            
            var response = await DoPostRequest("/Estudante/AlterarEstudante", cmd);
            
            var estudante = _f7DbContext.PessoaUsuarioDbSet.First(x => x.Id == cmd.Id);
            
            estudante.Should().NotBeNull();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            estudante.Nome.Should().Be(cmd.Nome);
            estudante.Username.Should().Be(cmd.Username);
            estudante.CPF.Should().Be(cmd.CPF);
            estudante.DataNascimento.Should().Be(cmd.DataNascimento);
        }

        [Fact]
        public void DeletaEstudante()
        {
            
        }

        private CriarPessoaCommand CriarComandoDeCriarEstudante()
        {
            
            return new CriarPessoaCommand()
            {
                Id = Guid.NewGuid(),
                Username = Fixture.Create<string>(),
                Password = Fixture.Create<string>(),
                Nome = Fixture.Create<string>(),
                CPF = Fixture.Create<string>(),
                DataNascimento = Fixture.Create<DateTime>()
            };
        }

        private async Task<HttpResponseMessage> CriarEstudante(CriarPessoaCommand cmd)
        {
           return await DoPostRequest("/Estudante/CriarEstudante", cmd);
        }
    }
}