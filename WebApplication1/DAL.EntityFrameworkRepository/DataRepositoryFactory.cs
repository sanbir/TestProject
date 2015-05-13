using System.ComponentModel.Composition;
using DAL.Contracts;
using Shared.Models;

namespace DAL.EntityFrameworkRepository
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
