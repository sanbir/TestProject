using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.Managers;
using Shared.Models;
using Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApplication.Controllers;

namespace WebApplication.Tests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // arrange
            string sortDirection = "Descending";
            string receivedSortDirection = "Ascending";
            string sortPropertyName = "FirstName";
            string filter = "alex";

            List<Employee> employees = FixturesGenerator.GenerateEmployees(2).ToList();
            employees[0].FirstName = "Alexander";
            employees[1].FirstName = "Alexey";

            Mock<IManagerFactory> mockManagerFactory = new Mock<IManagerFactory>();
            mockManagerFactory.Setup(mock => mock.GetManager<IEmployeeManager>()
                .GetAll(receivedSortDirection, sortPropertyName, filter))
                .Returns(employees);

            EmployeeController controller = new EmployeeController(mockManagerFactory.Object.GetManager<IEmployeeManager>());

            // act
            ActionResult result = controller.Index(sortDirection, sortPropertyName, null, filter, null);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(controller.ViewBag.CurrentSortDirection);
            Assert.AreEqual(receivedSortDirection, controller.ViewBag.CurrentSortDirection);
            Assert.IsNotNull(controller.ViewBag.CurrentFilter);
            Assert.AreEqual(filter, controller.ViewBag.CurrentFilter);
        }

    }
}
