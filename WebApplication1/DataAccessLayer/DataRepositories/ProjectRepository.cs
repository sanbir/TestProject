using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace DataAccessLayer.DataRepositories
{
    [Export(typeof(IProjectRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {
        IEnumerable<Project> IDataRepository<Project>.Get()
        {
            using (var entityContext = new BiryukovTestDbContext())
            {
                //var projects = (from project in entityContext.Projects
                //                select project).ToList();

                //var employee = (from p in projects
                //                join e in entityContext.Employees
                //                    on p.ManagerId equals e.Id
                //                select e).FirstOrDefault();

                //return projects.Select(p => new Project
                //{
                //    Id = p.Id,
                //    ProjectName = p.ProjectName,
                //    CustomerCompanyName = p.CustomerCompanyName,
                //    ManagerId = p.ManagerId,
                //    Manager = employee,
                //    StartDate = p.StartDate,
                //    EndDate = p.EndDate,
                //    Priority = p.Priority,
                //    Comment = p.Comment
                //}).ToList();

                var re = (from p in entityContext.Projects
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
                return (from p in re
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
