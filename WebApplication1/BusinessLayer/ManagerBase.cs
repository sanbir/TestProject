using System;
using System.ComponentModel.Composition;
using Shared.Exceptions;
using Shared.Models;

namespace BusinessLayer
{
    public class ManagerBase
    {
        protected ManagerBase()
        {
            if (EntityBase.Container != null)
                EntityBase.Container.SatisfyImportsOnce(this);
        }

        protected T ExecuteExceptionHandledOperation<T>(Func<T> codetoExecute)
        {
            try
            {
                return codetoExecute.Invoke();
            }
            catch (DataAccessException ex)
            {
                // TODO: Logger.Error(ex);
                throw new DataAccessException();
            }
            catch (Exception ex)
            {
                // TODO: Logger.Error(ex);
                throw new DataAccessException();
            }
        }

        protected void ExecuteExceptionHandledOperation(Action codetoExecute)
        {
            try
            {
                codetoExecute.Invoke();
            }
            catch (Exception ex)
            {
                // TODO: Logger.Error(ex);
                throw new DataAccessException();
            }
        }
    }
}
