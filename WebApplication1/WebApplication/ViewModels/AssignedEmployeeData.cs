using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Models;

namespace WebApplication.ViewModels
{
    public class AssignedEmployeeData
    {
        public AssignedEmployeeData(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; set; }

        public string FullName 
        {
            get { return string.Join(" ", Employee.LastName, Employee.FirstName, Employee.MiddleName); } 
        }

        public bool Assigned { get; set; }

        public bool IsManager { get; set; }
    }
}