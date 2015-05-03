namespace Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Project : ObjectBase
    {
        public Project()
        {
            ProjectsEmployees = new HashSet<ProjectsEmployee>();
        }

        public override int Id
        {
            get { return ProjectId; }
            set { ProjectId = value; }
        }

        public int ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string CustomerCompanyName { get; set; }

        public int ManagerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Priority { get; set; }

        public string Comment { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<ProjectsEmployee> ProjectsEmployees { get; set; }
    }
}
