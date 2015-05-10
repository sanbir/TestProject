namespace Data.Contracts
{
    public interface IDataRepositoryFactory
    {
        TDataRepository GetDataRepository<TDataRepository>() where TDataRepository : IDataRepository;
    }
}
