
using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.Models.ViewModels
{
    // Stores the information entered when creating an account.
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your first name.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your surname.")]
        public string UserSurname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password.")]
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
