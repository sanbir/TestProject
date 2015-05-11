using System.ComponentModel.Composition;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace DataAccessLayer.DataRepositories
{
    [Export(typeof(IProjectRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectRepository : DataRepositoryBase<Project>, IProjectRepository
    {
    }
}
