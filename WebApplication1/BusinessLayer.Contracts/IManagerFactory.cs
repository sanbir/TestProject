namespace BusinessLayer.Contracts
{
    public interface IManagerFactory
    {
        TManager GetManager<TManager>() where TManager : IManager;
    }
}
