using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using Shared.Models;

namespace BusinessLayer
{
    public class EmployeeManagerBase : ManagerBase
    {
        protected EmployeeManagerBase()
        {
        }

        protected EmployeeManagerBase(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        protected IEnumerable<Employee> GetAllEmployees(ListSortDirection sortDirection, PropertyDescriptor sortPropertyDescriptor, string filter)
        {
            var employees = GetAllEmployees();

            if (!String.IsNullOrEmpty(filter))
            {
                employees =
                    employees.Where(employee =>
                        employee.LastName.ToLowerInvariant()
                            .Contains(filter.ToLowerInvariant())
                        ||
                        employee.FirstName.ToLowerInvariant()
                            .Contains(filter.ToLowerInvariant())
                        ||
                        (employee.MiddleName != null && employee.MiddleName.ToLowerInvariant()
                            .Contains(filter.ToLowerInvariant()))
                        || employee.Email.ToLowerInvariant()
                            .Contains(filter.ToLowerInvariant())
                        ||
                        employee.ContractorCompanyName.ToLowerInvariant()
                            .Contains(filter.ToLowerInvariant()));
            }

            switch (sortDirection)
            {
                case ListSortDirection.Ascending:
                    employees = sortPropertyDescriptor == null
                        ? employees.OrderBy(employee => employee.LastName)
                        : employees.OrderBy(sortPropertyDescriptor.GetValue);
                    break;
                case ListSortDirection.Descending:
                    employees = sortPropertyDescriptor == null
                        ? employees.OrderByDescending(employee => employee.LastName)
                        : employees.OrderByDescending(sortPropertyDescriptor.GetValue);
                    break;
            }

            return employees;
        }

        protected IEnumerable<Employee> GetAllEmployees()
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
            IEnumerable<Employee> employees = employeeRepository.Get();
            return employees;
        }

        protected IEnumerable<Employee> GetAllEmployees(string sortDirection, string sortPropertyName, string filter)
        {
            ListSortDirection direction = ListSortDirection.Ascending;

            if (!string.IsNullOrEmpty(sortDirection))
            {
                Enum.TryParse(sortDirection, out direction);
            }

            PropertyDescriptor descriptor = null;

            if (!string.IsNullOrEmpty(sortPropertyName))
            {
                descriptor = TypeDescriptor.GetProperties(new Employee()).Find(sortPropertyName, false);
            }

            return GetAllEmployees(direction, descriptor, filter);
        }
    }
}
