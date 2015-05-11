using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;

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
