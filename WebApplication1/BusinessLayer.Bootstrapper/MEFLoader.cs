using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DataRepositories;

namespace BusinessLayer
{
    public static class MEFLoader
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
