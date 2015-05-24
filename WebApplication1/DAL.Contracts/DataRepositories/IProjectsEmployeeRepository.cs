using System.Collections.Generic;
using Shared.Models;

namespace DAL.Contracts.DataRepositories
{
    public interface IProjectsEmployeeRepository : IDataRepository<ProjectsEmployee>
    {
        IEnumerable<int> GetAssignedEmployeesIds(int projectId);
    }
}
