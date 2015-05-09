using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Contracts.Managers;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace BusinessLayer.Managers
{
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

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

            IEnumerable<Employee> employees = employeeRepository.Get();

            return employees;
        }

    }
}
