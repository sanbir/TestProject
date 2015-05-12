using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Shared.Models;

namespace Tests.Fixtures
{
    public static class FixturesGenerator
    {
        static readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        public static ICollection<Employee> GenerateEmployees()
        {
            return GenerateEmployees(_fixture.Create<int>());
        }

        public static ICollection<Employee> GenerateEmployees(int numberOfEmployees)
        {
            ICollection<Employee> employees = new List<Employee>();

            for (int i = 0; i < numberOfEmployees; i++)
            {
                employees.Add(GenerateEmployee());
            }

            return employees;
        }

        public static Employee GenerateEmployee()
        {
            Employee employee = new Employee();

            employee.FirstName = _fixture.Create<string>();
            employee.LastName = _fixture.Create<string>();
            employee.MiddleName = _fixture.Create<string>();
            employee.Email = _fixture.Create<string>() + "@" + _fixture.Create<string>() + ".com";
            employee.ContractorCompanyName = _fixture.Create<string>();

            return employee;
        }

        public static ICollection<Project> GenerateProjects()
        {
            return GenerateProjects(_fixture.Create<int>());
        }

        public static ICollection<Project> GenerateProjects(int numberOfProjects)
        {
            ICollection<Project> projects = new List<Project>();

            for (int i = 0; i < numberOfProjects; i++)
            {
                projects.Add(GenerateProject(GenerateEmployees()));
            }

            return projects;
        }

        public static Project GenerateProject(ICollection<Employee> employees)
        {
            Project project = new Project();

            project.ProjectName = _fixture.Create<string>();
            project.CustomerCompanyName = _fixture.Create<string>();
            project.ManagerId = employees.ElementAt(new Random().Next(employees.Count)).Id;
            project.StartDate = _fixture.Create<DateTime>();
            project.EndDate = project.StartDate + new TimeSpan(_fixture.Create<int>(), 0, 0, 0);
            project.Priority = Math.Abs(_fixture.Create<int>());
            project.Comment = _fixture.Create<string>();

            return project;
        }

        public static ProjectsEmployee GenerateProjectsEmployee(ICollection<Project> projects, ICollection<Employee> employees)
        {
            ProjectsEmployee projectsEmployee = new ProjectsEmployee();

            projectsEmployee.ProjectId = projects.ElementAt(new Random().Next(projects.Count)).Id;
            projectsEmployee.EmployeeId = employees.ElementAt(new Random().Next(employees.Count)).Id;

            return projectsEmployee;
        }
    }
}
