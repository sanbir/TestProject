using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;

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

        public List<Employee> GetEmployees(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            //ViewBag.CurrentFilter = searchString;

            IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
            IEnumerable<Employee> employees = employeeRepository.Get();

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(employee => employee.LastName.Contains(searchString)
                                                        || employee.FirstName.Contains(searchString)
                                                        || employee.MiddleName.Contains(searchString)
                                                        || employee.Email.Contains(searchString)
                                                        || employee.ContractorCompanyName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    employees = employees.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:  // Name ascending 
                    employees = employees.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return System.Web.UI.WebControls.View(employees.ToPagedList(pageNumber, pageSize));
        }

    }
}
