using BusinessLayer.Managers;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shared.Models;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class ProjectsEmployeeManagerTests
    {
        [TestMethod]
        public void UpdateProjectsEmployeeAddNew()
        {
            ProjectsEmployee newProjectsEmployee = new ProjectsEmployee();
            ProjectsEmployee addedProjectsEmployee = new ProjectsEmployee() { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectsEmployeeRepository>().Add(newProjectsEmployee)).Returns(addedProjectsEmployee);

            ProjectsEmployeeManager manager = new ProjectsEmployeeManager(mockDataRepositoryFactory.Object);

            ProjectsEmployee updateProjectsEmployeeResults = manager.UpdateProjectsEmployee(newProjectsEmployee);

            Assert.IsTrue(updateProjectsEmployeeResults == addedProjectsEmployee);
        }

        [TestMethod]
        public void UpdateProjectsEmployeeUpdateExisting()
        {
            ProjectsEmployee existingProjectsEmployee = new ProjectsEmployee() { Id = 1 };
            ProjectsEmployee updatedProjectsEmployee = new ProjectsEmployee() { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectsEmployeeRepository>().Update(existingProjectsEmployee)).Returns(updatedProjectsEmployee);

            ProjectsEmployeeManager manager = new ProjectsEmployeeManager(mockDataRepositoryFactory.Object);

            ProjectsEmployee updateProjectsEmployeeResults = manager.UpdateProjectsEmployee(existingProjectsEmployee);

            Assert.IsTrue(updateProjectsEmployeeResults == updatedProjectsEmployee);
        }
    }
}
