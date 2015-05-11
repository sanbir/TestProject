using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.Managers;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace BusinessLayer.Managers
{
    [Export(typeof(IProjectManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectManager : ManagerBase, IProjectManager
    {
        public ProjectManager()
        {
        }

        public ProjectManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        void IManager<Project>.CreateOrUpdate(Project project)
        {
            CreateOrUpdate(project);
        }

        public Project CreateOrUpdate(Project project)
        {
            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

            Project updatedEntity = project.Id == 0
                ? projectRepository.Add(project)
                : projectRepository.Update(project);

            return updatedEntity;
        }

        public void Delete(int projectId)
        {
            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

            projectRepository.Remove(projectId);
        }

        public IEnumerable<Project> GetAll(ListSortDirection sortDirection, PropertyDescriptor sortPropertyDescriptor, string filter)
        {
            var projects = GetAll();

            if (!String.IsNullOrEmpty(filter))
            {
                //projects =
                //    projects.Where(project =>
                //        project.ProjectName.ToLowerInvariant()
                //            .Contains(filter.ToLowerInvariant())
                //        ||
                //        project.CustomerCompanyName.ToLowerInvariant()
                //            .Contains(filter.ToLowerInvariant())
                //        ||
                //        (project.MiddleName != null && project.MiddleName.ToLowerInvariant()
                //            .Contains(filter.ToLowerInvariant()))
                //        || project.Email.ToLowerInvariant()
                //            .Contains(filter.ToLowerInvariant())
                //        ||
                //        project.ContractorCompanyName.ToLowerInvariant()
                //            .Contains(filter.ToLowerInvariant()));
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
        }

        public IEnumerable<Project> GetAll()
        {
            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();
            IEnumerable<Project> projects = projectRepository.Get();
            return projects;
        }

        public IEnumerable<Project> GetAll(string sortDirection, string sortPropertyName, string filter)
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
        }

        public Project Get(int projectId)
        {
            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();
            Project projectEntity = projectRepository.Get(projectId);
            return projectEntity;
        }

    }
}
