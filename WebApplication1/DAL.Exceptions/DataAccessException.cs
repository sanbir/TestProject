using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class DataAccessException : ApplicationException
    {
        public DataAccessException(string message)
            : this(message, null)
        {
        }

        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
