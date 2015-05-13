using System.ComponentModel.Composition;
using DAL.Contracts.DataRepositories;
using Shared.Models;

namespace DAL.EntityFrameworkRepository.DataRepositories
{
    [Export(typeof(IEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
    }
}
