namespace RaceDay.API.Models
{ // start of namespace
    public class User
    { // this class stores information about a user.

        public int UserID { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserSurname { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    } // end of class
} // end of namespace
