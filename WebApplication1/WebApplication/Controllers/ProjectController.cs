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

        [HttpPost]
        public string GetEmployees(string sortDirection, string sortPropertyName, string searchString, int? page)
        {
            const int pageSize = ViewStringConstants.PageSize;
            int pageNumber = page ?? ViewStringConstants.StartPage;
            int pageCount;

            var modelEmployees = _projectManager.GetAllEmployees(sortDirection, sortPropertyName, searchString,
                pageNumber, pageSize, out pageCount);

            var viewModelEmployees =
                modelEmployees.Select(employee => new PagedEmployeesViewModel.PlainEmployee
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName,
                    Email = employee.Email,
                    ContractorCompanyName = employee.ContractorCompanyName
                }).ToList();

            var viewModel = new PagedEmployeesViewModel
            {
                Employees = viewModelEmployees,
                PageSize = pageSize,
                PageNumber = pageNumber,
                PageCount = pageCount
            };

            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonViewModel = JsonConvert.SerializeObject(viewModel, Formatting.None, settings);
            return jsonViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = ProjectProperties.BindProjectProperties)]ProjectViewModel projectViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ActionToPerform = ViewStringConstants.Create;

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
        public string Persist([Bind(Include = ProjectProperties.BindProjectPropertiesWithManagerId)]ProjectViewModel projectViewModel)
        {
            try
            {
                if (projectViewModel == null) return "Error";

                if (ModelState.IsValid)
                {
                    var project = new Project
                    {
                        Id = projectViewModel.Id,
                        ProjectName = projectViewModel.ProjectName,
                        CustomerCompanyName = projectViewModel.CustomerCompanyName,
                        ManagerId = projectViewModel.ManagerId,
                        StartDate = projectViewModel.StartDate,
                        EndDate = projectViewModel.EndDate,
                        Priority = projectViewModel.Priority,
                        Comment = projectViewModel.Comment
                    };

                    if (projectViewModel.AssignedEmployeesIds != null && projectViewModel.AssignedEmployeesIds.Any())
                    {
                        _projectManager.CreateOrUpdateAndAssignEmployees(project,
                            projectViewModel.AssignedEmployeesIds);
                    }
                    else
                    {
                        _projectManager.CreateOrUpdate(project);
                    }
                }
                else if (projectViewModel.AssignedEmployeesIds != null && projectViewModel.AssignedEmployeesIds.Any() && projectViewModel.Id != 0)
                {
                    _projectManager.AssignEmployees(projectViewModel.Id, projectViewModel.AssignedEmployeesIds);
                }

                return "Success";
            }
            catch (Exception) // TODO: add custom
            {
                // TODO:
                return "Error";
            }
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
                    ViewBag.ActionToPerform = ViewStringConstants.Edit;

                    var assignedEmployeesIds = _projectManager.GetAssignedEmployeesIds(project.Id);

                    ProjectViewModel projectViewModel = GetProjectViewModel(project, assignedEmployeesIds);

                    var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    var jsonProjectViewModel = JsonConvert.SerializeObject(projectViewModel, Formatting.None, settings);
                    return View("AssignEmployees", string.Empty, jsonProjectViewModel);
                }
                catch (Exception) // TODO: add custom
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotSave);
                }
            }
            return View(project);
        }

        private static ProjectViewModel GetProjectViewModel(Project project, ICollection<int> assignedEmployeesIds)
        {
            var projectViewModel = new ProjectViewModel
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                CustomerCompanyName = project.CustomerCompanyName,
                ManagerId = project.ManagerId,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                Comment = project.Comment,
                AssignedEmployeesIds = assignedEmployeesIds
            };
            return projectViewModel;
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
            
            List<Employee> assignedEmployees = _projectManager.GetAssignedEmployees(project.Id);
            ProjectViewModel projectViewModel = GetProjectViewModel(project, assignedEmployees);

            return View(projectViewModel);
        }

        private ProjectViewModel GetProjectViewModel(Project project, ICollection<Employee> assignedEmployees)
        {
            var projectViewModel = new ProjectViewModel
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                CustomerCompanyName = project.CustomerCompanyName,
                ManagerId = project.ManagerId,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                Comment = project.Comment,
                AssignedEmployeesIds = assignedEmployees.Select(employee => employee.Id).ToList(),
                AssignedEmployees = assignedEmployees
            };
            return projectViewModel;
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