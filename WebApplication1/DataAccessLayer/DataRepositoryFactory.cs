using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;

namespace DataAccessLayer
{
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        TDataRepository IDataRepositoryFactory.GetDataRepository<TDataRepository>()
        {
            return EntityBase.Container.GetExportedValue<TDataRepository>();
        }
    }
}
