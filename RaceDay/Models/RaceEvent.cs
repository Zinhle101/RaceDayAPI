namespace RaceDay.API.Models
{ // start of namespace
    public class RaceEvent // stores information about a race or sporting event.
    { // start of class
        public int RaceEventId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string EventDescription { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        // foreign key linking the event to its organiser 
        public int OrganiserId { get; set; }

        // navigation property for the organiser 
        public User? Organiser { get; set; }

    } // end of class
} // end of namespace
