using SwiftBuy.Core.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int pageIndex, int pageSize, string? search)
            : base(p => 
                       (string.IsNullOrEmpty(search) || p.Name.ToUpper().Contains(search))
                       &&
                       (!brandId.HasValue || p.BrandId == brandId.Value)
                       &&
                       (!categoryId.HasValue || p.CategoryId == categoryId.Value)
            )
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }

            ApplyPagination((pageIndex - 1) * pageSize, pageSize);

        }
        public ProductWithBrandAndCategorySpecifications(int id)
            :base(p => p.Id == id)
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }

        public ProductWithBrandAndCategorySpecifications()
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }
    }
}
