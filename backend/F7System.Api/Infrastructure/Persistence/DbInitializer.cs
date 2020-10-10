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

            var semestres = SalvaSemestres();
            var horarios = SalvaHorarios();
            
            
            var disciplina = new Disciplina()
            {
                Id = Guid.NewGuid(),
                Creditos = 80,
                Nome = "Calculo"
            };
            _f7DbContext.Add(disciplina);
            
            var grade = new Grade()
            {
                Id = Guid.NewGuid(),
                Ano = 2020,
                Disciplinas = new List<Disciplina>(){disciplina}
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
            
            
            var professor = new PessoaUsuario()
            {
                Id = Guid.NewGuid(),
                Nome = "Rogerio",
                Perfil = Perfil.Professor,
            };
            
            var turma = new Turma()
            {
                Id = Guid.NewGuid(),
                Disciplina = disciplina,
                Semestre = semestres.semestre,
                Professor = professor,
                Horarios = new[] {horarios[0]}, 
            };
            _f7DbContext.Add(turma);
            
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