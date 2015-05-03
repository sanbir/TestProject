using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    interface IUnitOfWorkFactory
    {
        T GetUnitOfWork<T>() where T : IUnitOfWork;
    }
}
