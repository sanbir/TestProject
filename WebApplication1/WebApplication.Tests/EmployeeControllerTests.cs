using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.Managers;
using Common.Models.Fixtures;
using ContosoUniversity.Controllers;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace WebApplication.Tests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        [TestMethod]
        public void GetAllEmployees()
        {
            // arrange
            string sortDirection = "Descending";
            string sortPropertyName = "FirstName";
            string filter = "alex";

            List<Employee> employees = FixturesGenerator.GenerateEmployees(2).ToList();
            employees[0].FirstName = "Alexander";
            employees[1].FirstName = "Alexey";

            Mock<IManagerFactory> mockManagerFactory = new Mock<IManagerFactory>();
            mockManagerFactory.Setup(mock => mock.GetManager<IEmployeeManager>()
                .GetAllEmployeesSortedAndFiltered("Ascending", sortPropertyName, filter))
                .Returns(employees);

            EmployeeController controller = new EmployeeController(mockManagerFactory.Object.GetManager<IEmployeeManager>());

            var dsffsd = controller.Index(sortDirection, sortPropertyName, null, filter, null);

            Assert.IsNotNull(dsffsd);
        }
    }
}
