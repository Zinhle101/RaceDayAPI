using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RaceDay.MVC.Models
{
    // Information entered when a user logs in.
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }

    // Information entered when a participant registers.
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string UserSurname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must have at least 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    // Information sent to the API during registration and login.
    public class AccountRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Participant";
    }

    // Information returned by the API after successful login.
    public class ApiLoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("userID")]
        public int UserID { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("userSurname")]
        public string UserSurname { get; set; } = string.Empty;

        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;
    }
}