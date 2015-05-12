using System.ComponentModel.Composition;
using Shared.Models;

namespace BusinessLayer
{
    public class ManagerBase
    {
        public ManagerBase()
        {
            if (EntityBase.Container != null)
                EntityBase.Container.SatisfyImportsOnce(this);
        }
    }
}
