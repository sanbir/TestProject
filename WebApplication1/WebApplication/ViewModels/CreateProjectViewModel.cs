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
    }
}