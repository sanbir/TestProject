using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Data.Models;
using PagedList;

namespace ContosoUniversity.ViewModels
{
    public class EmployeeViewModel
    {
        public IPagedList<Employee> Employees { get; set; }

        public Sort Sorting { get; set; }

        public string Filter { get; set; }

        public string ErrorMessage { get; set; }

        public class Sort
        {
            public SortDirection SortOrder { get; set; }

            public Employee.PropertyNames SortPropertyName { get; set; }

            public override string ToString()
            {
                return SortOrder.ToString() + SortPropertyName;
            }
        }
    }
}