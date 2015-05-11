using System.ComponentModel.Composition.Hosting;
using System.Data.Entity.Infrastructure.Interception;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BusinessLayer.Managers;
using ContosoUniversity.DAL;
using Data.Models;
using DataAccessLayer.DataRepositories;
using PresentationLayer.Bootstrapper;

namespace ContosoUniversity
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbInterception.Add(new SchoolInterceptorTransientErrors());
            DbInterception.Add(new SchoolInterceptorLogging());

            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeRepository).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeManager).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            CompositionContainer container = new CompositionContainer(catalog);
            IControllerFactory mefControllerFactory = new MefControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(mefControllerFactory);
            EntityBase.Container = container;
        } 
    }
}
