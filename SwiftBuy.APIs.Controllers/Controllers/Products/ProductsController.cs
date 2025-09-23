using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Controllers.Controllers.Base;
using SwiftBuy.APIs.Controllers.Filters;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Abstraction.Common;
using SwiftBuy.Core.Application.Abstraction.Models;

namespace SwiftBuy.APIs.Controllers.Controllers.Products
{
    public class ProductsController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public ProductsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var products = await _serviceManager.ProductService.GetPrductsAsync(specParams);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await _serviceManager.ProductService.GetPrductByIdAsync(id);
            // if (product is null)
            //     return NotFound(new {StatusCode = 404, Message = "Not Found"});
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetBrands()
        {
            var brands = await _serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);
        }
    }
}
