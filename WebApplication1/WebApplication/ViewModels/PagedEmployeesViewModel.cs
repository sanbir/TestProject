using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels
{
    public class PagedEmployeesViewModel
    {
        public IEnumerable<PlainEmployee> Employees { get; set; }

        public string CurrentSortDirection { get; set; }

        public string CurrentFilter { get; set; }

        public class PlainEmployee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Email { get; set; }
            public string ContractorCompanyName { get; set; }
        }
    }
}