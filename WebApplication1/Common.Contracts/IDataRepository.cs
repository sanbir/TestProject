using System.Collections.Generic;
using Data.Models;

namespace Data.Contracts
{
    public interface IDataRepository
    {
    }

    public interface IDataRepository<TEntity> : IDataRepository
        where TEntity : class, IEntity, new()
    {
        TEntity Add(TEntity entity);

        void Remove(TEntity entity);

        void Remove(int id);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> Get();

        TEntity Get(int id);
    }
}
