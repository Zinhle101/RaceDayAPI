namespace RaceDay.Models
{ // start of namespace
    public class ParticipantProfile // stores profile information for a participant 
    { // start of class
        public int ParticipantProfileId { get; set; }

        public int UserId { get; set; }

        public string EmergencyContactName { get; set; } = string.Empty;

        public string EmergencyContactNumber { get; set; } = string.Empty;

    } // end of class
} // end of namespace
