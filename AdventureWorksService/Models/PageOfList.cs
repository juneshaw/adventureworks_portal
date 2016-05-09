using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureWorksService.Models
{
    public class PageOfList<T>
    {
        public PageOfList(IEnumerable<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            this.Data = new List<T>();
            this.Data.AddRange(items);

            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.TotalItemCount = totalItemCount;

            if (pageSize == -1)
                this.TotalPageCount = 1;
            else
                this.TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }

        public List<T> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount { get; private set; }
    }
}
