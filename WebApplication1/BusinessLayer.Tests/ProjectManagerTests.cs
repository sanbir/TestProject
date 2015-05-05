using System;
using BusinessLayer.Managers;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class ProjectManagerTests
    {
        [TestMethod]
        public void UpdateProjectAddNew()
        {
            Project newProject = new Project();
            Project addedProject = new Project() { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Add(newProject)).Returns(addedProject);

            ProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);

            Project updateProjectResults = manager.UpdateProject(newProject);

            Assert.IsTrue(updateProjectResults == addedProject);
        }

        [TestMethod]
        public void UpdateProjectUpdateExisting()
        {
            Project existingProject = new Project() { Id = 1 };
            Project updatedProject = new Project() { Id = 1 };

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Update(existingProject)).Returns(updatedProject);

            ProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);

            Project updateProjectResults = manager.UpdateProject(existingProject);

            Assert.IsTrue(updateProjectResults == updatedProject);
        }
    }
}
