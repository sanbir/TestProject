using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Net;
using System.Web.Mvc;
using BusinessLayer.Contracts.Managers;
using Common.Constants.Common;
using Common.Constants.Project;
using Data.Models;
using PagedList;

namespace ContosoUniversity.Controllers
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = ProjectProperties.BindProjectProperties)]Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _projectManager.CreateOrUpdate(project);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception) // TODO: add custom
            {
                ModelState.AddModelError(string.Empty, ErrorMessages.CouldNotSave);
            }
            return View(project);
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