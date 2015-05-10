using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
