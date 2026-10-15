namespace RaceDay.API.Models
{ // Stores the categories available for a race event.

    public class RaceCategory
    { // start of class
        public int RaceCategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryDescription { get; set; } = string.Empty;

        // foreign key linking cataegory to its race event 
        public int RaceEventId { get; set; }

        // navigation property that connects to the related race event
        public RaceEvent? RaceEvent { get; set; } 

    } // end of class
} // end of namespace
