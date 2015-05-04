using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public abstract class EntityBase
    {
        public abstract int Id { get; set; }
        public static CompositionContainer Container { get; set; }
    }
}
