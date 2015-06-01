using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class DataAccessException : ApplicationException
    {
        public DataAccessException() : base("DataAccessException")
        {
        }

        public DataAccessException(string message)
            : base(message)
        {
        }

        public DataAccessException(string message, Exception ex)
            : base(message, ex)
        {
        }

    }
}
