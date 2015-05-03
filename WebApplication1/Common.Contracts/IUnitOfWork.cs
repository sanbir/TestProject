using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Contracts
{
    public interface IUnitOfWork
    {
        IDataRepository<Employee> Employees { get; set; }
        IDataRepository<Project> Projects { get; set; }
        IDataRepository<ProjectsEmployee> ProjectsEmployees { get; set; }
        void Commit();
    }
}
