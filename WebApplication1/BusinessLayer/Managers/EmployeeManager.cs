using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Contracts.Managers;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;
using Utils;

namespace BusinessLayer.Managers
{
    [Export(typeof(IEmployeeManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeManager : ManagerBase, IEmployeeManager
    {
        public EmployeeManager()
        {
        }

        public EmployeeManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public Employee UpdateEmployee(Employee employee)
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

            Employee updatedEntity = null;

            if (employee.Id == 0)
                updatedEntity = employeeRepository.Add(employee);
            else
                updatedEntity = employeeRepository.Update(employee);

            return updatedEntity;
        }

        public void DeleteEmployee(int employeeId)
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

            employeeRepository.Remove(employeeId);
        }

        public Employee GetEmployee(int employeeId)
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

            Employee employeeEntity = employeeRepository.Get(employeeId);
            if (employeeEntity == null)
            {
                //NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in database", employeeId));
            }

            return employeeEntity;
        }

        public IEnumerable<Employee> GetAllEmployeesSortedAndFiltered(ListSortDirection sortDirection, PropertyDescriptor sortPropertyDescriptor, string filter)
        {
            var employees = GetAllEmployees();

            if (!String.IsNullOrEmpty(filter))
            {
                employees = employees.Where(employee => employee.LastName.Contains(filter)
                                                        || employee.FirstName.Contains(filter)
                                                        || employee.MiddleName.Contains(filter)
                                                        || employee.Email.Contains(filter)
                                                        || employee.ContractorCompanyName.Contains(filter));
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

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
            IEnumerable<Employee> employees = employeeRepository.Get();
            return employees;
        }


        public IEnumerable<Employee> GetAllEmployeesSortedAndFiltered(string sortDirection, string sortPropertyName, string filter)
        {
            ListSortDirection direction = ListSortDirection.Ascending;

            if (!string.IsNullOrEmpty(sortDirection))
            {
                Enum.TryParse(sortDirection, out direction);
            }

            PropertyDescriptor descriptor = null;

            if (!string.IsNullOrEmpty(sortPropertyName))
            {
                try
                {
                    descriptor = TypeDescriptor.GetProperties(new Employee()).Find(sortPropertyName, false);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return GetAllEmployeesSortedAndFiltered(direction, descriptor, filter);
        }
    }
}
