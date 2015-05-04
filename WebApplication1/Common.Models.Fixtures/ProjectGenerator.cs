using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Ploeh.AutoFixture;

namespace Common.Models.Fixtures
{
    public class ProjectGenerator:FixtureGenerator<Project>
    {
        public override Project GenerateFixture(ICollection<Employee> employees)
        {
            Project project = new Project();

            project.ProjectId = Fixture.Create<int>();
            project.ProjectName = Fixture.Create<string>();
            project.CustomerCompanyName = Fixture.Create<string>();
            project.ManagerId = employees.ElementAt(new Random().Next(employees.Count)).EmployeeId;
            project.StartDate = Fixture.Create<DateTime>();
            project.EndDate = project.StartDate + new TimeSpan(Fixture.Create<int>(), 0, 0, 0);
            project.Priority = Math.Abs(Fixture.Create<int>());
            project.Comment = Fixture.Create<string>();

            return project;
        }
    }
}
