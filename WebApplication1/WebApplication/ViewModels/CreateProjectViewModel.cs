using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using Shared.Models;

namespace WebApplication.ViewModels
{
    public class CreateProjectViewModel
    {
        public Project Project { get; set; }

        public IPagedList<AssignedEmployeeData> Employees { get; set; }

        public string ManagerFullName {
            get
            {
                return
                    Employees.Where(employee => employee.IsManager ?? false)
                        .Select(employee => employee.FullName)
                        .FirstOrDefault();
            }
        }

        public List<AssignedEmployeeData> AssignedEmployees
        {
            get { return Employees.Where(employee => employee.IsAssigned).ToList(); }
        }
    }
}