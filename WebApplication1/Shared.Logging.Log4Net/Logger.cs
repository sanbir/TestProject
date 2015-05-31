using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Logging.Contracts;

namespace Shared.Logging.Log4Net
{
    [Export(typeof(ILogger))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class Logger : ILogger
    {
        private readonly log4net.ILog _log;

        public Logger()
        {
        }

        public Logger(string assemblyName)
        {
            _log = log4net.LogManager.GetLogger(assemblyName);
        }

        public void LogError(string className, string methodName, string errorMsg)
        {
            _log.Error(string.Concat(className, " : ", methodName, " =>", errorMsg));
        }

        public void LogError(string className, string methodName, Exception ex)
        {
            _log.Error(string.Concat(className, " : ", methodName, " =>", ex.Message), ex);
        }

        public void LogInfo(string className, string methodName, string log)
        {
            _log.Info(string.Concat(className, " : ", methodName, " =>", log));
        }

        public void LogWarn(string className, string methodName, string log)
        {
            _log.Warn(string.Concat(className, " : ", methodName, " =>", log));
        }

    }
}
