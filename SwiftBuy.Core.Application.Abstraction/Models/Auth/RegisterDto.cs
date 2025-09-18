using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftBuy.Core.Application.Abstraction.Models.Auth
{
    public class RegisterDto
    {
        [Required]
        public required string DisplayName { get; set; }
        [Required]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                                    ErrorMessage = "Password must be at least 8 characters, include uppercase, lowercase, number, and special character.")]
        public required string Password { get; set; }
    }
}
