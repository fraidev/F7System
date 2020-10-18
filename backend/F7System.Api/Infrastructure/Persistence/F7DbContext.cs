using System.Linq;
using F7System.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace F7System.Api.Infrastructure.Persistence
{
    public class F7DbContext : DbContext
    {
        public DbSet<PessoaUsuario> PessoaUsuarioDbSet { get; set; }
        public DbSet<Curso> CursoDbSet { get; set; }
        public DbSet<Disciplina> DisciplinaDbSet { get; set; }
        public DbSet<Grade> GradeDbSet { get; set; }
        public DbSet<Horario> HorarioDbSet { get; set; }
        public DbSet<Inscricao> InscricaoDbSet { get; set; }
        public DbSet<Matricula> MatriculaDbSet { get; set; }
        public DbSet<Semestre> SemestreDbSet { get; set; }
        public DbSet<Turma> TurmaDbSet { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public Configuration Configuration => Configurations.Include(x => x.SemestreAtual).FirstOrDefault(x => x.Id == 1);


        public F7DbContext(DbContextOptions<F7DbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaUsuario>()
                .HasMany<Turma>(x => x.Turmas)
                .WithOne(x => x.Professor);

            modelBuilder.Entity<PessoaUsuario>()
                .HasMany<Matricula>(x => x.Matriculas)
                .WithOne(x => x.PessoaUsuario)
                .HasForeignKey(x => x.PessoaUsuarioId);

            modelBuilder.Entity<Curso>()
                .HasMany<Grade>(x => x.Grades)
                .WithOne(x => x.Curso)
                .HasForeignKey(x => x.CursoId);

            modelBuilder.Entity<Matricula>()
                .HasMany(x => x.Inscricoes)
                .WithOne(x => x.Matricula)
                .HasForeignKey(x => x.MatriculaId);

            modelBuilder.Entity<Inscricao>()
                .HasOne(x => x.Turma);

            modelBuilder.Entity<Turma>()
                .HasOne<Disciplina>(x => x.Disciplina)
                .WithMany(x => x.Turmas);

            modelBuilder.Entity<Turma>()
                .HasMany(x => x.Horarios);

            modelBuilder.Entity<TurmaHorario>()
                .HasKey(bc => new {bc.HorarioId, bc.TurmaId});

            modelBuilder.Entity<TurmaHorario>()
                .HasOne(bc => bc.Turma)
                .WithMany(b => b.TurmaHorarios)
                .HasForeignKey(bc => bc.TurmaId);

            // modelBuilder.Entity<Grade>()
            //     .HasOne<Grade>(x => x)
            //     .WithOne(x => x.Curso);


            // modelBuilder.Entity<Student>()
            //     .HasOne(x => x.User)
            //     .WithOne(x => x.Student)
            //     .HasForeignKey<User>(x => x.StudentId)
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            //
            // modelBuilder.Entity<Teacher>()
            //     .HasOne(x => x.User)
            //     .WithOne(x => x.Teacher)
            //     .HasForeignKey<User>(x => x.TeacherId)
            //     .OnDelete(DeleteBehavior.Restrict);
        }
    }
}