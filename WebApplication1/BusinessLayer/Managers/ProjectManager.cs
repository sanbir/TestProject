using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace BusinessLayer.Managers
{
    public class ProjectManager : ManagerBase //, IInventoryService
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

        public Project UpdateProject(Project project)
        {

            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

            Project updatedEntity = null;

            if (project.Id == 0)
                updatedEntity = projectRepository.Add(project);
            else
                updatedEntity = projectRepository.Update(project);

            return updatedEntity;
        }

        public void DeleteProject(int projectId)
        {

            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

            projectRepository.Remove(projectId);
        }

        public Project GetProject(int projectId)
        {
            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

            Project projectEntity = projectRepository.Get(projectId);
            if (projectEntity == null)
            {
                //NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in database", ProjectId));
            }

            return projectEntity;
        }

        public Project[] GetAllProjects()
        {
            IProjectRepository projectRepository = _dataRepositoryFactory.GetDataRepository<IProjectRepository>();

            IEnumerable<Project> projects = projectRepository.Get();

            foreach (Project project in projects)
            {
                //Rental rentedCar = rentedCars.Where(item => item.CarId == Project.CarId).FirstOrDefault();
                //Project.CurrentlyRented = (rentedCar != null);
            }

            return projects.ToArray();
        }

    }
}
