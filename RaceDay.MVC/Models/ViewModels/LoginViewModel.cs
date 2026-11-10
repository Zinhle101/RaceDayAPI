
using System.ComponentModel.DataAnnotations;

namespace RaceDay.MVC.Models.ViewModels
{
    // Stores the information entered on the login page.
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
