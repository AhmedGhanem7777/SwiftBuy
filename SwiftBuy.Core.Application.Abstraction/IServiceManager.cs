using SwiftBuy.Core.Application.Abstraction.Services.Basket;
using SwiftBuy.Core.Application.Abstraction.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction
{
    public interface IServiceManager
    {
        public IProductService ProductService { get;}
        public IBasketService BasketService { get;}
    }
}
