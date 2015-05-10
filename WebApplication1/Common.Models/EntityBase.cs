using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class EntityBase : IEntity
    {
        [Browsable(false)]
        public int Id { get; set; }

        [Browsable(false)]
        public static CompositionContainer Container { get; set; }
    }
}
