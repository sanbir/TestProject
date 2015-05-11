using System;
using System.Web;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Constants.Common;
using Data.Models;

namespace WebApplication
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AggregateCatalog catalog = new AggregateCatalog();

            string dataRepositoriesFolder = ConfigurationManager.AppSettings[MefAccess.DataRepositoriesFolder];
            string businessLayerManagersFolder = ConfigurationManager.AppSettings[MefAccess.BusinessLayerManagersFolder];

            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.LoadFile(dataRepositoriesFolder + MefAccess.DataAccessLayerDll)));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.LoadFile(businessLayerManagersFolder + MefAccess.BusinessLayerDll)));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            CompositionContainer container = new CompositionContainer(catalog);
            IControllerFactory mefControllerFactory = new MefControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(mefControllerFactory);
            EntityBase.Container = container;
        } 
    }
}
