using SwiftBuy.Core.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;

namespace SwiftBuy.AdminDashboard.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Description { get; set; }
        public string? PictureUrl { get; set; }
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(1, 1000000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Brand Id is required")]
        public int? BrandId { get; set; }
        public ProductBrand? Brand { get; set; }
        [Required(ErrorMessage = "Product Category Id is required")]
        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
