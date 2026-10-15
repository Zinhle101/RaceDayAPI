namespace RaceDay.Models
{ //start of namespace
    public class RaceResult // stores the results recorded for a race entry.
    { // start of class
        public int RaceResultId { get; set; }

        public int RaceEntryId { get; set; }

        public int FinishingPosition { get; set; }

        public TimeSpan FinishTime { get; set; }

    } // end of class
} // end of namespace
