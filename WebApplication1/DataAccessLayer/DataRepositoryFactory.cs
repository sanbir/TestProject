using System.ComponentModel.Composition;
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
