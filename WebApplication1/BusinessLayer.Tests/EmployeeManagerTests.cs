using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using BusinessLayer.Managers;
using Common.Models.Fixtures;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;
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

            Employee updateEmployeeResults = manager.UpdateEmployee(newEmployee);

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

            Employee updateEmployeeResults = manager.UpdateEmployee(existingEmployee);

            Assert.IsTrue(updateEmployeeResults == updatedEmployee);
        }

        [TestMethod]
        public void GetAllEmployees()
        {
            IEnumerable<Employee> employees = FixturesGenerator.GenerateEmployees(10);

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Get()).Returns(employees);

            EmployeeManager manager = new EmployeeManager(mockDataRepositoryFactory.Object);

            Assert.IsTrue(manager.GetAllEmployees().ToList()[5].Email == employees.ToList()[5].Email);
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
            List<Employee> receivedEmployees = manager.GetAllEmployeesSortedAndFiltered(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedEmployees.Count == 2);
            Assert.IsTrue(receivedEmployees[0].FirstName == "Alexey");
        }

    }
}
