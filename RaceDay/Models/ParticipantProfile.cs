using RaceDay.API.Models;

namespace RaceDay.Models
{ // start of namespace
    public class ParticipantProfile // stores profile information for a participant 
    { // start of class
        public int ParticipantProfileId { get; set; }

        // foreign key that links the profile to a user
        public int UserId { get; set; }

        // navigation property that links the profile to the user
        public User? User { get; set; }

        public string EmergencyContactName { get; set; } = string.Empty;

        public string EmergencyContactNumber { get; set; } = string.Empty;

    } // end of class
} // end of namespace
