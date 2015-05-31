using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Pagination
{
    internal class PagedEnumerable<T> : IEnumerable<T>
    {
        #region Fields
        private IEnumerable<T> collection;
        private Paging paging;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="paging"></param>
        public PagedEnumerable(IEnumerable<T> collection, Paging paging)
        {
            if (paging == null)
                throw new ArgumentNullException("paging");

            this.collection = collection;
            this.paging = paging;
        }
        #endregion

        #region Methods
        public IEnumerator<T> GetEnumerator()
        {
            return DoGetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DoGetEnumerator();
        }
        private IEnumerator<T> DoGetEnumerator()
        {
            var count = 0;
            var size = 0;
            var skip = this.paging.GetSkip();
            var enumerator = this.collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (++count <= skip)
                    continue;
                if (++size > this.paging.PageSize)
                    continue;
                yield return enumerator.Current;
            }
            this.paging.CalculateAndSetPageCount(count);
        }
        #endregion
    }
}
