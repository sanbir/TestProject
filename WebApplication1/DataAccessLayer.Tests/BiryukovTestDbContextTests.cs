using System.Linq;
using DAL.EntityFrameworkRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class BiryukovTestDbContextTests
    {
        private BiryukovTestDbContext _biryukovTestDbContext;

        [TestInitialize]
        public void Setup()
        {
            _biryukovTestDbContext = new BiryukovTestDbContext();
        }

        [TestMethod]
        public void TestEmployees()
        {
            var queryEmployees = from employees in _biryukovTestDbContext.Employees
                                 select employees;
            Assert.IsNotNull(queryEmployees);
        }

        [TestMethod]
        public void TestProjects()
        {
            var queryProjects = from projects in _biryukovTestDbContext.Projects
                                select projects;
            Assert.IsNotNull(queryProjects);
        }

        [TestMethod]
        public void TestProjectsEmployees()
        {
            var queryProjectsEmployees = from projectsEmployees in _biryukovTestDbContext.ProjectsEmployees
                                         select projectsEmployees;
            Assert.IsNotNull(queryProjectsEmployees);
        }

        [TestCleanup]
        public void Teardown()
        {
            _biryukovTestDbContext.Dispose();
        }
    }
}
