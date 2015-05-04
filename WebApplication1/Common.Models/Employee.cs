namespace Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Employee : EntityBase
    {
        public Employee()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
            Projects = new HashSet<Project>();
        }

        public override int Id
        {
            get { return EmployeeId; }
            set { EmployeeId = value; }
        }

        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string ContractorCompanyName { get; set; }

        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
