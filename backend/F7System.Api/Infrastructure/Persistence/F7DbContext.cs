using F7System.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace F7System.Api.Infrastructure.Persistence
{
    public class F7DbContext: DbContext
    {
        public DbSet<User> UserDbSet { get; set; }
        public DbSet<Student> StudentDbSet { get; set; }
        // public DbSet<Manager> ManagerDbSet { get; set; }



        public F7DbContext(DbContextOptions<F7DbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            //     .HasOne(x => x.Manager)
            //     .WithOne(x => x.User)
            //     .HasForeignKey<Manager>(x => x.UserId)
            //     .OnDelete(DeleteBehavior.Restrict);
            // .IsRequired();
        }
    }
}