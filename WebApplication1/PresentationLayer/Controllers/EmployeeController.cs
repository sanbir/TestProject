using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using BusinessLayer.Contracts.Managers;
using Data.Models;
using PagedList;

namespace ContosoUniversity.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeController : Controller
    {
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
            ViewBag.CurrentSortDirection = sortDirection;
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