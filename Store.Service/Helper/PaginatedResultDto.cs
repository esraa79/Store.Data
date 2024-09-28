using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Helper
{
    public class PaginatedResultDto<T>
    {
        public  int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public  int PageSize { get; set; }

        public IReadOnlyList<T> Data { get; set; }

       

        public PaginatedResultDto(int pageSize, int totalCount, int pageIndex, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            TotalCount = totalCount;
            PageSize = pageSize;
            Data = data;
        }
    }
}
