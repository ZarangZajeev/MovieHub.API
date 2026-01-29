namespace MovieHub.API.Models
{
    public class SpecificShowDetails
    {
        public int ShowId { get; set; }
        public string MovieName { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime ShowTime { get; set; }
        public string ScreenName { get; set; }
        public int TotalSeats { get; set; }


        public int AvailableSeatCount { get; set; }
        public int HeldSeatCount { get; set; }
        public int BookedSeatCount { get; set; }

        public string[] AvailableSeats { get; set; }
        public string[] HeldSeats { get; set; }
        public string[] BookedSeats { get; set; }
    }
}
