using System.ComponentModel.Composition;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace DataAccessLayer.DataRepositories
{
    [Export(typeof(IProjectsEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectsEmployeeRepository : DataRepositoryBase<ProjectsEmployee>, IProjectsEmployeeRepository
    {
    }
}
