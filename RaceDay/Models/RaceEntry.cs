using RaceDay.API.Models;

namespace RaceDay.Models
{ // start of namespace 
    public class RaceEntry  // stores a participant's entry into a race category.
    { // start of class
        public int RaceEntryId { get; set; }


        // foreign key linking an entry to a participant 
        public int UserId { get; set; }

        // navigation property linking an entry to a participant
        public User? Participant { get; set; }

        // foreign key linking the race entry to a category
        public int RaceCategoryId { get; set; }

        // navigation property that connects a race entry to the related category
        public RaceCategory? RaceCategory { get; set; }

        public DateTime EntryDate { get; set; }

        public string EntryStatus { get; set; } = "Pending";

    } // end of class

} // end of namespace
