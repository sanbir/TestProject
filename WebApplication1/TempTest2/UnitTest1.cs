using System;
using BusinessLayer;
using BusinessLayer.Bootstrapper;
using BusinessLayer.Managers;
using Common.Models.Fixtures;
using Data.Contracts.DataRepositories;
using Data.Models;
using DataAccessLayer.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TempTest2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ObjectBase.Container = MefLoader.Init();

            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            #region Create

            Employee savedEmployee = employeeRepository.Add(employee);
            Assert.AreEqual(employee, savedEmployee);

            #endregion

            EmployeeManager manager = new EmployeeManager();
            Employee updateEmployeeResults = manager.UpdateEmployee(employee);
            Assert.AreEqual(employee.Email, updateEmployeeResults.Email);
        }
    }
}
