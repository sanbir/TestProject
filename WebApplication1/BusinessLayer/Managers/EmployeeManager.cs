using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using BusinessLayer.Contracts;
using BusinessLayer.Contracts.Managers;
using DAL.Contracts;
using DAL.Contracts.DataRepositories;
using Shared.Models;


namespace BusinessLayer.Managers
{
    [Export(typeof(IEmployeeManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeManager : EmployeeManagerBase, IEmployeeManager
    {
        public EmployeeManager()
        {
        }

        public EmployeeManager(IDataRepositoryFactory dataRepositoryFactory)
            : base(dataRepositoryFactory)
        {
            _dataRepositoryFactory = dataRepositoryFactory;
        }

        [Import]
        IDataRepositoryFactory _dataRepositoryFactory;

        void IManager<Employee>.CreateOrUpdate(Employee employee)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                CreateOrUpdate(employee);
            });
        }

        public Employee CreateOrUpdate(Employee employee)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

                Employee updatedEntity = employee.Id == 0
                    ? employeeRepository.Add(employee)
                    : employeeRepository.Update(employee);

                return updatedEntity;
            });
        }

        public void Delete(int employeeId)
        {
            ExecuteExceptionHandledOperation(() =>
            {
                IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

                //TODO : check if assigned

                employeeRepository.Remove(employeeId);
            });
        }

        public IEnumerable<Employee> GetAll(ListSortDirection sortDirection, PropertyDescriptor sortPropertyDescriptor, string filter)
        {
            return ExecuteExceptionHandledOperation(() => GetAllEmployees(sortDirection, sortPropertyDescriptor, filter));
        }

        public IEnumerable<Employee> GetAll()
        {
            return ExecuteExceptionHandledOperation(() => GetAllEmployees());
        }

        public IEnumerable<Employee> GetAll(string sortDirection, string sortPropertyName, string filter)
        {
            return ExecuteExceptionHandledOperation(() => GetAllEmployees(sortDirection, sortPropertyName, filter));
        }

        public Employee Get(int employeeId)
        {
            return ExecuteExceptionHandledOperation(() =>
            {
                IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                Employee employeeEntity = employeeRepository.Get(employeeId);
                return employeeEntity;
            });
        }

    }
}
