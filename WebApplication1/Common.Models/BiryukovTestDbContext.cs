namespace Common.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BiryukovTestDbContext : DbContext
    {
        public BiryukovTestDbContext()
            : base("name=BiryukovTestDbConnectionString")
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
                .HasMany(e => e.ProjectsEmployees)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ManagerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.ProjectsEmployees)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);
        }
    }
}
