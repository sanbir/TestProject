using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.Managers;
using Shared.Models;
using Tests.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using Shared.Constants.Common;
using WebApplication.Controllers;
using WebApplication.ViewModels;

namespace WebApplication.Tests
{
    [TestClass]
    public class ProjectControllerTests
    {
        [TestMethod]
        public void GetEmployees()
        {
            // arrange
            List<Employee> employees = FixturesGenerator.GenerateEmployees(2).ToList();
            employees[0].FirstName = "Alexander";
            employees[1].FirstName = "Alexey";

            const int pageSize = ViewStringConstants.PageSize;
            const int pageNumber = ViewStringConstants.StartPage;
            int pageCount;
            Mock<IManagerFactory> mockManagerFactory = new Mock<IManagerFactory>();
            mockManagerFactory.Setup(mock => mock.GetManager<IProjectManager>()
                .GetAllEmployees(null, null, null, pageNumber, pageSize, out pageCount))
                .Returns(employees);

            ProjectController controller = new ProjectController(mockManagerFactory.Object.GetManager<IProjectManager>());

            // act
            string result = controller.GetEmployees(null, null, null, null);

            // assert
            Assert.IsNotNull(result);

            var jToken = JToken.Parse(result);
            var employeesFromJson = jToken.ToObject<PagedEmployeesViewModel>();

            Assert.IsNotNull(employeesFromJson);
        }

    }
}
