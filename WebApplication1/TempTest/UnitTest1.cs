using System;
using BusinessLayer;
using BusinessLayer.Managers;
using Common.Models.Fixtures;
using Data.Contracts.DataRepositories;
using Data.Models;
using DataAccessLayer.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TempTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UpdateEmployeeAddNewReal()
        {
            //Employee employee = FixturesGenerator.GenerateEmployee();

            //ObjectBase.Container = MEFLoader.Init();
            //EmployeeManager manager = new EmployeeManager();

            //Employee updateEmployeeResults = manager.UpdateEmployee(employee);

            //Assert.IsTrue(updateEmployeeResults == employee);

            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            #region Create

            Employee savedEmployee = employeeRepository.Add(employee);
            Assert.AreEqual(employee, savedEmployee);

            #endregion
        }
    }
}
