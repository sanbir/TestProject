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
        }

        [TestMethod]
        public void obtain_proxy_from_container_using_service_contract()
        {
            //var aa = GenerateEmployee();
            //var bb = GenerateProject(new List<Employee> {aa});
            //var cc = GenerateProjectsEmployee(new List<Project> {bb}, new List<Employee> {aa});
        }


        

    }
}