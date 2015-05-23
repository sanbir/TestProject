using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Pagination
{
    public class Paging
    {
        #region Constructors
        public Paging(int pageSize, int pageNumber)
        {
            if (pageSize < 1)
                throw new ArgumentException("The parameter must not be less than one.", "size");
            if (pageNumber < 1)
                throw new ArgumentException("The parameter must not be less than one.", "number");

            PageSize = pageSize;
            PageNumber = pageNumber;
        }
        #endregion

        #region Properties
        public int PageSize { get; private set; }
        public int PageNumber { get; private set; }
        public int PageCount { get; private set; }
        #endregion

        #region Methods
        public int GetSkip()
        {
            if (PageSize == 0 && PageNumber == 0)
                return 0;

            return PageSize * (PageNumber - 1);
        }
        public void CalculateAndSetPageCount(int itemCount)
        {
            var sizeAsDouble = Convert.ToDouble(PageSize);
            var itemCountAsDouble = Convert.ToDouble(itemCount);

            PageCount = Convert.ToInt32(Math.Ceiling(itemCountAsDouble / sizeAsDouble));
        }
        #endregion
    }
}
