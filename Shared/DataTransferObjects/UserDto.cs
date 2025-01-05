using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public string FullName { get; set; }    // Kullanıcının tam adı
        public int Id { get; set; }             // Kullanıcı ID'si
        public DateTime LastLogin { get; set; } // Son giriş zamanı (Opsiyonel)
    }

    public record UserForRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public ICollection<string>? Roles { get; init; }
    }


    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }

}