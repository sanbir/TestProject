using System.Collections.Generic;
using Shared.Models;

namespace DAL.Contracts
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
