using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts.DataRepositories;
using Data.Models;

namespace DataAccessLayer.DataRepositories
{
    [Export(typeof(IEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
        protected override Employee AddEntity(BiryukovTestDbContext entityContext, Employee entity)
        {
            return entityContext.Employees.Add(entity);
        }

        protected override Employee UpdateEntity(BiryukovTestDbContext entityContext, Employee entity)
        {
            return (from e in entityContext.Employees
                    where e.EmployeeId == entity.EmployeeId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Employee> GetEntities(BiryukovTestDbContext entityContext)
        {
            return from e in entityContext.Employees
                   select e;
        }

        protected override Employee GetEntity(BiryukovTestDbContext entityContext, int id)
        {
            var query = (from e in entityContext.Employees
                         where e.EmployeeId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

    }
}
