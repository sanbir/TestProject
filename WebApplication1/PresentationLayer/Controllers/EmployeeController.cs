using System;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Create()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //var employees = from s in db.Students
            //               select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    employees = employees.Where(s => s.LastName.Contains(searchString)
            //                           || s.FirstMidName.Contains(searchString));
            //}
            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        employees = employees.OrderByDescending(s => s.LastName);
            //        break;
            //    case "Date":
            //        employees = employees.OrderBy(s => s.EnrollmentDate);
            //        break;
            //    case "date_desc":
            //        employees = employees.OrderByDescending(s => s.EnrollmentDate);
            //        break;
            //    default:  // Name ascending 
            //        employees = employees.OrderBy(s => s.LastName);
            //        break;
            //}

            //int pageSize = 3;
            //int pageNumber = (page ?? 1);
            //return System.Web.UI.WebControls.View(employees.ToPagedList(pageNumber, pageSize));

            throw new NotImplementedException();
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