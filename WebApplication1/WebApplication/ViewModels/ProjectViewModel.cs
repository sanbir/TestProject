using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class ProjectViewModel
    {
        public string ProjectName { get; set; }
        public string CustomerCompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public string Comment { get; set; }
    }
}