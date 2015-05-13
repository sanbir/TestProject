using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using Shared.Models;


namespace BusinessLayer.Managers
{
    public class ProjectsEmployeeManager : ManagerBase
    {
        public ProjectsEmployeeManager()
        {
        }

        public ProjectsEmployeeManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        public ProjectsEmployee UpdateProjectsEmployee(ProjectsEmployee projectsEmployee)
        {

            IProjectsEmployeeRepository projectsEmployeeRepository = _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

            ProjectsEmployee updatedEntity = null;

            if (projectsEmployee.Id == 0)
                updatedEntity = projectsEmployeeRepository.Add(projectsEmployee);
            else
                updatedEntity = projectsEmployeeRepository.Update(projectsEmployee);

            return updatedEntity;
        }

        public void DeleteProjectsEmployee(int projectsEmployeeId)
        {

            IProjectsEmployeeRepository projectsEmployeeRepository = _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

            projectsEmployeeRepository.Remove(projectsEmployeeId);
        }

        public ProjectsEmployee GetProjectsEmployee(int projectsEmployeeId)
        {
            IProjectsEmployeeRepository projectsEmployeeRepository = _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

            ProjectsEmployee projectsEmployeeEntity = projectsEmployeeRepository.Get(projectsEmployeeId);
            if (projectsEmployeeEntity == null)
            {
                // TODO exception
            }

            return projectsEmployeeEntity;
        }

        public ProjectsEmployee[] GetAllProjectsEmployees()
        {
            IProjectsEmployeeRepository projectsEmployeeRepository = _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

            IEnumerable<ProjectsEmployee> projectsEmployees = projectsEmployeeRepository.Get();

            return projectsEmployees.ToArray();
        }

    }
}
