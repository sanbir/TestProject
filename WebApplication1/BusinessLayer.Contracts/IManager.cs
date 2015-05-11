using System.Collections.Generic;
using System.ComponentModel;
using Data.Models;

namespace BusinessLayer.Contracts
{
    public interface IManager
    {
    }

    public interface IManager<TEntity> : IManager
        where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> GetAll(ListSortDirection sortDirection,
            PropertyDescriptor sortPropertyDescriptor, string filter);

        IEnumerable<TEntity> GetAll(string sortDirection,
            string sortPropertyName, string filter);

        TEntity Get(int id);

        void CreateOrUpdate(TEntity entity);

        void Delete(int id);
    }
}
