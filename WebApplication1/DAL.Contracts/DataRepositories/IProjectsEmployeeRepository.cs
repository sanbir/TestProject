using System.Collections.Generic;
using Shared.Models;

namespace DAL.Contracts.DataRepositories
{
    public interface IProjectsEmployeeRepository : IDataRepository<ProjectsEmployee>
    {
        IEnumerable<int> GetAssignedEmployeesIds(int projectId);

        void Remove(int projectId, int employeeId);
    }
}
