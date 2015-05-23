using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Mvc;
using BusinessLayer.Contracts.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PagedList;
using Shared.Constants.Common;
using Shared.Constants.Employee;
using Shared.Constants.Project;
using Shared.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectController : Controller
    {
        public ProjectController()
        {
        }

        [ImportingConstructor]
        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [Import]
        IProjectManager _projectManager;

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetEmployees(string sortDirection, string sortPropertyName, string currentFilter, string searchString, int? page)
        {
            PagedEmployeesViewModel viewModel = new PagedEmployeesViewModel();

            sortDirection = SwapSortDirection(sortDirection);
            viewModel.CurrentSortDirection = sortDirection;

            if (string.IsNullOrEmpty(sortPropertyName))
            {
                sortPropertyName = EmployeeProperties.LastName;
            }

            if (searchString == null)
            {
                searchString = currentFilter;
            }
            viewModel.CurrentFilter = searchString;
            
            const int pageSize = ViewStringConstants.PageSize;
            int pageNumber = page ?? ViewStringConstants.StartPage;
            int pageCount;

            var employees =
                _projectManager.GetAllEmployees(sortDirection, sortPropertyName, searchString, pageNumber, pageSize,
                    out pageCount)
                    .Select(
                        employee =>
                            new PagedEmployeesViewModel.PlainEmployee(employee.FirstName, employee.LastName,
                                employee.MiddleName, employee.Email, employee.ContractorCompanyName));
            viewModel.Employees = employees;
            viewModel.PageSize = pageSize;
            viewModel.PageNumber = pageNumber;
            viewModel.PageCount = pageCount;

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonProjectViewModel = JsonConvert.SerializeObject(viewModel, Formatting.None, settings);
            return Json(jsonProjectViewModel);
        }

        //public JsonResult GetEmployees()
        //{
        //    IEnumerable<Employee> employees = _projectManager.GetAllEmployees();
        //    IEnumerable<AssignedEmployeeData> assignedEmployeeData =
        //        employees.Select(employee => new AssignedEmployeeData(employee));

        //    CreateProjectViewModel model = new CreateProjectViewModel();
        //    model.Project = null;
        //    model.Employees = assignedEmployeeData.ToPagedList(ViewStringConstants.StartPage, ViewStringConstants.PageSize);

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = ProjectProperties.BindProjectProperties)]ProjectPartialViewModel projectViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    var jsonProjectViewModel = JsonConvert.SerializeObject(projectViewModel, Formatting.None, settings);
                    return View("AssignEmployees", string.Empty, jsonProjectViewModel);
                }
            }
            catch (Exception) // TODO: add custom
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotSave);
            }
            return View();
        }

        [HttpPost]
        public JsonResult Persist(ProjectToPersistViewModel projectViewModel)
        {
            try
            {
                if (projectViewModel != null)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception) // TODO: add custom
            {
                // TODO:
            }
            return new JsonResult();
        }

        public ActionResult Index(string sortDirection, string sortPropertyName, string currentFilter, string searchString, int? page)
        {
            sortDirection = SwapSortDirection(sortDirection);
            ViewBag.CurrentSortDirection = sortDirection;

            if (string.IsNullOrEmpty(sortPropertyName))
            {
                sortPropertyName = ProjectProperties.ProjectName;
            }

            if (searchString == null)
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var projects = _projectManager.GetAll(sortDirection, sortPropertyName, searchString);

            const int pageSize = ViewStringConstants.PageSize;
            int pageNumber = page ?? ViewStringConstants.StartPage;
            return View(projects.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = _projectManager.Get((int)id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = _projectManager.Get((int)id);
            if (TryUpdateModel(project, string.Empty,
                new[] { ProjectProperties.ProjectName, ProjectProperties.CustomerCompanyName, ProjectProperties.StartDate, ProjectProperties.EndDate, ProjectProperties.Priority, ProjectProperties.Comment }))
            {
                try
                {
                    _projectManager.CreateOrUpdate(project);
                    return RedirectToAction("Index");
                }
                catch (Exception) // TODO: add custom
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotSave);
                }
            }
            return View(project);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = _projectManager.Get((int)id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
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
            Project project = _projectManager.Get((int)id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _projectManager.Delete(id);
            }
            catch (Exception)// TODO:
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
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