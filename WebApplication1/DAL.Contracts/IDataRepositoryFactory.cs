namespace DAL.Contracts
{
    public interface IDataRepositoryFactory
    {
        TDataRepository GetDataRepository<TDataRepository>() where TDataRepository : IDataRepository;
    }
}
