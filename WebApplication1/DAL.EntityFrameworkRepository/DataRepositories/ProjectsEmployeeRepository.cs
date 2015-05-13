using System.ComponentModel.Composition;
using DAL.Contracts.DataRepositories;
using Shared.Models;

namespace DAL.EntityFrameworkRepository.DataRepositories
{
    [Export(typeof(IProjectsEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectsEmployeeRepository : DataRepositoryBase<ProjectsEmployee>, IProjectsEmployeeRepository
    {
    }
}
