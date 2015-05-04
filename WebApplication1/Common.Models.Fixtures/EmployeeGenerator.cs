using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Ploeh.AutoFixture;

namespace Common.Models.Fixtures
{
    public class EmployeeGenerator : FixtureGenerator<Employee>
    {
        public override Employee GenerateFixture()
        {
            Employee employee = new Employee();

            employee.EmployeeId = Fixture.Create<int>();
            employee.FirstName = Fixture.Create<string>();
            employee.LastName = Fixture.Create<string>();
            employee.MiddleName = Fixture.Create<string>();
            employee.Email = Fixture.Create<string>();
            employee.ContractorCompanyName = Fixture.Create<string>();

            return employee;
        }
    }
}
