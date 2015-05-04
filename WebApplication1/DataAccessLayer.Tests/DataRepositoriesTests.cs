using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
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
            //AggregateCatalog catalog = new AggregateCatalog();
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeRepository).Assembly));
            //CompositionContainer container = new CompositionContainer(catalog);
            //ObjectBase.Container = container;
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            var aa = GenerateEmployee();
            var bb = GenerateProject(new List<Employee> {aa});
        }

        private Employee GenerateEmployee()
        {
            Employee employee = new Employee();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            employee.EmployeeId = fixture.Create<int>();
            employee.FirstName = fixture.Create<string>();
            employee.LastName = fixture.Create<string>();
            employee.MiddleName = fixture.Create<string>();
            employee.Email = fixture.Create<string>();
            employee.ContractorCompanyName = fixture.Create<string>();

            return employee;
        }

        private Project GenerateProject(ICollection<Employee> employees)
        {
            Project project = new Project();
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            project.ProjectId = fixture.Create<int>();
            project.ProjectName = fixture.Create<string>();
            project.CustomerCompanyName = fixture.Create<string>();
            project.ManagerId = employees.ElementAt(new Random().Next(employees.Count)).EmployeeId;
            project.StartDate = fixture.Create<DateTime>();
            project.EndDate = project.StartDate + new TimeSpan(fixture.Create<int>(), 0, 0, 0);
            project.Priority = Math.Abs(fixture.Create<int>());
            project.Comment = fixture.Create<string>();

            return project;
        }

    }
}