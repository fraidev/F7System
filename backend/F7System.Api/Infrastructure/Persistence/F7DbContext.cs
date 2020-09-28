using F7System.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace F7System.Api.Infrastructure.Persistence
{
    public class F7DbContext: DbContext
    {
        public DbSet<UserPerson> UserDbSet { get; set; }
        public DbSet<Student> StudentDbSet { get; set; }
        public DbSet<Teacher> TeacherDbSet { get; set; }


        public F7DbContext(DbContextOptions<F7DbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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