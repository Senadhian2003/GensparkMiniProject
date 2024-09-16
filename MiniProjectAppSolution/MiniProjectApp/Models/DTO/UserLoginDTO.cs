using System.ComponentModel.DataAnnotations;

namespace MiniProjectApp.Models.DTO
{
    public class UserLoginDTO
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
        public string Password { get; set; } = string.Empty;

    }
}
