using System.ComponentModel.Composition;
using Data.Models;

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
