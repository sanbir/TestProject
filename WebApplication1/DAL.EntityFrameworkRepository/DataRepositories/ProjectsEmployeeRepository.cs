using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using DAL.Contracts.DataRepositories;
using Shared.Models;

namespace DAL.EntityFrameworkRepository.DataRepositories
{
    [Export(typeof(IProjectsEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectsEmployeeRepository : DataRepositoryBase<ProjectsEmployee>, IProjectsEmployeeRepository
    {
        public IEnumerable<int> GetAssignedEmployeesIds(int projectId)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                return (from p in entityContext.Projects
                        join pe in entityContext.ProjectsEmployees
                            on p.Id equals pe.ProjectId
                        select pe.EmployeeId).ToList();
            }
        }
    }
}
