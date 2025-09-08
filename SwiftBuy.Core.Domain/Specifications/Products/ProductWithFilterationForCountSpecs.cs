using SwiftBuy.Core.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Specifications.Products
{
    public class ProductWithFilterationForCountSpecs : BaseSpecifications<Product, int>
    {
        public ProductWithFilterationForCountSpecs(int? brandId, int? categoryId, string? search)
            : base(p =>
                       (string.IsNullOrEmpty(search) || p.Name.ToUpper().Contains(search))
                       &&
                       (!brandId.HasValue || p.BrandId == brandId.Value)
                       &&
                       (!categoryId.HasValue || p.CategoryId == categoryId.Value)
            )
        {
            
        }
    }
}
