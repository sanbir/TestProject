using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Common.Models.Fixtures
{
    public class FixturesGenerator
    {
        readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        private ICollection<Employee> GenerateEmployees()
        {
            return GenerateEmployees(_fixture.Create<int>());
        }

        private ICollection<Employee> GenerateEmployees(int numberOfEmployees)
        {
            ICollection<Employee> employees = new List<Employee>();

            for (int i = 0; i < numberOfEmployees; i++)
            {
                employees.Add(GenerateEmployee());
            }

            return employees;
        }

        private Employee GenerateEmployee()
        {
            Employee employee = new Employee();

            employee.EmployeeId = _fixture.Create<int>();
            employee.FirstName = _fixture.Create<string>();
            employee.LastName = _fixture.Create<string>();
            employee.MiddleName = _fixture.Create<string>();
            employee.Email = _fixture.Create<string>();
            employee.ContractorCompanyName = _fixture.Create<string>();

            return employee;
        }

        private Project GenerateProject(ICollection<Employee> employees)
        {
            Project project = new Project();

            project.ProjectId = _fixture.Create<int>();
            project.ProjectName = _fixture.Create<string>();
            project.CustomerCompanyName = _fixture.Create<string>();
            project.ManagerId = employees.ElementAt(new Random().Next(employees.Count)).EmployeeId;
            project.StartDate = _fixture.Create<DateTime>();
            project.EndDate = project.StartDate + new TimeSpan(_fixture.Create<int>(), 0, 0, 0);
            project.Priority = Math.Abs(_fixture.Create<int>());
            project.Comment = _fixture.Create<string>();

            return project;
        }

        private ProjectsEmployee GenerateProjectsEmployee(ICollection<Project> projects, ICollection<Employee> employees)
        {
            ProjectsEmployee projectsEmployee = new ProjectsEmployee();

            projectsEmployee.Id = _fixture.Create<int>();
            projectsEmployee.ProjectId = projects.ElementAt(new Random().Next(projects.Count)).ProjectId;
            projectsEmployee.EmployeeId = employees.ElementAt(new Random().Next(employees.Count)).EmployeeId;

            return projectsEmployee;
        }
    }
}
