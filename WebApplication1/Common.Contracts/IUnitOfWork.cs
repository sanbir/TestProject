using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }

    public interface IUnitOfWork<T> : IUnitOfWork
        where T : class, new()
    {
        IEnumerable<IDataRepository<T>> Get();
    }
}
