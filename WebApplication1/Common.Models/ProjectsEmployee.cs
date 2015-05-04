namespace Data.Models
{
    public class ProjectsEmployee : EntityBase
    {
        public override int Id { get; set; }

        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }
    }
}
