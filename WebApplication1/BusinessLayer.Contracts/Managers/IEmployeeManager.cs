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
        IEnumerable<Employee> GetAllEmployeesSortedAndFiltered(ListSortDirection sortDirection,
            PropertyDescriptor sortPropertyDescriptor, string filter);

        IEnumerable<Employee> GetAllEmployeesSortedAndFiltered(string sortDirection,
            string sortPropertyName, string filter);
    }
}
