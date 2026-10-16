namespace RaceDay.API.Models
{
    // Stores the information required for registration and login.
    public class AccountRequest
    {
        public string UserName { get; set; } = string.Empty;

        public string UserSurname { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        // Used during registration to identify the user's role.
        public string Role { get; set; } = "Participant";
    }
}
