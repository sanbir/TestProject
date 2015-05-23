using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using BusinessLayer.Contracts.Managers;
using BusinessLayer.Managers;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using DAL.EntityFrameworkRepository.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shared.Models;
using Tests.Fixtures;

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

            Project updateProjectResults = manager.CreateOrUpdate(newProject);

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

            Project updateProjectResults = manager.CreateOrUpdate(existingProject);

            Assert.IsTrue(updateProjectResults == updatedProject);
        }

        [TestMethod]
        public void GetAllProjects()
        {
            IEnumerable<Employee> employees = FixturesGenerator.GenerateEmployees(10);
            IEnumerable<Project> projects = FixturesGenerator.GenerateProjects(10);

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Get()).Returns(projects);

            ProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);

            Assert.IsTrue(manager.GetAll().ToList()[5].CustomerCompanyName == projects.ToList()[5].CustomerCompanyName);
        }

        [TestMethod]
        public void GetAllProjectsSortedAndFiltered()
        {
            // arrange
            ListSortDirection sortDirection = ListSortDirection.Descending;
            PropertyDescriptor sortPropertyDescriptor = TypeDescriptor.GetProperties(new Project()).Find("CustomerCompanyName", false);
            string filter = "alex";

            List<Project> projects = FixturesGenerator.GenerateProjects(10).ToList();
            projects[0].CustomerCompanyName = "Anton";
            projects[1].CustomerCompanyName = "Anna";
            projects[2].CustomerCompanyName = "Olga";
            projects[3].CustomerCompanyName = "Alexander";
            projects[4].CustomerCompanyName = "Igor";
            projects[5].CustomerCompanyName = "Julia";
            projects[6].CustomerCompanyName = "Galina";
            projects[7].CustomerCompanyName = "Alexey";
            projects[8].CustomerCompanyName = "Daria";
            projects[9].CustomerCompanyName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Get()).Returns(projects);
            ProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);

            // act
            List<Project> receivedProjects = manager.GetAll(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedProjects.Count == 2);
            Assert.IsTrue(receivedProjects[0].CustomerCompanyName == "Alexey");
        }

        [TestMethod]
        public void GetAllProjectsSortedAndFiltered_WithStrings()
        {
            // arrange
            string sortDirection = "Descending";
            string sortPropertyDescriptor = "CustomerCompanyName";
            string filter = "alex";

            List<Project> projects = FixturesGenerator.GenerateProjects(10).ToList();
            projects[0].CustomerCompanyName = "Anton";
            projects[1].CustomerCompanyName = "Anna";
            projects[2].CustomerCompanyName = "Olga";
            projects[3].CustomerCompanyName = "Alexander";
            projects[4].CustomerCompanyName = "Igor";
            projects[5].CustomerCompanyName = "Julia";
            projects[6].CustomerCompanyName = "Galina";
            projects[7].CustomerCompanyName = "Alexey";
            projects[8].CustomerCompanyName = "Daria";
            projects[9].CustomerCompanyName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Get()).Returns(projects);
            ProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);

            // act
            List<Project> receivedProjects = manager.GetAll(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedProjects.Count == 2);
            Assert.IsTrue(receivedProjects[0].CustomerCompanyName == "Alexey");
        }

        [TestMethod]
        public void GetAllProjectsSortedAndFiltered_WithIncorrectStrings()
        {
            // arrange
            string sortDirection = "ThisIsAnIncorrectValue";
            string sortPropertyDescriptor = "ThisIsAnotherIncorrectValue";
            string filter = "alex";

            List<Project> projects = FixturesGenerator.GenerateProjects(10).ToList();
            projects[0].ProjectName = "Anton";
            projects[1].ProjectName = "Anna";
            projects[2].ProjectName = "Olga";
            projects[3].ProjectName = "Alexander";
            projects[4].ProjectName = "Igor";
            projects[5].ProjectName = "Julia";
            projects[6].ProjectName = "Galina";
            projects[7].ProjectName = "Alexey";
            projects[8].ProjectName = "Daria";
            projects[9].ProjectName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IProjectRepository>().Get()).Returns(projects);
            ProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);

            // act
            List<Project> receivedProjects = manager.GetAll(sortDirection, sortPropertyDescriptor, filter).ToList();

            // assert
            Assert.IsTrue(receivedProjects.Count == 2);
            Assert.IsTrue(receivedProjects[0].ProjectName == "Alexander");
        }

        [TestMethod]
        public void TestDependencyInjection()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ProjectRepository).Assembly));
            CompositionContainer container = new CompositionContainer(catalog);
            EntityBase.Container = container;

            List<Employee> employees = new EmployeeManager().GetAll().Take(5).ToList();

            Project project = FixturesGenerator.GenerateProject(employees);
            IProjectRepository projectRepository = new ProjectRepository();
            Project savedProject = projectRepository.Add(project);

            ProjectManager manager = new ProjectManager();
            Project updateProjectResults = manager.CreateOrUpdate(project);
            Assert.AreEqual(project.CustomerCompanyName, updateProjectResults.CustomerCompanyName);
        }

        [TestMethod]
        public void GetAllEmployeesPaged()
        {
            // arrange
            string sortDirection = "Descending";
            string sortPropertyName = "FirstName";
            string filter = "alex";

            List<Employee> employees = FixturesGenerator.GenerateEmployees(16).ToList();
            employees[0].FirstName = "Anton";
            employees[1].FirstName = "1Alexandra";
            employees[2].FirstName = "Anna";
            employees[3].FirstName = "Olga";
            employees[4].FirstName = "2Alexander";
            employees[5].FirstName = "Igor";
            employees[6].FirstName = "3Alexandro";
            employees[7].FirstName = "4Alexus";
            employees[8].FirstName = "5Alexandria";
            employees[9].FirstName = "Julia";
            employees[10].FirstName = "Galina";
            employees[11].FirstName = "6Alexey";
            employees[12].FirstName = "Daria";
            employees[13].FirstName = "7Alexea";
            employees[14].FirstName = "8Alexis";
            employees[15].FirstName = "Denis";

            Mock<IDataRepositoryFactory> mockDataRepositoryFactory = new Mock<IDataRepositoryFactory>();
            mockDataRepositoryFactory.Setup(mock => mock.GetDataRepository<IEmployeeRepository>().Get()).Returns(employees);
            IProjectManager manager = new ProjectManager(mockDataRepositoryFactory.Object);
            int pageCount;

            int pageSize = 3;
            int pageNumber = 2;

            // act
            List<Employee> receivedEmployees = manager.GetAllEmployees(sortDirection, sortPropertyName, filter,
                pageNumber, pageSize, out pageCount);

            // assert
            Assert.AreEqual(3, pageCount);
            Assert.IsTrue(receivedEmployees.Count == 3);
            Assert.IsTrue(receivedEmployees[0].FirstName == "5Alexandria");

            // arrange
            pageSize = 4;
            pageNumber = 2;

            // act
            receivedEmployees = manager.GetAllEmployees(sortDirection, sortPropertyName, filter,
                pageNumber, pageSize, out pageCount);

            // assert
            Assert.AreEqual(2, pageCount);
            Assert.IsTrue(receivedEmployees.Count == 4);
            Assert.IsTrue(receivedEmployees[0].FirstName == "4Alexus");

            // arrange
            pageSize = 3;
            pageNumber = 3;
            sortDirection = null;

            // act
            receivedEmployees = manager.GetAllEmployees(sortDirection, sortPropertyName, filter,
                pageNumber, pageSize, out pageCount);

            // assert
            Assert.AreEqual(3, pageCount);
            Assert.IsTrue(receivedEmployees.Count == 2);
            Assert.IsTrue(receivedEmployees[0].FirstName == "7Alexea");
        }

    }
}
