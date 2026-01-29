namespace MovieHub.API.Models
{
    public class UserBookingDto
    {
        public string BookingReference { get; set; }
        public string Username { get; set; }
        public int ShowId { get; set; }
        public string MovieName { get; set; }
        public string ScreenName { get; set; }
        public DateTime ShowTime { get; set; }
        public int TotalSeats { get; set; }
        public string Status { get; set; }
        public int BookedSeatsCount { get; set; }
        public string[] BookedSeats { get; set; }
    }
}
