using SwiftBuy.Core.Application.Abstraction.Services;
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
    }
}
