using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Logging.Contracts
{
    public interface ILogger
    {
        void LogError(string className, string methodName, string errorMsg);
        void LogError(string className, string methodName, Exception ex);
        void LogInfo(string className, string methodName, string log);
        void LogWarn(string className, string methodName, string log);
    }
}
