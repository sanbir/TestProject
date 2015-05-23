using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Pagination
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, Paging paging)
        {
            return new PagedEnumerable<T>(collection, paging);
        }
    }
}
