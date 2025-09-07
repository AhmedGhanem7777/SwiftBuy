using SwiftBuy.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Domain.Common.Entities
{
    public class ProductBrand : BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public string? PictureUrl { get; set; }
    }
}
