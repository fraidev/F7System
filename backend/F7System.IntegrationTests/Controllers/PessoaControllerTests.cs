using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using F7System.Api.Domain.Commands;
using F7System.Api.Domain.Commands.Estudante;
using F7System.Api.Domain.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            
            var student = _f7DbContext.PessoaUsuarioDbSet.FirstOrDefault(x => x.Username == cmd.Username);

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
            
            var response = await DoPostRequest("/Pessoa/AlterarPessoa", cmd);
            
            var estudante = _f7DbContext.PessoaUsuarioDbSet.FirstOrDefault(x => x.Id == cmd.Id);
            
            estudante.Should().NotBeNull();
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            estudante.Nome.Should().Be(cmd.Nome);
            estudante.Username.Should().Be(cmd.Username);
            estudante.CPF.Should().Be(cmd.CPF);
            estudante.DataNascimento.Should().Be(cmd.DataNascimento);
        }

        [Fact]
        public async Task DeletaEstudante()
        {
            var cmdCriaEstudante = CriarComandoDeCriarEstudante();
            await CriarEstudante(cmdCriaEstudante);
            
            
            var cmd = new DeletarPessoaCommand()
            {
                Id = cmdCriaEstudante.Id
            };
            
            var response = await DoPostRequest("/Pessoa/DeletarPessoa", cmd);
            
            var estudante = _f7DbContext.PessoaUsuarioDbSet.FirstOrDefault(x => x.Id == cmd.Id);
            
            estudante.Should().BeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task AdicionarMatriculaEstudante()
        {
            var cmdCriaEstudante = CriarComandoDeCriarEstudante();
            await CriarEstudante(cmdCriaEstudante);

            var cmd = CriaComandoMatricula(cmdCriaEstudante.Id);
            var response = await CriaMatricula(cmd);
            
            var estudante = _f7DbContext.PessoaUsuarioDbSet
                .Include(x => x.Matriculas)
                .FirstOrDefault(x => x.Id == cmd.PessoaId);
            
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            estudante.Should().NotBeNull();
            estudante.Matriculas.Should().HaveCount(1);
            estudante.Matriculas.First().Curso.Id.Should().Be(cmd.CursoId);
        }
        
        [Fact]
        public async Task AdicionaInscricoesEmMatriculaDeEstudante()
        {
            var cmdCriaEstudante = CriarComandoDeCriarEstudante();
            await CriarEstudante(cmdCriaEstudante);

            var cmdMatricula = CriaComandoMatricula(cmdCriaEstudante.Id);
            await CriaMatricula(cmdMatricula);
            
            var curso = _f7DbContext.CursoDbSet.First();

            var turma = _f7DbContext.TurmaDbSet
                .Include(x => x.Disciplina)
                .Include(x => x.Horarios)
                .Include(x => x.Semestre)
                .Include(x => x.Professor).First();
            
            var cmd = new AddInscricoesMatriculaEstudanteCommand()
            {
                MatriculaId = cmdMatricula.MatriculaId,
                TurmaIds = new[]{turma.Id} 
            };

            await DoPostRequest("/Pessoa/AddInscricoesMatriculaEstudante", cmd);
            
            var estudante = _f7DbContext.PessoaUsuarioDbSet
                .Include(x => x.Matriculas).ThenInclude(x => x.Inscricoes)
                .FirstOrDefault(x => x.Id == cmdCriaEstudante.Id);
            
            estudante.Should().NotBeNull();
            estudante?.Matriculas.Should().HaveCount(1);
            estudante?.Matriculas.First().Curso.Id.Should().Be(cmdMatricula.CursoId);
            estudante?.Matriculas.First().Inscricoes.Should().HaveCount(1);
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
           return await DoPostRequest("/Pessoa/CriarPessoa", cmd);
        }

        private AddMatriculaEstudanteCommand CriaComandoMatricula(Guid pessoaId)
        {
            var curso = _f7DbContext.CursoDbSet.First();
            var cmd = new AddMatriculaEstudanteCommand()
            {
                MatriculaId = Guid.NewGuid(),
                PessoaId = pessoaId,
                CursoId = curso.Id
            };
            return cmd;
        }
        
        private Task<HttpResponseMessage> CriaMatricula(AddMatriculaEstudanteCommand cmd)
        {
            return DoPostRequest("/Pessoa/AddMatriculaEstudante", cmd);
        }
    }
}