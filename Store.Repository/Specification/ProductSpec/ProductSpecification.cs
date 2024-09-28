using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.ProductSpec
{
    public class ProductSpecification
    {
        public  int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private const int MAXSize = 50;
        private int _pageSize=6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value>MAXSize)?int.MaxValue:value;

        }

        private string _search;

        public string? Search
        {
            get => _search; 
            set=> _search = value?.Trim().ToLower(); 
        }


    }
}
