using System.Data.Entity;
using Common.Constants.Common;
using Data.Models;

namespace DataAccessLayer
{
    public class BiryukovTestDbContext : DbContext
    {
        public BiryukovTestDbContext()
            : base(DbAccess.MainConnectionString)
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
                .HasColumnName(DbAccess.EmployeeId);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(p => p.Id)
                .HasColumnName(DbAccess.ProjectId);
            modelBuilder.Entity<Project>()
                .Ignore(p => p.Manager);
        }
    }
}
