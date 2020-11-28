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
            
            var calculo2 = CriarSemestreDisciplina("Calculo II", 80, 2, calculo);
            var calcVetorialGeometriaAnaliticaEspacial = CriarSemestreDisciplina("Calc. Vetorial e Geometria Analítica Espacial", 80, 2, calcVelotorialGeometriaAnaliticaPlana);
            var fisica = CriarSemestreDisciplina("Física I", 80, 2);
            var labSO = CriarSemestreDisciplina("Lab SO", 40, 2);
            var labProgConstSoftware = CriarSemestreDisciplina("Lab Prog e Const Software", 80, 2, algoritmos);
            
            var redacaoEmpresarial = CriarSemestreDisciplina("Redação Empresarial", 40, 3);
            var psicologiaOrganizacional = CriarSemestreDisciplina("Psicologia Organizacional", 40, 3);
            var calculo3 = CriarSemestreDisciplina("Cálculo III", 80, 3, calculo2);
            var fisica2 = CriarSemestreDisciplina("Física II", 80, 3, fisica);
            var arqComputadores = CriarSemestreDisciplina("Arq. Computadores", 80, 3, labSO);
            var estruturaDados1  = CriarSemestreDisciplina("Estrutura Dados I ", 80, 3);
            
            var algebraLinear = CriarSemestreDisciplina("Álgebra Linear", 80, 4);
            var fisica3 = CriarSemestreDisciplina("Física III", 80, 4, fisica2);
            var fundamentosEngDeSoftware = CriarSemestreDisciplina("Fundamentos de Eng. De Software", 80, 4);
            var programacaoWeb = CriarSemestreDisciplina("Programação Web", 80, 4);
            var estruturaDados2 = CriarSemestreDisciplina("Estrutura de Dados II", 80, 4, estruturaDados1);
            
            var eqDifSeries = CriarSemestreDisciplina("Eq Diferen Séries", 80, 5);
            var fundProbabEstatística = CriarSemestreDisciplina("Fund Probab Estatística", 40, 5);
            var teoriaDaComputacao = CriarSemestreDisciplina("Teoria da Computação", 40, 5);
            var bancoDeDados = CriarSemestreDisciplina("Banco de Dados I", 80, 5);
            var fundamentosDeAutomacao = CriarSemestreDisciplina("Fundamentos de Automação", 80, 5);
            var fundSistEletroEletronicos = CriarSemestreDisciplina("Fund Sist ElétroEletrônicos", 80, 5);
            
            var bancoDeDados2 = CriarSemestreDisciplina("Banco de Dados II", 40, 6, bancoDeDados); 
            var sistemasOperacionais = CriarSemestreDisciplina("Sistemas Operacionais", 80, 6); 
            var labProgConstSoftOO = CriarSemestreDisciplina("Lab Prog Const Soft OO", 80, 6);
            var sistemasDeControle = CriarSemestreDisciplina("Sistemas de Controle", 80, 6);
            var fundSistDigitais = CriarSemestreDisciplina("Fund Sist Digitais", 80, 6);
            
            var eticaNaEngenharia = CriarSemestreDisciplina("Ética na Engenharia", 40, 7);  
            var fisica4 = CriarSemestreDisciplina("Física IV", 80, 7, fisica3);  
            var arquiteturaModelagemDeSoftware = CriarSemestreDisciplina("Arquitetura e Modelagem de Software", 80, 7, fisica3);  
            var labProjSistDig = CriarSemestreDisciplina("Lab Proj Sist Dig", 80, 7);  
            var engenhariaDeSistemasEmbarcados = CriarSemestreDisciplina("Engenharia de Sistemas Embarcados", 80, 7);  
            
            var fundamentosDeAdministracao = CriarSemestreDisciplina("Fundamentos de Administração", 40, 8);   
            var redesDeComputadores = CriarSemestreDisciplina("Redes de Computadores", 80, 8);   
            var dispositivosProgramáveisMicroMicro = CriarSemestreDisciplina("Dispositivos Programáveis e Micro & micro", 80, 8);   
            var projetoIntegração = CriarSemestreDisciplina("Projeto de Integração", 80, 8);
            var tcc1 = CriarSemestreDisciplina("TCC I", 40, 8);   
            
            var gereneciaDeProjeto = CriarSemestreDisciplina("Gerência de Projetos", 80, 9);   
            var oHomemFenomenoReligioso = CriarSemestreDisciplina("O Homem e o Fenômeno Religioso", 40, 9);   
            var sistemaOperacionaisDeTempoReal = CriarSemestreDisciplina("Sistemas Operacionais de Tempo Real", 40, 9);   
            var teleprocessamento = CriarSemestreDisciplina("Teleprocessamento", 40, 9);   
            var fundamentosDeRobotica = CriarSemestreDisciplina("Fundamentos de Robótica", 80, 9);   
            var laboratorioDeSistemasEmbarcados = CriarSemestreDisciplina("Laboratório de Sistemas Embarcados", 80, 9);   
            var tcc2 = CriarSemestreDisciplina("TCC II", 40, 9, tcc1);
            
            var fundamentosDeEconomia = CriarSemestreDisciplina("Fundamentos de Economia", 40, 10);
            var inteligenciaArtifical = CriarSemestreDisciplina("Inteligência Artificial", 40, 10);   
            var sistemaDistribuidos = CriarSemestreDisciplina("Sistemas Distribuídos", 40, 10);   
            var auditoriaSegurancaDeRedes = CriarSemestreDisciplina("Auditoria e Segurança de Redes", 40, 10);   
            var estagioSupervisionado = CriarSemestreDisciplina("Estágio Supervisionado", 80, 10); 
            var tcc3 = CriarSemestreDisciplina("TCC II", 40, 10, tcc2);   
 
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
                    calcVetorialGeometriaAnaliticaEspacial,
                    fisica,
                    labSO,
                    labProgConstSoftware,
                    redacaoEmpresarial,
                    psicologiaOrganizacional,
                    calculo3,
                    fisica2,
                    arqComputadores,
                    estruturaDados1,
                    algebraLinear, 
                    fisica3, 
                    fundamentosEngDeSoftware, 
                    programacaoWeb, 
                    estruturaDados2, 
                    eqDifSeries, 
                    fundProbabEstatística, 
                    teoriaDaComputacao, 
                    bancoDeDados, 
                    fundamentosDeAutomacao, 
                    fundSistEletroEletronicos, 
                    bancoDeDados2, 
                    sistemasOperacionais, 
                    labProgConstSoftOO, 
                    sistemasDeControle, 
                    fundSistDigitais, 
                    eticaNaEngenharia, 
                    fisica4, 
                    arquiteturaModelagemDeSoftware, 
                    labProjSistDig, 
                    engenhariaDeSistemasEmbarcados, 
                    fundamentosDeAdministracao, 
                    redesDeComputadores, 
                    dispositivosProgramáveisMicroMicro, 
                    projetoIntegração, 
                    tcc1, 
                    gereneciaDeProjeto, 
                    oHomemFenomenoReligioso, 
                    sistemaOperacionaisDeTempoReal, 
                    teleprocessamento, 
                    fundamentosDeRobotica, 
                    laboratorioDeSistemasEmbarcados, 
                    tcc2, 
                    fundamentosDeEconomia, 
                    inteligenciaArtifical, 
                    sistemaDistribuidos, 
                    auditoriaSegurancaDeRedes, 
                    estagioSupervisionado, 
                    tcc3
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