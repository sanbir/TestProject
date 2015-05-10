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
        T IManagerFactory.GetManager<T>()
        {
            return EntityBase.Container.GetExportedValue<T>();
        }
    }
}
