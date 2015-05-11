using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Mvc;
using BusinessLayer.Contracts.Managers;
using Common.Constants.Common;
using Common.Constants.Employee;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = EmployeeProperties.BindEmployeeProperties)]Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeManager.CreateOrUpdate(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) // TODO: add custom
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotSave);
            }
            return View(employee);
        }

        public ActionResult Index(string sortDirection, string sortPropertyName, string currentFilter, string searchString, int? page)
        {
            sortDirection = SwapSortDirection(sortDirection);
            ViewBag.CurrentSortDirection = sortDirection;

            if (string.IsNullOrEmpty(sortPropertyName))
            {
                sortPropertyName = DefaultSortPropertyName;
            }

            if (searchString == null)
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var employees = _employeeManager.GetAll(sortDirection, sortPropertyName, searchString);

            const int pageSize = 3;
            int pageNumber = page ?? 1;
            return View(employees.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = _employeeManager.Get((int)id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employeeManager.Get((int)id);
            if (TryUpdateModel(employee, string.Empty,
                new[] { EmployeeProperties.LastName, EmployeeProperties.FirstName, EmployeeProperties.MiddleName, EmployeeProperties.Email, EmployeeProperties.ContractorCompanyName }))
            {
                try
                {
                    _employeeManager.CreateOrUpdate(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception) // TODO: add custom
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotSave);
                }
            }
            return View(employee);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = _employeeManager.Get((int)id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = ErrorMessages.CouldNotDelete;
            }
            Employee employee = _employeeManager.Get((int)id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _employeeManager.Delete(id);
            }
            catch (Exception)// TODO:
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        private static string DefaultSortPropertyName
        {
            get { return new Employee().GetPropertyNameFor(e => e.LastName); }
        }

        private static string SwapSortDirection(string sortDirection)
        {
            sortDirection = sortDirection == ListSortDirection.Ascending.ToString()
                ? ListSortDirection.Descending.ToString()
                : ListSortDirection.Ascending.ToString();
            return sortDirection;
        }
    }
}