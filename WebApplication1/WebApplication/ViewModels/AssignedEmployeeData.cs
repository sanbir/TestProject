using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class AssignedEmployeeData
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public bool Assigned { get; set; }
    }
}