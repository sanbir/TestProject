using System.ComponentModel;

namespace Data.Models
{
    public class ProjectsEmployee : ObjectBase
    {
        [Browsable(false)]
        public int ProjectId { get; set; }

        [Browsable(false)]
        public int EmployeeId { get; set; }

        [Browsable(false)]
        public virtual Employee Employee { get; set; }

        [Browsable(false)]
        public virtual Project Project { get; set; }
    }
}
