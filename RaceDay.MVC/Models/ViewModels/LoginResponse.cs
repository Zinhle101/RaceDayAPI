
namespace RaceDay.MVC.Models
{
    // Stores the response received after a successful API login.
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public int UserID { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserSurname { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
