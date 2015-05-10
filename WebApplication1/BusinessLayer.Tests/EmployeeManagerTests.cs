using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using BusinessLayer.Managers;
using Common.Models.Fixtures;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;
using DataAccessLayer.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class EmployeeManagerTests
    {
        [TestMethod]
        public void UpdateEmployeeAddNew()
        {
            Employee newEmployee = new Employee();
            Employee addedEmployee = new Employee() { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Add(newEmployee)).Returns(addedEmployee);

            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            Employee updateEmployeeResults = manager.CreateOrUpdate(newEmployee);

            Assert.IsTrue(updateEmployeeResults == addedEmployee);
        }

        [TestMethod]
        public void UpdateEmployeeUpdateExisting()
        {
            Employee existingEmployee = new Employee() { Id = 1 };
            Employee updatedEmployee = new Employee() { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Update(existingEmployee)).Returns(updatedEmployee);

            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            Employee updateEmployeeResults = manager.CreateOrUpdate(existingEmployee);

            Assert.IsTrue(updateEmployeeResults == updatedEmployee);
        }

        [TestMethod]
        public void GetAllEmployees()
        {
            IEnumerable<Employee> employees = FixturesGenerator.GenerateEmployees(10);

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Get()).Returns(employees);

            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            Assert.IsTrue(manager.GetAll().ToList()[5].Email == employees.ToList()[5].Email);
        }

        [TestMethod]
        public void GetAllEmployeesSortedAndFiltered()
        {
            // arrange
            ListSortDirection sortDirection = ListSortDirection.Descending;
            PropertyDescriptor sortPropertyDescriptor = TypeDescriptor.GetProperties(new Employee()).Find("FirstName", false);
            string filter = "alex";

            List<Employee> employees = FixturesGenerator.GenerateEmployees(10).ToList();
            employees[0].FirstName = "Anton";
            employees[1].FirstName = "Anna";
            employees[2].FirstName = "Olga";
            employees[3].FirstName = "Alexander";
            employees[4].FirstName = "Igor";
            employees[5].FirstName = "Julia";
            employees[6].FirstName = "Galina";
            employees[7].FirstName = "Alexey";
            employees[8].FirstName = "Daria";
            employees[9].FirstName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Get()).Returns(employees);
            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            // act
            List<Employee> receivedEmployees = manager.GetAll(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedEmployees.Count == 2);
            Assert.IsTrue(receivedEmployees[0].FirstName == "Alexey");
        }

        [TestMethod]
        public void GetAllEmployeesSortedAndFiltered_WithStrings()
        {
            // arrange
            string sortDirection = "Descending";
            string sortPropertyDescriptor = "FirstName";
            string filter = "alex";

            List<Employee> employees = FixturesGenerator.GenerateEmployees(10).ToList();
            employees[0].FirstName = "Anton";
            employees[1].FirstName = "Anna";
            employees[2].FirstName = "Olga";
            employees[3].FirstName = "Alexander";
            employees[4].FirstName = "Igor";
            employees[5].FirstName = "Julia";
            employees[6].FirstName = "Galina";
            employees[7].FirstName = "Alexey";
            employees[8].FirstName = "Daria";
            employees[9].FirstName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Get()).Returns(employees);
            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            // act
            List<Employee> receivedEmployees = manager.GetAll(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedEmployees.Count == 2);
            Assert.IsTrue(receivedEmployees[0].FirstName == "Alexey");
        }

        [TestMethod]
        public void GetAllEmployeesSortedAndFiltered_WithIncorrectStrings()
        {
            // arrange
            string sortDirection = "ThisIsAnIncorrectValue";
            string sortPropertyDescriptor = "ThisIsAnotherIncorrectValue";
            string filter = "alex";

            List<Employee> employees = FixturesGenerator.GenerateEmployees(10).ToList();
            employees[0].LastName = "Anton";
            employees[1].LastName = "Anna";
            employees[2].LastName = "Olga";
            employees[3].LastName = "Alexander";
            employees[4].LastName = "Igor";
            employees[5].LastName = "Julia";
            employees[6].LastName = "Galina";
            employees[7].LastName = "Alexey";
            employees[8].LastName = "Daria";
            employees[9].LastName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Get()).Returns(employees);
            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            // act
            List<Employee> receivedEmployees = manager.GetAll(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedEmployees.Count == 2);
            Assert.IsTrue(receivedEmployees[0].LastName == "Alexander");
        }

        [TestMethod]
        public void TestDependencyInjection()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeRepository).Assembly));
            CompositionContainer container = new CompositionContainer(catalog);
            ObjectBase.Container = container;

            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();
            Employee savedEmployee = employeeRepository.Add(employee);

            EmployeeManager manager = new EmployeeManager();
            Employee updateEmployeeResults = manager.CreateOrUpdate(employee);
            Assert.AreEqual(employee.Email, updateEmployeeResults.Email);
        }

    }
}
