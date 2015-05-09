using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IManager
    {
    }

    public interface IManager<T> where T : class, new()
    {
    }
}
