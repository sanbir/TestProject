using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;

namespace Shared.Models
{
    public class EntityBase : IEntity
    {
        [Browsable(false)]
        public int Id { get; set; }

        [Browsable(false)]
        public static CompositionContainer Container { get; set; }
    }
}
