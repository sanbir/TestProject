using System.Collections.Generic;

namespace Data.Contracts
{
    public interface IDataRepository
    {
        // do nothing
    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        T Add(T entity);

        void Remove(T entity);

        void Remove(int id);

        T Update(T entity);

        IEnumerable<T> Get();

        T Get(int id);
    }
}
