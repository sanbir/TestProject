using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace BusinessLayer.Contracts.Managers
{
    public interface IEmployeeManager : IManager<Employee>
    {
        IEnumerable<Employee> GetAll(ListSortDirection sortDirection,
            PropertyDescriptor sortPropertyDescriptor, string filter);

        IEnumerable<Employee> GetAll(string sortDirection,
            string sortPropertyName, string filter);

        Employee Get(int id);

        void CreateOrUpdate(Employee employee);
    }
}
