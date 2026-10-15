namespace RaceDay.API.Models
{ // Stores the categories available for a race event.

    public class RaceCategory
    { // start of class
        public int RaceCategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string CategoryDescription { get; set; } = string.Empty;

        public int RaceEventId { get; set; }
    } // end of class
} // end of namespace
