using System.Collections.Generic;
using Shared.Models;

namespace BusinessLayer.Contracts.Managers
{
    public interface IProjectManager : IManager<Project>
    {
        List<Employee> GetAllEmployees(string sortDirection, string sortPropertyName, string filter, int pageNumber,
            int pageSize, out int pageCount);
    }
}
