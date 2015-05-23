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
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }

        public class PlainEmployee
        {
            public PlainEmployee(string firstName, string lastName, string middleName, string email,
                string contractorCompanyName)
            {
                FirstName = firstName;
                LastName = lastName;
                MiddleName = middleName;
                Email = email;
                ContractorCompanyName = contractorCompanyName;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string Email { get; set; }
            public string ContractorCompanyName { get; set; }
        }
    }
}