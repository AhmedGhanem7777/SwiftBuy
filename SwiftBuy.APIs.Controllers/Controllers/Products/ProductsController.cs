using Microsoft.AspNetCore.Mvc;
using SwiftBuy.APIs.Controllers.Controllers.Base;
using SwiftBuy.Core.Application.Abstraction;
using SwiftBuy.Core.Application.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.APIs.Controllers.Controllers.Products
{
    public class ProductsController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public ProductsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await _serviceManager.ProductService.GetPrductsAsync();
            return Ok(products);
        }
    }
}
