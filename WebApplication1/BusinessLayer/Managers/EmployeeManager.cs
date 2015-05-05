using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;
using Data.Contracts.DataRepositories;

namespace BusinessLayer.Managers
{
    public class EmployeeManager : ManagerBase //, IInventoryService
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

        public Employee[] GetAllEmployees()
        {
            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

            IEnumerable<Employee> employees = employeeRepository.Get();

            foreach (Employee employee in employees)
            {
                //Rental rentedCar = rentedCars.Where(item => item.CarId == employee.CarId).FirstOrDefault();
                //employee.CurrentlyRented = (rentedCar != null);
            }

            return employees.ToArray();
        }

    }
}
