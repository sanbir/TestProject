using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Common.Models.Fixtures;
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
        [TestMethod]
        public void TestEmployeeRepository()
        {
            Employee employee = FixturesGenerator.GenerateEmployee();
            IEmployeeRepository employeeRepository = new EmployeeRepository();

            #region Create

            Employee savedEmployee = employeeRepository.Add(employee);
            Assert.AreEqual(employee, savedEmployee);

            #endregion

            #region Read

            employeeRepository.Add(employee);
            Employee readEmployee = employeeRepository.Get(savedEmployee.Id);
            Assert.AreEqual(employee.Email, readEmployee.Email);

            #endregion

            #region Read Many

            var readEmployees = employeeRepository.Get();
            Assert.IsTrue(readEmployees.Count() > 1);

            #endregion

            #region Update

            employee = FixturesGenerator.GenerateEmployee();
            readEmployee.FirstName = employee.FirstName;
            readEmployee.LastName = employee.LastName;
            readEmployee.MiddleName = employee.MiddleName;
            readEmployee.Email = employee.Email;
            readEmployee.ContractorCompanyName = employee.ContractorCompanyName;
            Employee updatedEmployee = employeeRepository.Update(readEmployee);
            Assert.AreEqual(employee.Email, updatedEmployee.Email); 

            #endregion

            #region Delete

            employeeRepository.Remove(updatedEmployee);
            readEmployee = employeeRepository.Get(updatedEmployee.Id);
            Assert.IsNull(readEmployee);

            #endregion

            #region Delete By Id

            savedEmployee = employeeRepository.Add(employee);
            employeeRepository.Remove(savedEmployee.Id);
            readEmployee = employeeRepository.Get(savedEmployee.Id);
            Assert.IsNull(readEmployee);

            #endregion

        }

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
        

    }
}