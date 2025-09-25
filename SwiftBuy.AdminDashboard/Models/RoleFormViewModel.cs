using System.ComponentModel.DataAnnotations;

namespace SwiftBuy.AdminDashboard.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Role Name is required")]
        [StringLength(256, ErrorMessage = "Role Name cannot exceed 256 characters")]
        public string Name { get; set; }
    }
}
