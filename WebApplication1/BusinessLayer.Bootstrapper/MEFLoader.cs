using System.ComponentModel.Composition.Hosting;
using DataAccessLayer.DataRepositories;

namespace BusinessLayer.Bootstrapper
{
    public static class MefLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(EmployeeRepository).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }

    }
}
