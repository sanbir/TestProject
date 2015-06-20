using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Contracts.DataRepositories;
using DAL.EntityFrameworkRepository.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Exceptions;
using Shared.Models;
using Tests.Fixtures;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        [TestMethod]
        public void CreateEmployee()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            Employee savedEmployee = employeeRepository.Add(employee);
            Assert.AreEqual(employee, savedEmployee);
        }

        [TestMethod]
        public void ReadEmployee()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee savedEmployee = employeeRepository.Add(employee);

            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);

            Assert.AreEqual(employee.Email, readEmployee.Email);
        }

        [TestMethod]
        public void ReadManyEmployees()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            employeeRepository.Add(employee);
            employeeRepository.Add(employee);

            var readEmployees = employeeRepository.Get();
            Assert.IsTrue(readEmployees.Count() > 1);
        }

        [TestMethod]
        public void UpdateEmployee()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee savedEmployee = employeeRepository.Add(employee);
            employeeRepository.Add(employee);
            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);
            employee = FixturesGenerator.GenerateEmployee();
            readEmployee.FirstName = employee.FirstName;
            readEmployee.LastName = employee.LastName;
            readEmployee.MiddleName = employee.MiddleName;
            readEmployee.Email = employee.Email;
            readEmployee.ContractorCompanyName = employee.ContractorCompanyName;

            Employee updatedEmployee = employeeRepository.Update(readEmployee);

            Assert.AreEqual(employee.Email, updatedEmployee.Email);
        }

        [TestMethod]
        public void DeleteEmployee()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee savedEmployee = employeeRepository.Add(employee);
            employeeRepository.Add(employee);
            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);

            employeeRepository.Remove(readEmployee);

            readEmployee = employeeRepository.Get(readEmployee.Id);
            Assert.IsNull(readEmployee);
        }

        [TestMethod]
        public void DeleteEmployeeById()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee savedEmployee = employeeRepository.Add(employee);
            employeeRepository.Add(employee);
            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);

            employeeRepository.Remove(readEmployee.Id);

            readEmployee = employeeRepository.Get(readEmployee.Id);
            Assert.IsNull(readEmployee);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public void EmployeeRepositoryThrowsDataAccessException()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee savedEmployee = employeeRepository.Add(employee);
            employeeRepository.Add(employee);
            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);

            employeeRepository.Remove(readEmployee.Id);

            readEmployee = employeeRepository.Get(readEmployee.Id);
            employeeRepository.Update(readEmployee);
        }

    }
}
