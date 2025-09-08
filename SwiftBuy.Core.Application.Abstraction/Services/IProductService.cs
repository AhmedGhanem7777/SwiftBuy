using SwiftBuy.Core.Application.Abstraction.Common;
using SwiftBuy.Core.Application.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Services
{
    public interface IProductService
    {
        Task<Pagination<ProductToReturnDto>> GetPrductsAsync(ProductSpecParams specParams);
        Task<ProductToReturnDto> GetPrductByIdAsync(int id);

        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
}
