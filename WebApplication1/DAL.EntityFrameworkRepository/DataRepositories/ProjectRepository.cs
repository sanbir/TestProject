using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using Shared.Models;

namespace DAL.EntityFrameworkRepository.DataRepositories
{
    [Export(typeof(IProjectRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {
        IEnumerable<Project> IDataRepository<Project>.Get()
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                var dto = (from p in entityContext.Projects
                          join e in entityContext.Employees
                              on p.ManagerId equals e.Id
                          select new
                          {
                              Id = p.Id,
                              ProjectName = p.ProjectName,
                              CustomerCompanyName = p.CustomerCompanyName,
                              ManagerId = p.ManagerId,
                              Manager = e,
                              StartDate = p.StartDate,
                              EndDate = p.EndDate,
                              Priority = p.Priority,
                              Comment = p.Comment
                          }).ToList();
                return (from p in dto
                        select new Project
                        {
                            Id = p.Id,
                            ProjectName = p.ProjectName,
                            CustomerCompanyName = p.CustomerCompanyName,
                            ManagerId = p.ManagerId,
                            Manager = p.Manager,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            Priority = p.Priority,
                            Comment = p.Comment
                        }).ToList();
            }
        }

        Project IDataRepository<Project>.Get(int id)
        {
            Project project;

            using (var entityContext = new BiryukovTestDbContext())
            {
                IEnumerable<Employee> employees = from e in entityContext.Employees
                                                  select e;

                project = (from p in entityContext.Projects
                           where p.Id == id
                           select p).FirstOrDefault();

                if (project != null)
                    project.Manager = (from employee in employees
                                       where employee.Id == project.ManagerId
                                       select employee).FirstOrDefault();
            }

            return project;
        }
    }
}
