using System.Configuration;
using System.Data.Entity;
using Data.Models;
using Utils;

namespace DataAccessLayer
{
    public class BiryukovTestDbContext : DbContext
    {
        public BiryukovTestDbContext()
            : base(Constants.ConnectionString)
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
                .Property(e => e.Id)
                .HasColumnName(Constants.EmployeeId);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Id)
                .HasColumnName(Constants.ProjectId);
        }
    }
}
