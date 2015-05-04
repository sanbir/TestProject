using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Common.Models.Fixtures;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;
using DataAccessLayer.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class DataRepositoriesTests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        public void TestEmployeeRepository()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            Employee savedEmployee = employeeRepository.Add(employee);
            Assert.AreEqual(employee, savedEmployee);

            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);
            Assert.AreEqual(employee.Email, readEmployee.Email);

            employee = FixturesGenerator.GenerateEmployee();
            readEmployee.FirstName = employee.FirstName;
            readEmployee.LastName = employee.LastName;
            readEmployee.MiddleName = employee.MiddleName;
            readEmployee.Email = employee.Email;
            readEmployee.ContractorCompanyName = employee.ContractorCompanyName;
            Employee updatedEmployee = employeeRepository.Update(readEmployee);
            Assert.AreEqual(employee.Email, readEmployee.Email);
        }

        [TestMethod]
        public void TestReadEmployeeFromDatabase()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            

           // Assert.AreEqual(employee, savedEmployee);
        }

        [TestMethod]
        public void TestCreateProjectInDatabase()
        {
            //Project project = FixturesGenerator.GenerateProject();
            //IEmployeeRepository employeeRepository = new EmployeeRepository();

            //Employee savedEmployee = employeeRepository.Add(employee);

            //Assert.AreEqual(employee, savedEmployee);
        }
        

    }
}