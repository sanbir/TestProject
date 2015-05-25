using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
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
                        where p.Id == projectId
                        select pe.EmployeeId).Distinct().ToList();
            }
        }

        public void Remove(int projectId, int employeeId)
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                var projectsEmployees = from pe in entityContext.ProjectsEmployees
                    where pe.ProjectId == projectId && pe.EmployeeId == employeeId
                    select pe;

                foreach (var projectsEmployee in projectsEmployees)
                {
                    entityContext.Entry(projectsEmployee).State = EntityState.Deleted;
                }

                entityContext.SaveChanges();
            }
        }
    }
}
