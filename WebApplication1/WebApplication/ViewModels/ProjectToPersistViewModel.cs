using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ProjectToPersistViewModel : ProjectPartialViewModel
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public IEnumerable<int> AssignedEmployeesIds { get; set; }
    }
}