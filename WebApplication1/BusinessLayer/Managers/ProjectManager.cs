﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.Managers;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using Shared.Exceptions;
using Shared.Models;


namespace BusinessLayer.Managers
{
    [Export(typeof(IProjectManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectManager : EmployeeManagerBase, IProjectManager
    {
        public ProjectManager()
        {
        }

        public ProjectManager(IDataRepositoryFactory dataRepositoryFactory)
            : base(dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        void IManager<Project>.CreateOrUpdate(Project project)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                CreateOrUpdate(project); 
            });
        }

        public Project CreateOrUpdate(Project project)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

                Project updatedProject = project.Id == 0
                    ? projectRepository.Add(project)
                    : projectRepository.Update(project);

                return updatedProject;
            });
        }

        public void Delete(int projectId)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

                projectRepository.Remove(projectId);
            });
        }

        public IEnumerable<Project> GetAll(ListSortDirection sortDirection, PropertyDescriptor sortPropertyDescriptor, string filter)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                var projects = GetAll();

                if (!String.IsNullOrEmpty(filter))
                {
                    // TODO: filter dates
                    projects =
                        projects.Where(project =>
                            project.ProjectName.ToLowerInvariant()
                                .Contains(filter.ToLowerInvariant())
                            ||
                            project.CustomerCompanyName.ToLowerInvariant()
                                .Contains(filter.ToLowerInvariant())
                            ||
                            (project.Comment != null && project.Comment.ToLowerInvariant()
                                .Contains(filter.ToLowerInvariant()))
                            || project.Priority.ToString() == filter);
                }

                switch (sortDirection)
                {
                    case ListSortDirection.Ascending:
                        projects = sortPropertyDescriptor == null
                            ? projects.OrderBy(project => project.ProjectName)
                            : projects.OrderBy(sortPropertyDescriptor.GetValue);
                        break;
                    case ListSortDirection.Descending:
                        projects = sortPropertyDescriptor == null
                            ? projects.OrderByDescending(project => project.ProjectName)
                            : projects.OrderByDescending(sortPropertyDescriptor.GetValue);
                        break;
                }

                return projects;
            });
        }

        public IEnumerable<Project> GetAll()
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();
                IEnumerable<Project> projects = projectRepository.Get();
                return projects;
            });
        }

        public IEnumerable<Project> GetAll(string sortDirection, string sortPropertyName, string filter)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                ListSortDirection direction = ListSortDirection.Ascending;
                if (!string.IsNullOrEmpty(sortDirection))
                {
                    Enum.TryParse(sortDirection, out direction);
                }

                PropertyDescriptor descriptor = null;
                if (!string.IsNullOrEmpty(sortPropertyName))
                {
                    descriptor = TypeDescriptor.GetProperties(new Project()).Find(sortPropertyName, false);
                }

                return GetAll(direction, descriptor, filter);
            });
        }

        List<Employee> IProjectManager.GetAllEmployees(string sortDirection, string sortPropertyName,
            string filter, int pageNumber, int pageSize, out int pageCount)
        {
            try
            {
                return GetAllEmployees(sortDirection, sortPropertyName, filter, pageNumber, pageSize, out pageCount);
            }
            catch (Exception ex)
            {
                // TODO: Logger.Error(ex);
                throw new BusinessLayerException(ex.Message, ex);
            }
        }

        public void CreateOrUpdateAndAssignEmployees(Project project, ICollection<int> assignedEmployeesIds)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                var updatedProject = CreateOrUpdate(project);
                AssignEmployees(updatedProject.Id, assignedEmployeesIds);
            });
        }

        public void AssignEmployees(int projectId, ICollection<int> assignedEmployeesIds)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                IProjectsEmployeeRepository projectsEmployeeRepository =
                    _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

                var formerAssignedEmployeesIds = GetAssignedEmployeesIds(projectId);

                foreach (var formerAssignedEmployeesId in formerAssignedEmployeesIds)
                {
                    if (assignedEmployeesIds.Contains(formerAssignedEmployeesId))
                    {
                        assignedEmployeesIds.Remove(formerAssignedEmployeesId);
                    }
                    else
                    {
                        projectsEmployeeRepository.Remove(projectId, formerAssignedEmployeesId);
                    }
                }

                foreach (var newlyAssignedEmployeesId in assignedEmployeesIds)
                {
                    var projectsEmployee = new ProjectsEmployee
                    {
                        ProjectId = projectId,
                        EmployeeId = newlyAssignedEmployeesId
                    };

                    projectsEmployeeRepository.Add(projectsEmployee);
                }
            });
        }

        public List<int> GetAssignedEmployeesIds(int projectId)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IProjectsEmployeeRepository projectsEmployeeRepository =
                    _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

                return projectsEmployeeRepository.GetAssignedEmployeesIds(projectId).ToList();
            });
        }

        public List<Employee> GetAssignedEmployees(int projectId)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IProjectsEmployeeRepository projectsEmployeeRepository =
                    _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

                return projectsEmployeeRepository.GetAssignedEmployees(projectId).ToList();
            });
        }

        public Project Get(int projectId)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();
                Project projectEntity = projectRepository.Get(projectId);
                return projectEntity;
            });
        }

    }
}
