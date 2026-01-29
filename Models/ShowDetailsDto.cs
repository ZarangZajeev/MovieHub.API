namespace MovieHub.API.Models
{
    public class ShowDetailsDto
    {
        public int ShowId { get; set; }
        public string MovieName { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime ShowTime { get; set; }
        public string ScreenName { get; set; }
        public int TotalSeats { get; set; }

    }
}
