using System.Collections.Generic;
using Shared.Models;

namespace BusinessLayer.Contracts.Managers
{
    public interface IProjectManager : IManager<Project>
    {
        List<Employee> GetAllEmployees(string sortDirection, string sortPropertyName, string filter, int pageNumber,
            int pageSize, out int pageCount);

        void CreateOrUpdateAndAssignEmployees(Project project, ICollection<int> assignedEmployeesIds);

        void AssignEmployees(int projectId, ICollection<int> assignedEmployeesIds);
    }
}
