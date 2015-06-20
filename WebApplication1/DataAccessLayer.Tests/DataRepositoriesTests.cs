using System.Linq;
using DAL.Contracts.DataRepositories;
using DAL.EntityFrameworkRepository.DataRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Models;
using Tests.Fixtures;

namespace DataAccessLayer.Tests
{
    [TestClass]
    public class DataRepositoriesTests // TODO: refactor to separate classes
    {
        [TestMethod]
        public void TestProjectRepository()
        {
            var employees = new EmployeeRepository().Get().ToList();

            Project project = FixturesGenerator.GenerateProject(employees);
            IProjectRepository projectRepository = new ProjectRepository();

            #region Create

            Project savedProject = projectRepository.Add(project);
            Assert.AreEqual(project, savedProject);

            #endregion

            #region Read

            projectRepository.Add(project);
            Project readProject = projectRepository.Get(savedProject.Id);
            Assert.AreEqual(project.ProjectName, readProject.ProjectName);

            #endregion

            #region Read Many

            var readProjects = projectRepository.Get();
            Assert.IsTrue(readProjects.Count() > 1);

            #endregion

            #region Update

            project = FixturesGenerator.GenerateProject(employees);
            readProject.ProjectName = project.ProjectName;
            Project updatedProject = projectRepository.Update(readProject);
            Assert.AreEqual(project.ProjectName, updatedProject.ProjectName);

            #endregion

            #region Delete

            projectRepository.Remove(updatedProject);
            readProject = projectRepository.Get(updatedProject.Id);
            Assert.IsNull(readProject);

            #endregion

            #region Delete By Id

            savedProject = projectRepository.Add(project);
            projectRepository.Remove(savedProject.Id);
            readProject = projectRepository.Get(savedProject.Id);
            Assert.IsNull(readProject);

            #endregion

        }

        [TestMethod]
        public void TestProjectsEmployeeRepository()
        {
            var employees = new EmployeeRepository().Get().ToList();
            var projects = new ProjectRepository().Get().ToList();

            ProjectsEmployee projectsEmployee = FixturesGenerator.GenerateProjectsEmployee(projects, employees);
            IProjectsEmployeeRepository projectsEmployeeRepository = new ProjectsEmployeeRepository();

            #region Create

            ProjectsEmployee savedEmployee = projectsEmployeeRepository.Add(projectsEmployee);
            Assert.AreEqual(projectsEmployee, savedEmployee);

            #endregion

            #region Read

            projectsEmployeeRepository.Add(projectsEmployee);
            ProjectsEmployee readProjectsEmployee = projectsEmployeeRepository.Get(savedEmployee.Id);
            Assert.AreEqual(projectsEmployee.EmployeeId, readProjectsEmployee.EmployeeId);

            #endregion

            #region Read Many

            var readProjectsEmployees = projectsEmployeeRepository.Get();
            Assert.IsTrue(readProjectsEmployees.Count() > 1);

            #endregion

            #region Update

            projectsEmployee = FixturesGenerator.GenerateProjectsEmployee(projects, employees);
            readProjectsEmployee.EmployeeId = projectsEmployee.EmployeeId;
            readProjectsEmployee.ProjectId = projectsEmployee.ProjectId;
            ProjectsEmployee updatedProjectsEmployee = projectsEmployeeRepository.Update(readProjectsEmployee);
            Assert.AreEqual(projectsEmployee.EmployeeId, updatedProjectsEmployee.EmployeeId);

            #endregion

            #region Delete

            projectsEmployeeRepository.Remove(updatedProjectsEmployee);
            readProjectsEmployee = projectsEmployeeRepository.Get(updatedProjectsEmployee.Id);
            Assert.IsNull(readProjectsEmployee);

            #endregion

            #region Delete By Id

            savedEmployee = projectsEmployeeRepository.Add(projectsEmployee);
            projectsEmployeeRepository.Remove(savedEmployee.Id);
            readProjectsEmployee = projectsEmployeeRepository.Get(savedEmployee.Id);
            Assert.IsNull(readProjectsEmployee);

            #endregion

        }
    }
}