using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Models;

namespace WebApplication.ViewModels
{
    public class ProjectViewModel
    {
        private readonly Project _project;

        public ProjectViewModel(Project project)
        {
            _project = project;
        }

        public Project Project
        {
            get { return _project; }
        }

        public string ProjectManagerFullName
        {
            get
            {
                return string.Join(" ",
                    Project.Manager.LastName,
                    Project.Manager.FirstName,
                    Project.Manager.MiddleName ?? string.Empty);
            }
        }
    }
}