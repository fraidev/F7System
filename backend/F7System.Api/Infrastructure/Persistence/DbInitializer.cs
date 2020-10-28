using System;
using System.Collections.Generic;
using F7System.Api.Domain.Enums;
using F7System.Api.Domain.Models;
using F7System.Api.Domain.Services;
using F7System.Api.Infrastructure.Persistence;

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

        public DbInitializer(IUserService userService, F7DbContext f7DbContext)
        {
            _userService = userService;
            _f7DbContext = f7DbContext;
        }

        public void Initialize()
        {
            _userService.CreateAdminUserWhenDontHaveManagerUsers();
            
            var estudante = new PessoaUsuario()
            {
                Perfil = Perfil.Estudante,
                Id = Guid.NewGuid(),
                Nome = "Felipe",
                Username = "felipe",
                CPF = "12345678901",
                DataNascimento = DateTime.Now.AddYears(-23)
            };

            _f7DbContext.Add(estudante);

            var semestres = SalvaSemestres();
            var horarios = SalvaHorarios();
            
            
            var calculo = new Disciplina()
            {
                Id = Guid.NewGuid(),
                Creditos = 80,
                Nome = "Calculo"
            };
            _f7DbContext.Add(calculo);
            
            var calculo2 = new Disciplina()
            {
                Id = Guid.NewGuid(),
                Creditos = 80,
                Nome = "Calculo 2",
                Prerequisites = {calculo}
            };
            _f7DbContext.Add(calculo);
            
            var algoritmos = new Disciplina()
            {
                Id = Guid.NewGuid(),
                Creditos = 80,
                Nome = "Algoritmos"
            };
            _f7DbContext.Add(calculo);
            
            var logicaMatematica = new Disciplina()
            {
                Id = Guid.NewGuid(),
                Creditos = 80,
                Nome = "Logica Matematica"
            };
            _f7DbContext.Add(calculo);
            
            var grade = new Grade()
            {
                Id = Guid.NewGuid(),
                Ano = 2020,
                Disciplinas = new List<Disciplina>(){calculo, calculo2, algoritmos, logicaMatematica}
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
                Disciplina = calculo,
                Semestre = semestres.semestre,
                Professor = professorAfonso,
            };
            _f7DbContext.TurmaDbSet.Add(turmaDeCalculo);
            
            var turmaDeCalculo2 = new Turma()
            {
                Sala = "101A",
                Id = Guid.NewGuid(),
                Disciplina = calculo2,
                Semestre = semestres.semestre2,
                Professor = professorAfonso,
            };
            _f7DbContext.TurmaDbSet.Add(turmaDeCalculo);

            var turmaDeLogicaMatematica = new Turma()
            {
                Sala = "102A",
                Id = Guid.NewGuid(),
                Disciplina = logicaMatematica,
                Semestre = semestres.semestre2,
                Professor = professorRogerio,
            };
            var turmaDeAlgoritmos = new Turma()
            {
                Sala = "103A",
                Id = Guid.NewGuid(),
                Disciplina = algoritmos,
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
                Nota = 10,
                Turma = turmaDeCalculo,
                DataInscricao = DateTime.Now.AddMonths(-6),
                Id = Guid.NewGuid(),
            };

            var matricula = new Matricula()
            {
                Grade = grade,
                Id = Guid.NewGuid(),
                PessoaUsuario = estudante,
                Inscricoes = new[] {inscricao},
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