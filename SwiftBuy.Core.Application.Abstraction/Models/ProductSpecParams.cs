using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Models
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public int PageIndex { get; set; } = 1;
        
        private const int MaxPageSize = 30;
        private int _pageSize = 24;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        private string? search;
        public string? Search
        {
            get => search;
            set
            {
                search = value?.Trim().ToUpper();
            }
        }
    }
}
