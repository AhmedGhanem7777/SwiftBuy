using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Models
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDto> Items { get; set; }
    }
}
