using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using BusinessLayer.Contracts.Managers;
using Data.Models;
using PagedList;
using Utils;

namespace ContosoUniversity.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeController : Controller
    {
        public EmployeeController()
        {
        }

        [ImportingConstructor]
        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [Import]
        IEmployeeManager _employeeManager;

        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Index(string sortDirection, string sortPropertyName, string currentFilter, string searchString, int? page)
        {
            sortDirection = sortDirection == ListSortDirection.Ascending.ToString()
                ? ListSortDirection.Descending.ToString()
                : ListSortDirection.Ascending.ToString();
            ViewBag.CurrentSortDirection = sortDirection;

            if (string.IsNullOrEmpty(sortPropertyName))
            {
                sortPropertyName = new Employee().GetPropertyNameFor(e => e.LastName);
            }
            ViewBag.CurrentPropertyName = sortPropertyName;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var employees = _employeeManager.GetAllEmployeesSortedAndFiltered(sortDirection, sortPropertyName, searchString);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(object id)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Details(object id)
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Delete(object id)
        {
            throw new System.NotImplementedException();
        }
    }
}