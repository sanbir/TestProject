using System.Collections.Generic;
using Shared.Models;

namespace BusinessLayer.Contracts.Managers
{
    public interface IProjectManager : IManager<Project>
    {
        IEnumerable<Employee> GetAllEmployees(string sortDirection, string sortPropertyName, string filter);
    }
}
