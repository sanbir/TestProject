using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Data.Contracts;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace BusinessLayer.Managers
{
    public class ProjectsEmployeeManager : ManagerBase //, IInventoryService
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
                //NotFoundException ex = new NotFoundException(string.Format("Car with ID of {0} is not in database", ProjectsEmployeeId));
            }

            return projectsEmployeeEntity;
        }

        public ProjectsEmployee[] GetAllProjectsEmployees()
        {
            IProjectsEmployeeRepository projectsEmployeeRepository = _dataRepositoryFactory.GetDataRepository<IProjectsEmployeeRepository>();

            IEnumerable<ProjectsEmployee> projectsEmployees = projectsEmployeeRepository.Get();

            foreach (ProjectsEmployee projectsEmployee in projectsEmployees)
            {
                //Rental rentedCar = rentedCars.Where(item => item.CarId == ProjectsEmployee.CarId).FirstOrDefault();
                //ProjectsEmployee.CurrentlyRented = (rentedCar != null);
            }

            return projectsEmployees.ToArray();
        }

    }
}
