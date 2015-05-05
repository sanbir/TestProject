using System;
using BusinessLayer.Managers;
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

    }
}
