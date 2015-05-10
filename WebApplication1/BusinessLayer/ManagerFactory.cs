using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Contracts;
using Data.Models;

namespace BusinessLayer
{
    [Export(typeof(IManagerFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManagerFactory : IManagerFactory
    {
        TManager IManagerFactory.GetManager<TManager>()
        {
            return EntityBase.Container.GetExportedValue<TManager>();
        }
    }
}
