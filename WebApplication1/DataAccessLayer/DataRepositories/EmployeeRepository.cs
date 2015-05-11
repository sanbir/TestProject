using System.ComponentModel.Composition;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace DataAccessLayer.DataRepositories
{
    [Export(typeof(IEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
    }
}
