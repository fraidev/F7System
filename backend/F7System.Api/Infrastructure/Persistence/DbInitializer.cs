using System;
using System.Collections.Generic;
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

            SalvaSemestres();
            SalvaHorarios();
            
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
                Grade = new[] {grade},
            };

            grade.Curso = curso;
            _f7DbContext.Add(curso);
            
            
            _f7DbContext.SaveChanges();
        }

        private void SalvaSemestres()
        {
            var semestre = new Semestre()
            {
                Id = Guid.NewGuid(),
                Ano = 2020,
                SegundoSemestre = false,
                Start = new DateTime(2020, 01, 01),
                End = new DateTime(2020, 05, 31)
            };
            
            var semestre2 = new Semestre()
            {
                Id = Guid.NewGuid(),
                Ano = 2020,
                SegundoSemestre = true,
                Start = new DateTime(2020, 01, 01),
                End = new DateTime(2020, 05, 31)
            };

            _f7DbContext.Add(semestre);
            _f7DbContext.Add(semestre2);
        }


        private void SalvaHorarios()
        {
            var horarios = new List<Horario>()
            {
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Monday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Monday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Tuesday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Tuesday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Wednesday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Wednesday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Thursday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Thursday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Friday,
                    Start = new TimeSpan(18, 30, 00),
                    End = new TimeSpan(20, 30, 00),
                },
                new Horario()
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = DayOfWeek.Friday,
                    Start = new TimeSpan(20, 31, 00),
                    End = new TimeSpan(22, 30, 00),
                }
            };
            
            _f7DbContext.AddRange(horarios);
        }
    }
}