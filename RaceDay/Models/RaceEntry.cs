namespace RaceDay.Models
{ // start of namespace 
    public class RaceEntry  // stores a participant's entry into a race category.
    { // start of class
        public int RaceEntryId { get; set; }

        public int UserId { get; set; }

        public int RaceCategoryId { get; set; }

        public DateTime EntryDate { get; set; }

        public string EntryStatus { get; set; } = "Pending";

    } // end of class

} // end of namespace
