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
        [TestMethod]
        public void TestEmployeeRepository()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            #region Create

            Employee savedEmployee = employeeRepository.Add(employee);
            Assert.AreEqual(employee, savedEmployee);

            #endregion

            #region Read

            employeeRepository.Add(employee);
            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);
            Assert.AreEqual(employee.Email, readEmployee.Email);

            #endregion

            #region Read Many

            var readEmployees = employeeRepository.Get();
            Assert.IsTrue(readEmployees.Count() > 1);

            #endregion

            #region Update

            employee = FixturesGenerator.GenerateEmployee();
            readEmployee.FirstName = employee.FirstName;
            readEmployee.LastName = employee.LastName;
            readEmployee.MiddleName = employee.MiddleName;
            readEmployee.Email = employee.Email;
            readEmployee.ContractorCompanyName = employee.ContractorCompanyName;
            Employee updatedEmployee = employeeRepository.Update(readEmployee);
            Assert.AreEqual(employee.Email, updatedEmployee.Email); 

            #endregion

            #region Delete

            employeeRepository.Remove(updatedEmployee);
            readEmployee = employeeRepository.Get(updatedEmployee.Id);
            Assert.IsNull(readEmployee);

            #endregion

            #region Delete By Id

            savedEmployee = employeeRepository.Add(employee);
            employeeRepository.Remove(savedEmployee.Id);
            readEmployee = employeeRepository.Get(savedEmployee.Id);
            Assert.IsNull(readEmployee);

            #endregion

        }

        //[TestMethod]
        //public void TestProjectRepository()
        //{
        //    Project project = FixturesGenerator.GenerateProject();
        //    IEmployeeRepository employeeRepository = new EmployeeRepository();

        //    #region Create

        //    Employee savedEmployee = employeeRepository.Add(project);
        //    Assert.AreEqual(project, savedEmployee);

        //    #endregion

        //    #region Read

        //    Employee readEmployee = employeeRepository.Get(savedEmployee.Id);
        //    Assert.AreEqual(project.Email, readEmployee.Email);

        //    #endregion

        //    #region Update

        //    project = FixturesGenerator.GenerateEmployee();
        //    readEmployee.FirstName = project.FirstName;
        //    readEmployee.LastName = project.LastName;
        //    readEmployee.MiddleName = project.MiddleName;
        //    readEmployee.Email = project.Email;
        //    readEmployee.ContractorCompanyName = project.ContractorCompanyName;
        //    Employee updatedEmployee = employeeRepository.Update(readEmployee);
        //    Assert.AreEqual(project.Email, updatedEmployee.Email);

        //    #endregion

        //    #region Delete

        //    employeeRepository.Remove(updatedEmployee);
        //    readEmployee = employeeRepository.Get(updatedEmployee.Id);
        //    Assert.IsNull(readEmployee);

        //    #endregion
        //}

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