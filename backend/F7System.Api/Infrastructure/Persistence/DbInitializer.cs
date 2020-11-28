using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Castle.Core.Internal;
using F7System.Api.Domain.Commands.Estudante;
using F7System.Api.Domain.Enums;
using F7System.Api.Domain.Models;
using F7System.Api.Domain.Services;
using F7System.Api.Infrastructure.Persistence;
using MediatR;

namespace ContosoUniversity.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly IUserService _userService;
        private readonly F7DbContext _f7DbContext;
        private readonly IMediator _mediator;

        public DbInitializer(IUserService userService, F7DbContext f7DbContext, IMediator mediator)
        {
            _userService = userService;
            _f7DbContext = f7DbContext;
            _mediator = mediator;
        }

        public void Initialize()
        {
            _userService.CreateAdminUserWhenDontHaveManagerUsers();

            var criarEstudante = new CriarPessoaCommand()
            {
                Perfil = Perfil.Estudante,
                Id = Guid.NewGuid(),
                Nome = "Felipe",
                Username = "felipe",
                Password = "felipe", 
                CPF = "12345678901",
                DataNascimento = DateTime.Now.AddYears(-23)
            };

            _mediator.Send(criarEstudante);

            var semestres = SalvaSemestres();
            var horarios = SalvaHorarios();

            var computacaoCienciaProfissao = CriarSemestreDisciplina("Computação, Ciência e Profissão", 40, 1);
            var metodologiaCientifica = CriarSemestreDisciplina("Metodologia Científica", 40, 1);
            var calculo = CriarSemestreDisciplina("Calculo", 80, 1);
            var calcVelotorialGeometriaAnaliticaPlana = CriarSemestreDisciplina("Calc. Vetorial e Geometria Analítica Plana", 80, 1);
            var algoritmos = CriarSemestreDisciplina("Algoritmos", 80, 1);
            var logicaMatematica = CriarSemestreDisciplina("Introdução Logica Matematica", 80, 1);
            
            var calculo2 = CriarSemestreDisciplina("Calculo 2", 80, 2, calculo);
            var calcVetorialGeometriaAnaliticaEspacial = CriarSemestreDisciplina("Calc. Vetorial e Geometria Analítica Espacial", 80, 2, calcVelotorialGeometriaAnaliticaPlana);
            
            var grade = new Grade()
            {
                Id = Guid.NewGuid(),
                Ano = 2020,
                SemestreDisciplinas = new List<SemestreDisciplina>()
                {
                    computacaoCienciaProfissao,
                    metodologiaCientifica,
                    calculo,
                    calcVelotorialGeometriaAnaliticaPlana,
                    algoritmos,
                    logicaMatematica,
                    
                    calculo2,
                    calcVetorialGeometriaAnaliticaEspacial
                }
            };
            _f7DbContext.Add(grade);

            var curso = new Curso()
            {
                Id = Guid.NewGuid(),
                Nome = "Engenharia da Computação",
                Grades = new[] {grade},
            };

            grade.Curso = curso;
            _f7DbContext.Add(curso);
            
            
            var professorRogerio = new PessoaUsuario()
            {
                Id = Guid.NewGuid(),
                Nome = "Rogerio",
                Perfil = Perfil.Professor,
            };
            var professorAfonso = new PessoaUsuario()
            {
                Id = Guid.NewGuid(),
                Nome = "Afonso",
                Perfil = Perfil.Professor,
            };

            var turmaDeCalculo = new Turma()
            {
                Sala = "101A",
                Id = Guid.NewGuid(),
                Disciplina = calculo.Disciplina,
                Semestre = semestres.semestre,
                Professor = professorAfonso,
            };
            _f7DbContext.TurmaDbSet.Add(turmaDeCalculo);
            
            var turmaDeCalculo2 = new Turma()
            {
                Sala = "101A",
                Id = Guid.NewGuid(),
                Disciplina = calculo2.Disciplina,
                Semestre = semestres.semestre2,
                Professor = professorAfonso,
            };
            _f7DbContext.TurmaDbSet.Add(turmaDeCalculo);

            var turmaDeLogicaMatematica = new Turma()
            {
                Sala = "102A",
                Id = Guid.NewGuid(),
                Disciplina = logicaMatematica.Disciplina,
                Semestre = semestres.semestre2,
                Professor = professorRogerio,
            };
            var turmaDeAlgoritmos = new Turma()
            {
                Sala = "103A",
                Id = Guid.NewGuid(),
                Disciplina = algoritmos.Disciplina,
                Semestre = semestres.semestre2,
                Professor = professorRogerio
            };

            var turmaDeCalculoHorarios = new List<TurmaHorario>
            {
                new TurmaHorario {Horario = horarios[0], Turma = turmaDeCalculo},
                new TurmaHorario {Horario = horarios[1], Turma = turmaDeCalculo},
                
                new TurmaHorario {Horario = horarios[6], Turma = turmaDeCalculo2},
                new TurmaHorario {Horario = horarios[7], Turma = turmaDeCalculo2},
                
                new TurmaHorario {Horario = horarios[1], Turma = turmaDeLogicaMatematica},
                new TurmaHorario {Horario = horarios[2], Turma = turmaDeLogicaMatematica},
                
                new TurmaHorario {Horario = horarios[4], Turma = turmaDeAlgoritmos},
                new TurmaHorario {Horario = horarios[5], Turma = turmaDeAlgoritmos}
            };
            _f7DbContext.AddRange(turmaDeCalculoHorarios);

            var inscricao = new Inscricao()
            {
                Completa = true,
                P1 = 10,
                P2 = 9,
                Turma = turmaDeCalculo,
                DataInscricao = DateTime.Now.AddMonths(-6),
                Id = Guid.NewGuid(),
            };

            var matricula = new Matricula()
            {
                Grade = grade,
                Id = Guid.NewGuid(),
                PessoaUsuarioId = criarEstudante.Id,
                Ativo = true,
                Inscricoes = new List<Inscricao>() {inscricao},
            };

            inscricao.MatriculaId = matricula.Id;
            
            _f7DbContext.Add(matricula);
            _f7DbContext.Add(inscricao);
            
            var config = new Configuration()
            {
                NotaMedia = 6,
                SemestreAtual = semestres.semestre2
            };

            _f7DbContext.Configurations.Add(config);
            
            _f7DbContext.SaveChanges();
        }

        private SemestreDisciplina CriarSemestreDisciplina(string nome, int creditos, int semestre,
            params SemestreDisciplina[] prerequisito)
        {
            var disciplina = new Disciplina()
            {
                Id = Guid.NewGuid(),
                Creditos = creditos,
                Nome = nome
            };
            

            if (!prerequisito.IsNullOrEmpty())
            {
                disciplina.Prerequisites = prerequisito.Select(x => x.Disciplina).ToList();
            }

            return new SemestreDisciplina()
            {
                Id = Guid.NewGuid(),
                Disciplina = disciplina,
                Semestre = semestre
            };
        }
        
        private (Semestre semestre, Semestre semestre2) SalvaSemestres()
        {
            var semestre = new Semestre()
            {
                Id = 1,
                Ano = 2020,
                SegundoSemestre = false,
                Start = new DateTime(2020, 01, 01),
                End = new DateTime(2020, 05, 31)
            };
            
            var semestre2 = new Semestre()
            {
                Id = 2,
                Ano = 2020,
                SegundoSemestre = true,
                Start = new DateTime(2020, 01, 01),
                End = new DateTime(2020, 05, 31)
            };

            _f7DbContext.Add(semestre);
            _f7DbContext.Add(semestre2);

            return (semestre, semestre2);
        }


        private List<Horario> SalvaHorarios()
        {
            var horarios = new List<Horario>()
            {
                new Horario()
                {
                    Id = 1,
                    DayOfWeek = DayOfWeek.Monday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = 2,
                    DayOfWeek = DayOfWeek.Monday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = 3,
                    DayOfWeek = DayOfWeek.Tuesday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = 4,
                    DayOfWeek = DayOfWeek.Tuesday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = 5,
                    DayOfWeek = DayOfWeek.Wednesday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = 6,
                    DayOfWeek = DayOfWeek.Wednesday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = 7,
                    DayOfWeek = DayOfWeek.Thursday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = 8,
                    DayOfWeek = DayOfWeek.Thursday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = 9,
                    DayOfWeek = DayOfWeek.Friday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = 10,
                    DayOfWeek = DayOfWeek.Friday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                }
            };
            
            _f7DbContext.AddRange(horarios);

            return horarios;
        }
    }
}