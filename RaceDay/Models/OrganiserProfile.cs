namespace RaceDay.Models
{ // start of namespace
    public class OrganiserProfile // stores information about a user's profile
    { // start of class
        public int OrganiserProfileId { get; set; }

        public int UserId { get; set; }

        public string OrganisationName { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

    } // end of class
} // end of namespace
