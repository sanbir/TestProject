using System.Configuration;
using System.Data.Entity;
using Data.Models;

namespace DataAccessLayer
{
    public class BiryukovTestDbContext : DbContext
    {
        public BiryukovTestDbContext()
            : base("name=MainConnectionString")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectsEmployee> ProjectsEmployees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Ignore(e => e.Id)
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Ignore(e => e.Id);
        }
    }
}
