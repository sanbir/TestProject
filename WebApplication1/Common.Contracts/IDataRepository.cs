using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    public interface IDataRepository
    {

    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        void Add(T entity);

        void Remove(T entity);

        void Remove(int id);

        void Update(T entity);

        IEnumerable<T> Get();

        T Get(int id);
    }
}
